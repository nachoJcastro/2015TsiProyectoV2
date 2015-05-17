using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Crosscutting.EntityTenant;
using Site.Models;
using BusinessLogicLayer.TenantInterfaces;
using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.Interfaces;
using Crosscutting.Enum;
using Crosscutting.Entity;

namespace Site.Controllers
{
    public class SubastaController : Controller
    {
        //private SiteContext db = new SiteContext();
        IBLSubasta subIBL= new BLSubasta();
        IBLProducto proIBL= new BLProducto();
       /* IBLComentario comIBL;
        IBLProducto proIBL;
        IBLOferta ofeIBL;
        IBLCategoria catIBL;
        IBLAtributo atrIBL;*/
        public UsuarioSite user_sitio;
        private string valor_tenant;
        public UsuarioSite user;

        public SubastaController() { }

       /* public SubastasController(IBLSubasta subbl, IBLComentario combl, IBLProducto probl,IBLOferta ofebl, IBLCategoria catbl, IBLAtributo atrbl)
        {
            this.subIBL = subbl;
            this.comIBL = combl;
            this.proIBL = probl;
            this.ofeIBL = ofebl;
            this.catIBL = catbl;
            this.atrIBL = atrbl;
        }*/
        //ME DA ERROR CONSTRUCTOR X DEFECTO. DESCOMENTAR!!!!
        //public SubastasController() : this(new BLSubasta(), new BLComentario(), new BLProducto(), new BLOferta(), new BLCategoria(), new BLAtributo())
        //{

        //}

       
        // GET: Subastas
        public ActionResult Index()
        {
            user = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;



            //ViewBag.CategoriaId = new SelectList(catIBL.ObtenerCategorias(), "CategoriaId", "Nombre");
            //ViewBag.TipoId = new SelectList(proIBL.ObtenerProductos(), "TipoId", "Titulo");
            //ViewBag.Atributo = new SelectList(atrBL.)
            return View();
            
        }

        //// GET: Subastas/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Subasta subasta = db.Subastas.Find(id);
        //    if (subasta == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(subasta);
        //}

        // GET: Subastas/Create
        public ActionResult Create(string id)
        {
            try
            {
                user = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;

                 

                Subasta subasta = new Subasta();
                //subasta.id_Categoria = 1;
                //subasta.id_Producto = 1;
                subasta.id_Vendedor = 1;
                subasta.titulo = "Prueba";
                subasta.valor_Actual = 111;
                subasta.estado = EstadoTransaccion.Activa;
                subasta.finalizado = TipoFinalizacion.Subasta;

                 user_sitio = Session["usuario"] as UsuarioSite;

                if (user_sitio.Dominio != null)
                {
                    System.Diagnostics.Debug.WriteLine(" Dominio en sesion Login " + user_sitio.Dominio.ToString());
                    valor_tenant = user_sitio.Dominio.ToString();
                }

                subIBL.AltaSubasta(valor_tenant, subasta);

                if (user.idTienda == 0) { System.Diagnostics.Debug.WriteLine("Usuario nulo"); }
                else System.Diagnostics.Debug.WriteLine(user.idTienda.ToString());
                
                
                List<string> tipo =new List<string>();
                tipo.Add("Subasta");
                tipo.Add("Compra Directa");
                ViewData["Tipo"] = tipo;
               // ViewData["Categorias"] = proIBL.ObtenerCategoriasPorTienda(user.idTienda);
               // ViewData["Productos"] = proIBL.ObtenerTipoProdCategoria(user.idTienda);
               // ViewData["Atributos"] = proIBL.ObtenerAtributosTipoProd(user.idTienda);

                ViewBag.CategoriaId = new SelectList(proIBL.ObtenerCategoriasPorTienda(user.idTienda), "CategoriaId", "Nombre");
        }
            catch (Exception)
            {
                
                throw;
            }
           
            return View();
        }

        
        public JsonResult TipoProdList(int idCategoria)
        {

            System.Diagnostics.Debug.WriteLine("Entro en obtener producto. :" + proIBL.ObtenerTipoProdCategoria(user.idTienda, idCategoria).ToString());

            return Json(new SelectList(proIBL.ObtenerTipoProdCategoria(user.idTienda, idCategoria),"TipoProductoId","CategoriaId","Titulo" ), JsonRequestBehavior.AllowGet);
        
		}

        public JsonResult ArticuloList(int idTipoProd)
        {
            System.Diagnostics.Debug.WriteLine("Entro en obtener articulos. :" + proIBL.ObtenerAtributosTipoProd(user.idTienda, idTipoProd).ToString());

            return Json (new SelectList (proIBL.ObtenerAtributosTipoProd(user.idTienda,idTipoProd),"AtributoId","CategoriaId","Nombre"), JsonRequestBehavior.AllowGet);
        }

        public IList<TipoProductoDTO> ObtenerProducto(int CategoriaId)
        {
            System.Diagnostics.Debug.WriteLine("Entro en obtener producto. Id Categoria:" + CategoriaId);
            return proIBL.ObtenerTipoProdCategoria(user.idTienda, CategoriaId);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CargarCategoriaId(string CategoriaId)
        {
            var listaTipoProd = this.ObtenerProducto(Convert.ToInt32(CategoriaId));
            var tiposProd = listaTipoProd.Select(m => new SelectListItem()
            {
                Text = m.Descripcion,
                Value = m.CategoriaId.ToString(),
            });
            return Json(tiposProd, JsonRequestBehavior.AllowGet);
        }
        






        // POST: Subastas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="id_Comprador,id_Vendedor,id_Producto,estado,valor_Actual,finalizado,fecha_Inicio,fecha_Cierre")] Subasta subasta)
        {
            if (ModelState.IsValid)
            {
                user_sitio = Session["usuario"] as UsuarioSite;

                if (user_sitio.Dominio != null)
                {
                    System.Diagnostics.Debug.WriteLine(" Dominio en sesion Login " + user_sitio.Dominio.ToString());
                    valor_tenant = user_sitio.Dominio.ToString();
                }
                subIBL.AgregarSubasta(valor_tenant,subasta);
                //db.Subastas.Add(subasta);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subasta);
        }

        //// GET: Subastas/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Subasta subasta = db.Subastas.Find(id);
        //    if (subasta == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(subasta);
        //}

        //// POST: Subastas/Edit/5
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        //// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,id_Comprador,id_Vendedor,id_Producto,estado,valor_Actual,finalizado,fecha_Inicio,fecha_Cierre")] Subasta subasta)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(subasta).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(subasta);
        //}

        //// GET: Subastas/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Subasta subasta = db.Subastas.Find(id);
        //    if (subasta == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(subasta);
        //}

        //// POST: Subastas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Subasta subasta = db.Subastas.Find(id);
        //    db.Subastas.Remove(subasta);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
