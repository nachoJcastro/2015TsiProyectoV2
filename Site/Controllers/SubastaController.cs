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
using Microsoft.WindowsAzure.Storage.Blob;
using BusinessLogicLayer.Controllers;

namespace Site.Controllers
{
    public class SubastaController : Controller
    {
        //private SiteContext db = new SiteContext();
        IBLSubasta subIBL;
        IBLProducto proIBL;
        IBLComentario comIBL;
        IBLOferta ofeIBL;
        IBLUsuario usuIBL;
        IBLAtributo atrIBL;
        IBLAtributo_Subasta atrSubIBL;
        IBLFavorito favIBL;

        BlobStorage _bls = new BlobStorage();
        //IBLCategoria catIBL;
        //IBLAtributo atrIBL;
        public UsuarioSite user_sitio;
        private string valor_tenant;
        private SubastaSite sub_site;

        public SubastaController(IBLSubasta subbl, IBLComentario combl, IBLProducto probl, IBLOferta ofebl, IBLUsuario usubl, IBLAtributo atrIBL, IBLAtributo_Subasta atrSubIBL, IBLFavorito favIBL)
        {
            this.subIBL = subbl;
            this.comIBL = combl;
            this.proIBL = probl;
            this.ofeIBL = ofebl;
            this.usuIBL = usubl;
            this.atrIBL = atrIBL;
            this.favIBL = favIBL;
            this.atrSubIBL = atrSubIBL;
        }

        public SubastaController()
            : this(new BLSubasta(), new BLComentario(), new BLProducto(), new BLOferta(), new BLUsuario(), new BLAtributo(), new BLAtributo_Subasta(), new BLFavorito())
        {

        }

       
        // GET: Subastas
        public ActionResult Index()
        {
            user_sitio = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
            
            //ViewBag.CategoriaId = new SelectList(catIBL.ObtenerCategorias(), "CategoriaId", "Nombre");
            //ViewBag.TipoId = new SelectList(proIBL.ObtenerProductos(), "TipoId", "Titulo");
            //ViewBag.Atributo = new SelectList(atrBL.)
            return View();
            
        }

