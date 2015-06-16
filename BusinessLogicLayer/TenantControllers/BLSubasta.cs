using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.EntityTenant;
using BusinessLogicLayer.TenantInterfaces;
using DAL;
using Crosscutting.Enum;
using BusinessLogicLayer.Controllers;
using Crosscutting.EntityTareas;
using ServicioCorreo;


namespace BusinessLogicLayer.TenantControllers
{
    public class BLSubasta : IBLSubasta{
        
        private IDALSubasta _dal = new DALSubastaEF();


        public BLSubasta(IDALSubasta dal)
        {
            this._dal = dal;
        }

        public BLSubasta() { }

        //********************
        public void AltaSubasta(string tenant, Subasta subasta) {

            _dal.AltaSubasta (tenant, subasta);
        }


        public void AgregarSubasta(String tenant,Subasta subasta)
        {
            _dal.AgregarSubasta(tenant,subasta);
        }


        public Subasta ObtenerSubasta(String tenant,int subastaId)
        {
            return _dal.ObtenerSubasta(tenant,subastaId);
        }


        public List<Subasta> ObtenerSubastas(string tenant)
        {
            return _dal.ObtenerSubastas(tenant);
        }


        public void ActualizarSubasta(String tenant, Subasta subasta)
        {
            _dal.ActualizarSubasta(tenant, subasta);
        }


        public void EliminarSubasta(int subastaId)
        {
            _dal.EliminarSubasta(subastaId);
        }


        public List<Oferta> ObtenerOfertas(int subastaId)
        {
            return _dal.ObtenerOfertas(subastaId);
        }


        public List<Subasta> ObtenerSubastasByTipoProducto(String tenant, int idTipoProducto)
        {
            return _dal.ObtenerSubastasByTipoProducto(tenant, idTipoProducto);
        }
        //public void FinalizarSubastaPorTiempo(String tenant,int subastaId)
        //{
        //    var subasta = ObtenerSubasta(tenant,subastaId);
        //    var ofertaGanadora = subasta.Oferta.LastOrDefault();
        //    subasta.id_Comprador = ofertaGanadora.id_Usuario;
        //    subasta.valor_Actual = ofertaGanadora.Monto;
        //    subasta.estado = EstadoTransaccion.Cerrada;
        //    subasta.finalizado = TipoFinalizacion.Subasta;
        //    _dal.ActualizarSubasta(subasta);

        //    //enviarMails(Uvendedor, Ucomprador);
        //}


        //public void FinalizarSubastaCompraDirecta(String tenant, int subastaId)
        //{
        //    var subasta = ObtenerSubasta(tenant,subastaId);
        //    subasta.estado = EstadoTransaccion.Cerrada;
        //    //subasta.id_Comprador = USUARIO LOGUEADO;
        //    subasta.valor_Actual = subasta.precio_Compra.Value;
        //    subasta.finalizado = TipoFinalizacion.Compra_directa;
        //    _dal.ActualizarSubasta(subasta);

        //    //enviarMails(Uvendedor, Ucomprador);
        //}
        public Boolean ActualizarMonto(string tenant, int id_subasta, double monto)
        {

             return _dal.ActualizarMonto( tenant, id_subasta,  monto);

        }


        /*public void FinalizarSubastaTiempo() {

            IBLTiendaVirtual tiendas= new BLTiendaVirtual();

            List<String> tenants = tiendas.ObtenerTenants();

            IBLSubasta sub = new BLSubasta();
            foreach (var item in tenants)
            {
                sub.FinalizarSubastasTarea(tenants);
            }
        }*/


        public List<Subasta> ObtenerSubastasActivas(String tenant)
        {
            return _dal.ObtenerSubastasActivas(tenant);
        }




