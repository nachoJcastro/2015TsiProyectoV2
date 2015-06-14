using BusinessLogicLayer;
using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.EntityTenant;
using Site.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recomendaciones;

namespace Site.Controllers
{
    public class TenantController : Controller
    {
        IBLTenant _ibl = new BLTenant();
        IBLSubasta _sub = new BLSubasta();
        IBLProducto proIBL = new BLProducto();
        
        public UsuarioSite user;
        public string tenantID;
        public TiendaTenant t;
        public EstiloTienda estilo;

        /*static LocalDataStoreSlot local;
        private String tenantID;
        static LocalDataStoreSlot tenant = Thread.AllocateNamedDataSlot("Tenant");*/
        // GET: Tenant
        public ActionResult Index(string id)
        {
            tenantID = id;
            System.Diagnostics.Debug.WriteLine("ID tenant :" + tenantID);
            if (_ibl.ExisteSitio(tenantID))
            {
                /*System.Diagnostics.Debug.WriteLine("ID tenant :" + id);
                System.Diagnostics.Debug.WriteLine("Tenant");
                ViewBag.Message = id;
                Session["sitio"] = new Sitio { dominio =id };

                System.Threading.ThreadLocal<String> tenant;

               ThreadLocal<string> tenant_thread = new ThreadLocal<string>(() =>
                {
                    return id;
                });

                tenant_thread.Value.ToString(); */
                /*local = Thread.GetNamedDataSlot("tenant");
                if (local == null) { 
                
                    local = Thread.AllocateNamedDataSlot("tenant");
                }
                Thread.SetData(local, id);
                string valor_Tenant = System.Threading.Thread.GetData(local).ToString();*/
                if ( System.Web.HttpContext.Current.Session["usuario"] == null)
                    System.Web.HttpContext.Current.Session["usuario"] =new UsuarioSite();
                //if (user == null) Session["usuario"] = new UsuarioSite();
                //System.Web.HttpContext.Current.Session["usuario"] = user;
                if (System.Web.HttpContext.Current.Session["datosTienda"] == null)
                    System.Web.HttpContext.Current.Session["datosTienda"] = new TiendaTenant();

                if (System.Web.HttpContext.Current.Session["estilo"] == null)
                    System.Web.HttpContext.Current.Session["estilo"] = new EstiloTienda();


                user = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
                user.Dominio = tenantID;
                user.idTienda = _ibl.ObtenerIdTenant(tenantID);

                t = System.Web.HttpContext.Current.Session["datosTienda"] as TiendaTenant;
                estilo = System.Web.HttpContext.Current.Session["estilo"] as EstiloTienda;

                t = _ibl.ObtenerDatosTenant(user.idTienda);
                estilo.css = CrearCss(t.Css, t.Nombre);

                List<ImagenesTenant> imagenesT = _ibl.ObtenerImgTenant(user.idTienda);

                System.Web.HttpContext.Current.Session["datosTienda"] = t;
                System.Web.HttpContext.Current.Session["estilo"] = estilo;
                System.Web.HttpContext.Current.Session["usuario"] = user;

                var lista_Subastas = _sub.ObtenerSubastas(tenantID);

                List<Subasta> ls_Subastas_Subasta = new List<Subasta>();
                foreach (Subasta item in lista_Subastas)
                {
                    if (item.finalizado == Crosscutting.Enum.TipoFinalizacion.Subasta)
                    {
                        ls_Subastas_Subasta.Add(item);
                    }
                }

                List<Subasta> ls_Subastas_Directa = new List<Subasta>();
                foreach (Subasta item in lista_Subastas)
                {
                    if (item.finalizado == Crosscutting.Enum.TipoFinalizacion.Compra_directa)
                    {
                        ls_Subastas_Directa.Add(item);
                    }
                }
                
                List<Subasta> lista_Subastas_Activas = new List<Subasta>();
                foreach (Subasta element in lista_Subastas)
                {
                    if(element.estado == Crosscutting.Enum.EstadoTransaccion.Activa){
                        lista_Subastas_Activas.Add(element);
                    }
                }

                List<Subasta> lista_Subastas_compraDirecta = new List<Subasta>();

                foreach (Subasta element in lista_Subastas_compraDirecta)
                {
                    if(element.finalizado == Crosscutting.Enum.TipoFinalizacion.Compra_directa){
                        lista_Subastas_compraDirecta.Add(element);
                    }
                }

                ViewBag.ListaCategorias = proIBL.ObtenerCategoriasPorTienda(user.idTienda);

                ViewBag.ListaImagenes = imagenesT;

                ViewBag.ListarSubastas = lista_Subastas;
                ViewBag.ListarSubastasActivas = lista_Subastas_Activas;

                ViewBag.ListarSubastasSubasta = lista_Subastas;
                ViewBag.ListarSubastasDirecta = lista_Subastas_compraDirecta;


                ///////////

                RamdomRecomendaciones recom = new RamdomRecomendaciones();

                IBLUsuario usuIBL = new BLUsuario();
                var idUsuario = usuIBL.ObtenerIdByEmail(tenantID, user.Email);

                List<Subasta> prodsfav = recom.AlgRecomen(tenantID, idUsuario);/////////////RECOMENDACIONES//////////////////////////////

                
                return View();
                //System.Diagnostics.Debug.WriteLine(" Dominio en sesion " + user.Dominio.ToString());
                //Thread t = Thread.CurrentThread;
                //System.Diagnostics.Debug.WriteLine(" Tenant Controller . desde el thread: "+ valor_Tenant);
            }
            else
            {
                Session.Clear();
                return RedirectToActionPermanent("Error");
            }

        }


