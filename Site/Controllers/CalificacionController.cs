using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.EntityTenant;
using Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class CalificacionController : Controller
    {
        IBLCalificacion _blcalificacion;
        IBLSubasta subIBL;
        IBLUsuario _blusuario;
        private string valor_tenant;
        public UsuarioSite user_sitio;

        public CalificacionController(IBLCalificacion bl, IBLUsuario blU, IBLSubasta subIBL)
        {
            this._blcalificacion = bl;
            this.subIBL = subIBL;
            this._blusuario = blU;
        }

        public CalificacionController()
            : this(new BLCalificacion(), new BLUsuario(), new BLSubasta())
        {

        }

        // GET: Calificacion
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CreateCalificacion(int idSubasta, bool venta)
        {
            var user = Session["usuario"] as UsuarioSite;
            valor_tenant = user.Dominio;
            SubastaSite sub_site = new SubastaSite();
            try
            {
                user_sitio = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
                valor_tenant = user_sitio.Dominio.ToString();
                int idLogueado = _blusuario.ObtenerIdByEmail(valor_tenant, user_sitio.Email);
                var usuario = _blusuario.GetUsuario(valor_tenant, idLogueado);

                Subasta subasta = subIBL.ObtenerSubasta(valor_tenant, idSubasta);
                ViewBag.ListaImg = subIBL.ObtenerImagenes(valor_tenant, idSubasta);
                if (subasta == null)
                {
                    return HttpNotFound();
                }
                sub_site.id = subasta.id;
                sub_site.descripcion = subasta.descripcion;
                sub_site.fecha_Cierre = subasta.fecha_Cierre;
                sub_site.finalizado = subasta.finalizado;
                sub_site.nick_Comprador = _blusuario.GetNombreUsuario(valor_tenant, Convert.ToInt32(subasta.id_Comprador));
                sub_site.id_Comprador = Convert.ToInt32(subasta.id_Comprador);
                sub_site.portada = subasta.portada;
                sub_site.nombre_producto = subasta.titulo;
                sub_site.precio_Compra = Convert.ToDouble(subasta.precio_Compra);
                if (venta)
                {
                    sub_site.listaVenta = 1;
                }
                else
                {
                    sub_site.listaVenta = 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return View(sub_site);
        }


        [HttpPost]
        public JsonResult CreateCalificacion(int idSubasta, int puntaje, string observaciones, int venta)
        {
            user_sitio = Session["usuario"] as UsuarioSite;
            valor_tenant = user_sitio.Dominio.ToString();
            int idLogueado = _blusuario.ObtenerIdByEmail(valor_tenant, user_sitio.Email);

            CalificacionMongo calificacion = new CalificacionMongo();
            calificacion.id_Subasta = idSubasta;
            calificacion.observacion = observaciones;
            calificacion.id_Usuario = idLogueado;
            calificacion.puntaje = puntaje;
            calificacion.vendedor = (1 == venta);

            _blcalificacion.AgregarCalificacion(valor_tenant, calificacion);

            return Json(new { redirectUrl = Url.Action("DatosUsuario/", "Manage", new { idUsuario = idLogueado}), isRedirect = true });
        }


        public ActionResult ListadoComprasVentas(bool ventas)
        {
            try
            {
                user_sitio = Session["usuario"] as UsuarioSite;
                var idUsuario = _blusuario.ObtenerIdByEmail(user_sitio.Dominio, user_sitio.Email);
                valor_tenant = user_sitio.Dominio;
                List<Subasta> listaSubSite = new List<Subasta>();
                List<SubastaSite> listaSub = new List<SubastaSite>();
                if (ventas)
                {
                    listaSubSite = subIBL.ObtenerVentasbyUsuario(valor_tenant, idUsuario);

                }
                else
                {
                    listaSubSite = subIBL.ObtenerComprasbyUsuario(valor_tenant, idUsuario);
                    
                }

                foreach (var item in listaSubSite)
                {
                    SubastaSite subasta = new SubastaSite();
                    subasta.id = item.id;
                    try
                    {
                        subasta.titulo = item.titulo;
                        subasta.finalizado = item.finalizado;
                        subasta.precio_Compra = Convert.ToDouble(item.precio_Compra);
                        subasta.fecha_Cierre = item.fecha_Cierre;
                        subasta.portada = item.portada;
                        if (_blcalificacion.ObtenerCalificacionDelVendedor(valor_tenant, subasta.id) != null)
                        {
                            subasta.calificacionDelVendedor = _blcalificacion.ObtenerCalificacionDelVendedor(valor_tenant, subasta.id).puntaje;
                        }
                        if (_blcalificacion.ObtenerCalificacionDelComprador(valor_tenant, subasta.id) != null)
                        {
                            subasta.calificacionDelComprador = _blcalificacion.ObtenerCalificacionDelComprador(valor_tenant, subasta.id).puntaje;
                        }

                        listaSub.Add(subasta);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                ViewBag.idUsuario = idUsuario;
                ViewBag.Ventas = ventas;
                ViewBag.TamanioLista = listaSub.Count;
                ViewBag.ListaComprasVentas = listaSub.OrderBy(c => c.fecha_Cierre);
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
    }
}