        // GET: Subastas/Details/5
        public ActionResult Details(int idSubasta)
        {
            
            try
            {
                user_sitio = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
                valor_tenant = user_sitio.Dominio.ToString();

                Subasta subasta = subIBL.ObtenerSubasta(valor_tenant, idSubasta);
                sub_site = new SubastaSite();
                sub_site.id = subasta.id;


                if (subasta.id_Comprador != null) {
                    sub_site.nick_Comprador=usuIBL.GetNombreUsuario(valor_tenant, (int)subasta.id_Comprador);
                }
                sub_site.titulo = subasta.titulo;
                sub_site.nick_Vendedor = usuIBL.GetNombreUsuario(valor_tenant, (int)subasta.id_Vendedor);
                sub_site.portada = subasta.portada;

                if (subasta.id_Producto != 0)
                {
                    var prod = proIBL.ObtenerProductoTenant(user_sitio.idTienda, subasta.id_Categoria, subasta.id_Producto);
                    sub_site.nombre_producto = prod.Titulo;
                }
               
                sub_site.valor_Actual = subasta.valor_Actual;
                sub_site.estado = subasta.estado;
                if (subasta.finalizado.Equals(TipoFinalizacion.Compra_directa))
                {
                    sub_site.tipo_venta = "Compra directa";
                }
                else 
                {
                    sub_site.tipo_venta = "Subasta";
                }
                
                sub_site.finalizado = subasta.finalizado;
                sub_site.fecha_Inicio = subasta.fecha_Inicio;
                sub_site.fecha_Cierre = subasta.fecha_Cierre;
                sub_site.garantia = subasta.garantia;
                sub_site.Comentario = subasta.Comentario;
                sub_site.Atributo_Subasta = subasta.Atributo_Subasta;
               
                sub_site.Calificacion = subasta.Calificacion;
                sub_site.Favorito = subasta.Favorito;

                if (subasta == null)
                {
                    return HttpNotFound();
                }

            }
            catch (Exception)
            {
                
                throw;
            }

            return View(sub_site);
        }

                
        // GET: Subastas/Create
        public ActionResult Create()
        {
            try
            {
                user_sitio = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
                valor_tenant = user_sitio.Dominio.ToString();
                //List<string> tipo = new List<string>();
                //tipo.Add("Subasta");
                //tipo.Add("Compra Directa");
                //ViewData["Tipo"] = tipo;
                ViewData["Categorias"] = proIBL.ObtenerCategoriasPorTienda(user_sitio.idTienda);
                ViewData["Productos"] = proIBL.ObtenerProductos();
                ViewData["Atributos"] = atrIBL.ObtenerAtributos();
                ViewBag.ListaAtributos = atrIBL.ObtenerAtributos();
                List<String> tipo_subasta = new List<String> { "Subasta", "Compra directa" };
                ViewData["Tipo"] = tipo_subasta;

                ViewBag.CategoriaId = new SelectList(proIBL.ObtenerCategoriasPorTienda(user_sitio.idTienda), "CategoriaId", "Nombre");

            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
        
        // POST: Subastas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "titulo,descripcion,tags,precio_Base,precio_Compra,garantia,coordenadas,fecha_Inicio,fecha_Cierre")]Subasta subasta, FormCollection form, HttpPostedFileBase portada)
        {
            user_sitio = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
            subasta.id_Vendedor = usuIBL.ObtenerIdByEmail(user_sitio.Dominio, user_sitio.Email);
            subasta.estado = EstadoTransaccion.Activa;
            subasta.valor_Actual = (double)subasta.precio_Base;
            subasta.fecha_Inicio = System.DateTime.Now;
            string tipo = form["Tipo"];
            string cat = form["Categorias"];
            string prod = form["Productos"];
            string atr = form["Atributos"];

            string atr_sub = form["Atributos"];

            int id_cat = int.Parse(cat);
            subasta.id_Categoria = id_cat;

            int producto = int.Parse(prod);
            subasta.id_Producto = producto;

            CloudBlobContainer blobContainer = _bls.GetContainerTienda(user_sitio.Dominio);


            if (portada != null && portada.ContentLength > 0)
            {

                //Elminar foto anterior
                //TiendaVirtualDTO old = _bl.ObtenerTienda(tiendaVirtualDTO.TiendaVitualId);
                //CloudBlockBlob blobOld = blobContainer.GetBlockBlobReference("Nombreblob");
                //blobOld.Delete();


                var nombreFoto = user_sitio.Dominio + Guid.NewGuid().ToString() + "_subasta";
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(nombreFoto);
                blob.UploadFromStream(portada.InputStream);
                subasta.portada = blob.Uri.ToString();
            }


            string jsonData = Request.Form[0];
            string jsonData2 = Request.Form[1];
            

            if (tipo == "Subasta")
            {
                TipoFinalizacion tipoSub = TipoFinalizacion.Subasta;
                subasta.finalizado = tipoSub;

                valor_tenant = user_sitio.Dominio.ToString();
                subIBL.AgregarSubasta(valor_tenant, subasta);
            }
            else
            {
                TipoFinalizacion tipoSub = TipoFinalizacion.Compra_directa;
                subasta.finalizado = tipoSub;

                valor_tenant = user_sitio.Dominio.ToString();
                subIBL.AgregarSubasta(valor_tenant, subasta);

                sub_site = crearSubastaSite(subasta);
            }

            return View("DetalleProducto", sub_site);
       }

        private SubastaSite crearSubastaSite(Subasta subasta)
        {
            sub_site = new SubastaSite();
            try
            {
                user_sitio = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
                valor_tenant = user_sitio.Dominio.ToString();

                
                
                sub_site.id = subasta.id;


                if (subasta.id_Comprador != null)
                {
                    sub_site.nick_Comprador = usuIBL.GetNombreUsuario(valor_tenant, (int)subasta.id_Comprador);
                }
                sub_site.titulo = subasta.titulo;
                sub_site.nick_Vendedor = usuIBL.GetNombreUsuario(valor_tenant, (int)subasta.id_Vendedor);
                sub_site.portada = subasta.portada;

                if (subasta.id_Producto != 0)
                {
                    var prod = proIBL.ObtenerProductoTenant(user_sitio.idTienda, subasta.id_Categoria, subasta.id_Producto);
                    sub_site.nombre_producto = prod.Titulo;
                }

                sub_site.valor_Actual = subasta.valor_Actual;
                sub_site.estado = subasta.estado;
                if (subasta.finalizado.Equals(TipoFinalizacion.Compra_directa))
                {
                    sub_site.tipo_venta = "Compra directa";
                }
                else
                {
                    sub_site.tipo_venta = "Subasta";
                }

                sub_site.finalizado = subasta.finalizado;
                sub_site.fecha_Inicio = subasta.fecha_Inicio;
                sub_site.fecha_Cierre = subasta.fecha_Cierre;
                sub_site.garantia = subasta.garantia;
                sub_site.Comentario = subasta.Comentario;
                sub_site.Atributo_Subasta = subasta.Atributo_Subasta;

                sub_site.Calificacion = subasta.Calificacion;
                sub_site.Favorito = subasta.Favorito;

               

            }
            catch (Exception)
            {

                throw;
            }

            return sub_site;
            
        }


        [HttpPost]
        public JsonResult AgregarAtributos(String valor, int idAtributo)//, int idSubasta
        {
            user_sitio = Session["usuario"] as UsuarioSite;
            valor_tenant = user_sitio.Dominio.ToString();
            Atributo_Subasta atributo = new Atributo_Subasta();
            //atributo.id_Subasta = idSubasta;
            atributo.valor = valor;
            atributo.id_Atributo = idAtributo;

            atrSubIBL.AgregarAtributo_Subasta(valor_tenant, atributo);
            return Json( JsonRequestBehavior.AllowGet);
        }


        public JsonResult TipoProdList(int idCategoria)
        {

            System.Diagnostics.Debug.WriteLine("Entro en obtener producto. :" + proIBL.ObtenerTipoProdCategoria(user_sitio.idTienda, idCategoria).ToString());
            //var ls = new[] { proIBL.ObtenerTipoProdCategoria(user.idTienda, idCategoria) };
            //return Json(ls, JsonRequestBehavior.AllowGet);
            return Json(new SelectList(proIBL.ObtenerTipoProdCategoria(user_sitio.idTienda, idCategoria), "TipoProductoId", "CategoriaId", "Titulo"), JsonRequestBehavior.AllowGet);
		}


        public JsonResult ArticuloList(int idTipoProd)
        {
            System.Diagnostics.Debug.WriteLine("Entro en obtener articulos. :" + proIBL.ObtenerAtributosTipoProd(user_sitio.idTienda, idTipoProd).ToString());

            return Json(new SelectList(proIBL.ObtenerAtributosTipoProd(user_sitio.idTienda, idTipoProd), "AtributoId", "CategoriaId", "Nombre"), JsonRequestBehavior.AllowGet);
        }


        public IList<TipoProductoDTO> ObtenerProducto(int CategoriaId)
        {
            System.Diagnostics.Debug.WriteLine("Entro en obtener producto. Id Categoria:" + CategoriaId);
            return proIBL.ObtenerTipoProdCategoria(user_sitio.idTienda, CategoriaId);
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


        public ActionResult FinalizarCompraDirecta(int idSubasta)
        {
            user_sitio = Session["usuario"] as UsuarioSite;
            valor_tenant = user_sitio.Dominio.ToString();
            int idLogueado = usuIBL.ObtenerIdByEmail(valor_tenant, user_sitio.Email);
            var usuario = usuIBL.GetUsuario(valor_tenant, idLogueado);

            Subasta subasta = subIBL.ObtenerSubasta(valor_tenant, idSubasta);

            if (subasta == null)
            {
                return HttpNotFound();
            }

            if (usuario.billetera < subasta.precio_Compra)
            {
                ViewBag.idSubasta = subasta.id;
                return View("SinSaldo");
            }
            else
            {
                usuario.billetera = usuario.billetera - (double)subasta.precio_Compra;
                usuIBL.ActualizarUsuario(valor_tenant, usuario);
                subasta.estado = EstadoTransaccion.Cerrada;
                subasta.finalizado = TipoFinalizacion.Compra_directa;
                subasta.id_Comprador = idLogueado;
               
                subIBL.ActualizarSubasta(valor_tenant, subasta);
                var vendedor = usuIBL.GetUsuario(valor_tenant, idLogueado);
                //enviar mail
                try
                {
                    subIBL.correoCompraDirecta(valor_tenant, subasta);

                }
                catch (Exception)
                {
                    
                    throw;
                }
                

                //enviar mail
                ViewBag.Vendedor = vendedor;
                return View("Transaccion", subasta);
            }
        }


        public ActionResult DetalleProducto(int idSubasta)
        {
            var user = Session["usuario"] as UsuarioSite;
            valor_tenant = user.Dominio;

            try
            {
                user_sitio = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
                valor_tenant = user_sitio.Dominio.ToString();

                Subasta subasta = subIBL.ObtenerSubasta(valor_tenant, idSubasta);
                if (subasta == null)
                {
                    return HttpNotFound();
                }

                sub_site = crearSubastaSite(subasta);
            }
            catch (Exception)
            {
                throw;
            }

            return View(sub_site);
        }

        [HttpPost]
        public JsonResult AgregarComentario(int idSubasta, string texto)
        {
            user_sitio = Session["usuario"] as UsuarioSite;
            valor_tenant = user_sitio.Dominio.ToString();
            int idLogueado = usuIBL.ObtenerIdByEmail(valor_tenant, user_sitio.Email);

            Comentario comentario = new Comentario();
            comentario.fecha = System.DateTime.Now;
            comentario.id_Subasta = idSubasta;
            comentario.id_Usuario = idLogueado;
            comentario.texto = texto;

            //comentario.Usuario = usuIBL.GetUsuario(valor_tenant, idLogueado);
            //comentario.Subasta = subIBL.ObtenerSubasta(valor_tenant, idSubasta);
            comIBL.AgregarComentario(valor_tenant,comentario);

            IEnumerable<Comentario> modelList = new List<Comentario>();
            List<Comentario> comentarios;
            comentarios = comIBL.ComentariosByProducto(valor_tenant, idSubasta);
            comentarios.Reverse();
            modelList = comentarios.Select(x =>
                                            new Comentario()
                                            {
                                                id_Subasta = x.id_Subasta,
                                                id_Usuario = x.id_Usuario,
                                                texto = x.texto,
                                                fecha = x.fecha
                                            });
            return Json(modelList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarComentario(int idSubasta)
        {
            user_sitio = Session["usuario"] as UsuarioSite;
            valor_tenant = user_sitio.Dominio.ToString();

            IEnumerable<Comentario> modelList = new List<Comentario>();
            List<Comentario> comentarios;
            comentarios = comIBL.ComentariosByProducto(valor_tenant, idSubasta);
            comentarios.Reverse();
            modelList = comentarios.Select(x =>
                                            new Comentario()
                                            {
                                                id_Subasta = x.id_Subasta,
                                                id_Usuario = x.id_Usuario,
                                                texto = x.texto,
                                                fecha = x.fecha
                                            });

            return Json(modelList, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult CambiarFavorito(int idSubasta)
        {
            user_sitio = Session["usuario"] as UsuarioSite;
            var idUsuario = usuIBL.ObtenerIdByEmail(user_sitio.Dominio, user_sitio.Email);
            valor_tenant = user_sitio.Dominio.ToString();

            var esfavorito = favIBL.esFavorito(valor_tenant, idSubasta, idUsuario);
            Boolean modelList;
            if (esfavorito)
            {
               favIBL.EliminarFavorito(valor_tenant, idSubasta, idUsuario);
                modelList = false;
            }
            else{
                Favorito favorito = new Favorito();
                favorito.id_Subasta = idSubasta;
                favorito.id_Usuario = idUsuario;

                favIBL.AgregarFavorito(valor_tenant, favorito);
                modelList = true;
            }

            return Json(modelList, JsonRequestBehavior.AllowGet);
        }
    }
}


                
                
                                                

