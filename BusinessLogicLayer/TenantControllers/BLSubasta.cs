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
using log4net;


namespace BusinessLogicLayer.TenantControllers
{
    public class BLSubasta : IBLSubasta{
        
        private IDALSubasta _dal = new DALSubastaEF();

        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


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



        // TAREA FINALIZAR SUBASTAS
        public void FinalizarSubastasTarea(String tenant)
        {
            log.Info("Entro en finalizar SUBASTA POR TAREA");
            try
            {
                List<Subasta> subastas =ObtenerSubastasActivas(tenant);
                IBLOferta ioferta = new BLOferta();
                _dal = new DALSubastaEF();

                if (subastas.Count==0) System.Diagnostics.Debug.WriteLine("No hay subastas activas");
                if (subastas.Count > 0) System.Diagnostics.Debug.WriteLine("Cantidad Subastas = "+ subastas.Count.ToString());

                foreach (var item in subastas)
                {

                    log.Info("Hay subasta activa  Id =" + item.id.ToString());
                    List<Correo> lista = new List<Correo>();
                    DateTime ahora = DateTime.Now;
                    DateTime fecha_subasta = (DateTime)item.fecha_Cierre;

                    int resultado = DateTime.Compare(fecha_subasta, ahora);

                    log.Info("Paso fechas y resultado = " + resultado.ToString());

                    log.Info("Estado subasta = " + item.estado.ToString());

                    String estado = item.estado.ToString();



                    if (resultado <= 0 && estado.Equals("Activa"))
                    {
                        List<Oferta> ofertas = _dal.ObtenerOfertas(item.id);

                        log.Info("Cantidad ofertas = " + ofertas.Count.ToString());
                        if (ofertas.Count > 0)
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
                            catch (Exception e)
                            {
                                log.Error("Error", e);
                                throw e;
                            }


                        }
                        else {

                            log.Info("Sin ofertas = " + ofertas.Count.ToString());
                            try
                            {
                                
                                item.estado = EstadoTransaccion.Cerrada;
                                _dal.ActualizarSubasta(tenant, (Subasta)item);

                                log.Info("Actualizo Subasta sin ofertas " + item.titulo);
                                
                                
                                lista = new List<Correo>();

                                Correo correo = _dal.correoSinOfertas(tenant, (Subasta)item);

                                log.Info("Correo " + correo.mensaje);
                                
                                lista.Add( _dal.correoSinOfertas(tenant, (Subasta)item));
                                
                                
                                IEnvioCorreo _envio = new EnvioCorreo();

                                _envio.enviarCorreos(lista);
                                log.Info("Salgo Finalizar Compra directa sin oferta ");
                            }
                            catch (Exception e)
                            {
                                log.Error("Error", e);
                                throw e;
                            }
                        }



                    }   
                }
                log.Info("Salgo de finalizar SUBASTA POR TAREA");
            }
            catch (Exception e)
            {
                log.Error("Error", e);
                throw e;
            }
        }


        public void correoCompraDirecta(String tenant,Subasta sub) {
            List<Correo> lista = new List<Correo>();
            try
            {
                log.Info("Entro correoCompraDirecta ");
                
                lista = _dal.correoCompraDirecta(tenant, sub);
                IEnvioCorreo _envio = new EnvioCorreo();
                _envio.enviarCorreos(lista);
            }
            catch (Exception e)
            {
                log.Error("Error", e);
                throw e;
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


        public List<Subasta> ObtenerVentasbyUsuario(string tenant, int idUsuario)
        {
            return _dal.ObtenerVentasbyUsuario(tenant, idUsuario);
        }


        public List<Subasta> ObtenerComprasbyUsuario(string tenant, int idUsuario)
        {
            return _dal.ObtenerComprasbyUsuario(tenant, idUsuario);
        }

    }
}