          // FUNCION PARA CARGAR LA VISTA PARCIAL DE RESULTADOS
        //public PartialViewResult Busqueda(string opcion)
        //{


        //    var user = Session["usuario"] as UsuarioSite;
        //    var tenantID = user.idTienda.ToString();
        //    List<Subasta> lista_Subastas = _sub.ObtenerSubastas(tenantID);


        //    List<Subasta> ls_Subastas_Subasta = new List<Subasta>();
        //    foreach (Subasta item in lista_Subastas)
        //    {
        //        if (item.finalizado.ToString() == Crosscutting.Enum.TipoFinalizacion.Subasta.ToString())
        //        {
        //            ls_Subastas_Subasta.Add(item);

        //            // System.Diagnostics.Trace.WriteLine("DESCRIPCION SUBASTA" + item.descripcion);
        //        }

        //    }


        //    List<Subasta> ls_Subastas_Directa = new List<Subasta>();
        //    foreach (Subasta item in lista_Subastas)
        //    {
        //        if (item.finalizado == Crosscutting.Enum.TipoFinalizacion.Compra_directa)
        //        {
        //            ls_Subastas_Directa.Add(item);
        //        }
        //    }



        //    if (opcion == "Subasta")
        //    {
        //        ViewBag.Resultado = ls_Subastas_Subasta;
        //        return PartialView(ls_Subastas_Subasta);
        //    }

        //    else
        //    {
        //        ViewBag.Resultado = ls_Subastas_Directa;
        //        return PartialView(ls_Subastas_Directa);
        //    }

        //}
           
        public ActionResult Error(string id)
        {
            ViewBag.Message = "Pagina de error. No existe el sitio: " + id;
            return View();


        }

        public String CrearCss(string texto, string nombreT)
        {
            //var contentType = "text/css";    

            String rutaCarpetafull = "~/Content/";

            var nombreCss = nombreT + "_" + Guid.NewGuid().ToString() + ".css";
            var rutaRetorno = rutaCarpetafull + nombreCss;
            var ruta = Path.Combine(Server.MapPath(rutaCarpetafull), nombreCss);

            var content = texto;
            //var bytes = Encoding.UTF8.GetBytes(content);


            StreamWriter sw = System.IO.File.CreateText(ruta);
            sw.WriteLine(content);
            sw.Close();


            //var result = new FileContentResult(bytes, contentType);

            //Si no existe el directorio se crea
            //if (!Directory.Exists(rutaCarpetafull))
            //{
            //    DirectoryInfo di = Directory.CreateDirectory(rutaCarpetafull);

            //}
            //if (System.IO.File.Exists(tienda.Css))
            //{
            //    String dir = tienda.Css;
            //    using (StreamReader sr = new StreamReader(dir))
            //    {
            //        String line = sr.ReadToEnd();
            //        var bytes = Encoding.UTF8.GetBytes(line);
            //        var result = new FileContentResult(bytes, contentType);
            //        result.FileDownloadName = "Site.css";
            //        return result;
            //    }
            //}


            return nombreCss;

        }

        //public ContentResult GetCss(string id)
        //{
        //    string cssBody = GetCssBodyFromDatabase(id);
        //    return Content(cssBody, "text/css");
        //}
    }
}