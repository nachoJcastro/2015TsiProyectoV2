using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.EntityTareas;
using Crosscutting.EntityTenant;
using DAL;
using ServicioCorreo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.TenantControllers
{
    public class BLOferta : IBLOferta
    {
        private IDALOferta _dal = new DALOfertaEF();


        public BLOferta(IDALOferta dal)
        {
            this._dal = dal;
        }

        public BLOferta() { }


        public void AgregarOferta(String tenant,Oferta oferta)
        {
           try 
	        {	      
                _dal.AgregarOferta(tenant,oferta);
               // correoOferta( tenant, oferta);
		
	        }
	        catch (Exception)
	        {
		
		        throw;
	        }

        }

        public void correoOferta(String tenant, Oferta oferta)
        {
            List<Correo> lista = new List<Correo>();
            try
            {
                System.Diagnostics.Debug.WriteLine("Entro correoOferta ");

                lista = _dal.correoNuevaOferta(tenant, oferta);
                IEnvioCorreo _envio = new EnvioCorreo();
                _envio.enviarCorreos(lista);

                System.Diagnostics.Debug.WriteLine("Salgo correoOferta ");

            }
            catch (Exception)
            {

                throw;
            }


        }







        public Oferta ObtenerOferta(int ofertaId)
        {
            return _dal.ObtenerOferta(ofertaId);
        }


        public List<Oferta> ObtenerOfertas()
        {
            return _dal.ObtenerOfertas();
        }


        public void ActualizarOferta(Oferta oferta)
        {
            _dal.ActualizarOferta(oferta);
        }


        public void EliminarOferta(int ofertaId)
        {
            _dal.EliminarOferta(ofertaId);
        }

        public List<Oferta> ObtenerOfertasByProducto(String tenant,int id_subasta)
        {
            return _dal.ObtenerOfertasByProducto(tenant,id_subasta);
        }

    }
}