        public void FinalizarSubastasTarea(String tenant)
        {
            System.Diagnostics.Debug.WriteLine("Entro en finalizar tarea por JOB");
            try
            {
                List<Subasta> subastas =this.ObtenerSubastas(tenant);
                IBLOferta ioferta = new BLOferta();
                _dal = new DALSubastaEF();

                foreach (var item in subastas)
                {

                    // List<Oferta> ofertas = ibl.ObtenerOfertas(item.id);  
                    List<Correo> lista = new List<Correo>();

                    DateTime ahora = DateTime.Now;

                    DateTime fecha_subasta = (DateTime)item.fecha_Cierre;

                    int resultado = DateTime.Compare(fecha_subasta, ahora);

                    System.Diagnostics.Debug.WriteLine("paso fechas y resultado = " + resultado.ToString());

                    if (resultado <= 0 && item.estado==EstadoTransaccion.Activa)
                    {
                        List<Oferta> ofertas = _dal.ObtenerOfertas(item.id);

                        if (ofertas != null)
                        {

                            var ofertasOrdenadas = ofertas.OrderByDescending(o => o.fecha);
                            //var oferta = ofertasOrdenadas.First();
                            IBLUsuario blUsu = new BLUsuario();
                            Usuario ganador = null;
                            foreach (var itemOfertas in ofertasOrdenadas)
                            {
                                if (ganador == null)
                                {
                                    var usuario = blUsu.GetUsuario(tenant, itemOfertas.id_Usuario);
                                    if (usuario.billetera > item.valor_Actual)
                                    {
                                        ganador = usuario;
                                        ganador.billetera = ganador.billetera - itemOfertas.Monto;
                                        blUsu.ActualizarUsuario(tenant, ganador);
                                        item.valor_Actual = itemOfertas.Monto;
                                        item.id_Comprador = ganador.id;
                                    }      
                                }
                            }
                            item.finalizado = TipoFinalizacion.Subasta;
                            item.estado = EstadoTransaccion.Cerrada;
                            _dal.ActualizarSubasta(tenant, item);

                            try
                            {
                                lista = _dal.correoCompraSubasta(tenant, (Subasta)item);
                                IEnvioCorreo _envio = new EnvioCorreo();
                                _envio.enviarCorreos(lista);
                            }
                            catch (Exception)
                            {
                                
                                throw;
                            }


                        }
                        else {

                            
                            try
                            {
                                System.Diagnostics.Debug.WriteLine("Entro Finalizar Compra directa sin oferta " + item.titulo);
                                lista = new List<Correo>();

                                Correo correo = _dal.correoSinOfertas(tenant, (Subasta)item);
                                System.Diagnostics.Debug.WriteLine("Correo "+ correo.mensaje);
                                
                                lista.Add( _dal.correoSinOfertas(tenant, (Subasta)item));
                                
                                
                                IEnvioCorreo _envio = new EnvioCorreo();

                                _envio.enviarCorreos(lista);
                                System.Diagnostics.Debug.WriteLine("Salgo Finalizar Compra directa sin oferta ");
                            }
                            catch (Exception)
                            {

                                throw;
                            }
            
                        
                        
                        
                        
                        
                        }



                        //enviarMails(Uvendedor, Ucomprador);*/
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void correoCompraDirecta(String tenant,Subasta sub) {
            List<Correo> lista = new List<Correo>();
            try
            {
                System.Diagnostics.Debug.WriteLine("Entro correoCompraDirecta ");
                
                lista = _dal.correoCompraDirecta(tenant, sub);
                IEnvioCorreo _envio = new EnvioCorreo();
                _envio.enviarCorreos(lista);
            }
            catch (Exception)
            {
                
                throw;
            }
            
            
        }

        public void AgregarImagen(string tenant, Imagen img) {
            _dal.AgregarImagen(tenant, img);
        }

        public List<Imagen> ObtenerImagenes(string tenant, int id) {
            return _dal.ObtenerImagenes(tenant, id);
        }

        public List<Subasta> ObtenerSubastasActivasxCategoria(string tenant, int idCat) {
            return _dal.ObtenerSubastasActivasxCategoria(tenant, idCat);
        }

     public int AgregarSubasta_ID(String tenant, Subasta subasta){

        return  _dal.AgregarSubasta_ID(tenant, subasta);

      }
        public List<Subasta> ObtenerSubastasPorCriterio(string tenant, int idCat, string criterio, int? tipo, string min, string max)
        {
            return _dal.ObtenerSubastasPorCriterio(tenant, idCat, criterio,tipo,min,max);
        }

    }
}
