using Crosscutting.EntityTareas;
using Crosscutting.EntityTenant;
using DAL.Contextos;
using DAL.DAL_Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALOfertaEF : IDALOferta
    {
        static TenantDB db;
        private DALSubastaEF _iblsub;
        private  DALUsuario _idal;
       

        public DALOfertaEF() {

            _iblsub = new DALSubastaEF();
        }


        public void AgregarOferta(String tenant,Oferta oferta)
        {
            try
            {
                db = new TenantDB(tenant);
                db.Oferta.Add(oferta);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Oferta ObtenerOferta(int ofertaId)
        {
            try
            {
                var oferta = db.Oferta.FirstOrDefault(o => o.id == ofertaId);
                return oferta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<Oferta> ObtenerOfertas()
        {
            var listaOfertas = new List<Oferta>();
            try
            {
                listaOfertas = db.Oferta.ToList();
                return listaOfertas;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        

        public void ActualizarOferta(Oferta oferta)
        {
            try
            {
                var ofertaVieja = db.Oferta.FirstOrDefault(o => o.id == oferta.id);
                if (ofertaVieja != null)
                {
                    ofertaVieja = oferta;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void EliminarOferta(int ofertaId)
        {
            try
            {
                var oferta = db.Oferta.FirstOrDefault(o => o.id == ofertaId);
                if (oferta != null)
                {

                    db.Oferta.Remove(oferta);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Oferta> ObtenerOfertasByProducto(String tenant, int id_subasta)
        {
            db = new TenantDB(tenant);
            var listaOfer = new List<Oferta>();
            try
            {
                listaOfer = db.Oferta.Where(o => o.id_Subasta == id_subasta).ToList();
                return listaOfer;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Correo> correoNuevaOferta(string tenant, Oferta oferta) {
            List<Correo> lista = new List<Correo>();

            try
            {
                 _iblsub = new DALSubastaEF();

                Subasta subasta =_iblsub.ObtenerSubasta(tenant, oferta.id_Subasta);
                if (subasta != null) {

                    System.Diagnostics.Debug.WriteLine("Entro correoNuevaOferta DAL ");
                    //Creo los correos a enviar
                    Correo comprador = correoOfertante(tenant, subasta, oferta);

                    Correo vendedor = _iblsub.correoVendedorOferta(tenant, subasta ,oferta);

                    List <Oferta> otras_ofertas = this.ObtenerOfertasSubasta(tenant,oferta.id_Subasta,oferta.id);

                    Oferta ultima= new Oferta();
                    ultima.Monto = 0;
                    ultima.id_Usuario = 0;

                    foreach(var item in  otras_ofertas){
                    
                        if (ultima.Monto<item.Monto){

                                ultima =    (Oferta)item;

                        }     
                    }

                    //Agrego
                    

                    if(ultima.id_Usuario!=0){
                        
                      Correo  ultimoOfertante = correoUltimoOfertante(tenant, subasta, oferta, ultima.id_Usuario);
                      lista.Add(ultimoOfertante);
                    }

                    lista.Add(comprador);

                    lista.Add(vendedor);

                    System.Diagnostics.Debug.WriteLine("Salgo correoNuevaOferta  DAL ");
                 }
            }
            catch (Exception)
            {

                throw;
            }



            return lista;

        }

    private Correo correoUltimoOfertante(string tenant,Subasta subasta,Oferta oferta,int id_ultimo)
    {

         Correo correo = new Correo();
            try
            {
                System.Diagnostics.Debug.WriteLine("Entro correo ultimo ofertante DAL");

                _idal= new DALUsuario();
                Usuario vendedor = _idal.GetUsuario(tenant, id_ultimo);
                Usuario comprador = _idal.GetUsuario(tenant,(int)oferta.id_Usuario);
                
                correo.destinatario = comprador.email;
                correo.asunto = "Lo siento  " + comprador.nick + ". Tu oferta en el articulo " + subasta.titulo + " ha sido superada.";
                correo.mensaje = "Articulo : " + subasta.titulo + "Nueva oferta " + oferta.Monto.ToString() + " Fecha : " + DateTime.Now.ToString() + System.Environment.NewLine + " Sitio " + tenant + "chebay.com";

                System.Diagnostics.Debug.WriteLine("Salgo ultimo ofertante DAL");
                

            }
            catch (Exception)
            {

                throw;
            }

            return correo;
 	    
    }

        //********************************
        
        public List<Oferta> ObtenerOfertasSubasta(String tenant, int id_subasta, int id_oferta)
        {
            
            db = new TenantDB(tenant);
            var listaOfertas = new List<Oferta>();
            try
            {
                listaOfertas = db.Oferta.Where(x=> x.id_Subasta==id_subasta && x.id!=id_oferta).ToList();
                return listaOfertas;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //*********************
        private Correo correoOfertante(string tenant, Subasta subasta, Oferta oferta)
        {
            Correo correo = new Correo();
            try
            {
                System.Diagnostics.Debug.WriteLine("Entro correo ofertante DAL");

                _idal= new DALUsuario();
                Usuario vendedor = _idal.GetUsuario(tenant, subasta.id_Vendedor);
                Usuario comprador = _idal.GetUsuario(tenant,(int)oferta.id_Usuario);
                
                correo.destinatario = comprador.email;
                correo.asunto = "Felicidades " + comprador.nick + ". Has ofertado el articulo " + subasta.titulo;
                correo.mensaje = "Articulo : " + subasta.titulo + "Valor oferta " + oferta.Monto.ToString() + " Fecha : " + DateTime.Now.ToString() + System.Environment.NewLine + " Sitio " + tenant + "chebay.com";

                System.Diagnostics.Debug.WriteLine("Salgo correoComprador DAL");
                

            }
            catch (Exception)
            {

                throw;
            }

            return correo;
        }



        
        //******************************
       

        /*public List<Oferta> ObtenerOfertasSubasta(string tenant, int id)
        {
            var listaOfertas = new List<Oferta>();
            try
            {
                listaOfertas = db.Oferta.Where(x=>x.==id ).to
                return listaOfertas;
            }
            catch (Exception e)
            {
                throw e;
            }
        }*/


        
    }
}
