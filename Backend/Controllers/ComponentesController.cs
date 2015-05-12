using Backend.Models;
using BusinessLogicLayer.Controllers;
using BusinessLogicLayer.Interfaces;
using Crosscutting.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Backend.Controllers
{
    public class ComponentesController : Controller
    {
        //IBLCategoria _bl = new BLCategoria();
        IBLCategoria _blcategoria;
        IBLAtributo _blatributo;
        IBLTipoProducto _bltipo;

        public ComponentesController(IBLCategoria bl, IBLAtributo bla, IBLTipoProducto blt)
        {
            this._blcategoria = bl;
            this._blatributo = bla;
            this._bltipo = blt;
        }

        public ComponentesController()
            : this(new BLCategoria(), new BLAtributo(), new BLTipoProducto())
        {

        }


        // GET: Componentes
        public ActionResult Index(int id)
        {
            var listaCat = _blcategoria.ObtenerCategoriasPorTienda(id);
            ViewBag.ListaCategorias = listaCat;

            var listaT = new List<TipoProductoDTO>();
            var listaA = new List<AtributosDTO>();

            foreach (var cat in listaCat)
            {
                foreach (var atributo in _blatributo.ObtenerAtributosPorCategoria(cat.CategoriaId))
                {
                    listaA.Add(atributo);
                }

                foreach (var tipo in _bltipo.ObtenerTipoPorCategoria(cat.CategoriaId))
                {
                    listaT.Add(tipo);
                }
            }

            ViewBag.ListaAtri = listaA;
            ViewBag.ListaTipos = listaT;

            this.Session["_tiendaSesion"] = id;
            ViewBag.idT = this.Session["_tiendaSesion"];

            return View();
        }

        /*-------------------------------------CATEGORIAS-------------------------------------*/
        #region Categorias

        // GET: Categorias/Create
        public ActionResult CreateCategoria()
        {
            ViewBag.idTienda = this.Session["_tiendaSesion"];
            return View();
        }

        // POST: Categorias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategoria([Bind(Include = "CategoriaId,TiendaId,Nombre")] CategoriasDTO categoriasDTO)
        {
            if (ModelState.IsValid)
            {
                categoriasDTO.TiendaId = Int32.Parse(this.Session["_tiendaSesion"].ToString());
                _blcategoria.AgregarCategoria(categoriasDTO);
                return RedirectToAction("Index", new { id = this.Session["_tiendaSesion"] });
            }

            return View(categoriasDTO);
        }

        // GET: Categorias/Edit/5
        public ActionResult EditCategoria(int id)
        {
            CategoriasDTO categoriasDTO = _blcategoria.ObtenerCategoria(id);
            if (categoriasDTO == null)
            {
                return HttpNotFound();
            }
            return View(categoriasDTO);
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategoria([Bind(Include = "CategoriaId,TiendaId,Nombre")] CategoriasDTO categoriasDTO)
        {
            if (ModelState.IsValid)
            {
                _blcategoria.ActualizarCategoria(categoriasDTO);
                return RedirectToAction("Index", new { id = this.Session["_tiendaSesion"] });
            }
            return View(categoriasDTO);
        }

        // GET: Categorias/Delete/5
        public ActionResult DeleteCategoria(int id)
        {
            CategoriasDTO categoriasDTO = _blcategoria.ObtenerCategoria(id);
            if (categoriasDTO == null)
            {
                return HttpNotFound();
            }
            return View(categoriasDTO);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("DeleteCategoria")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedCategoria(int id)
        {
            _blcategoria.EliminarCategoria(id);
            return RedirectToAction("Index", new { id = this.Session["_tiendaSesion"] });
        }

        #endregion
        /*-------------------------------------/CATEGORIAS-------------------------------------*/

        ///*-------------------------------------IMAGENES-------------------------------------*/
        //        #region Imagenes

        //        // GET: Imagenes/Create
        //        public ActionResult Create()
        //        {
        //            return View();
        //        }

        //        // POST: Imagenes/Create
        //        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        //        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult CreateImagen([Bind(Include = "ImageneId,TiendaId,UrlImagenMediana,EsPortada,ImagenEliminada")] ImagenesDTO imagenesDTO)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                db.ImagenesDTOes.Add(imagenesDTO);
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }

        //            return View(imagenesDTO);
        //        }

        //        // GET: Imagenes/Edit/5
        //        public ActionResult EditImagen(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            ImagenesDTO imagenesDTO = db.ImagenesDTOes.Find(id);
        //            if (imagenesDTO == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(imagenesDTO);
        //        }

        //        // POST: Imagenes/Edit/5
        //        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        //        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult EditImagen([Bind(Include = "ImageneId,TiendaId,UrlImagenMediana,EsPortada,ImagenEliminada")] ImagenesDTO imagenesDTO)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                db.Entry(imagenesDTO).State = EntityState.Modified;
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }
        //            return View(imagenesDTO);
        //        }

        //        // GET: Imagenes/Delete/5
        //        public ActionResult DeleteImagen(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            ImagenesDTO imagenesDTO = db.ImagenesDTOes.Find(id);
        //            if (imagenesDTO == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(imagenesDTO);
        //        }

        //        // POST: Imagenes/Delete/5
        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult DeleteConfirmedImagen(int id)
        //        {
        //            ImagenesDTO imagenesDTO = db.ImagenesDTOes.Find(id);
        //            db.ImagenesDTOes.Remove(imagenesDTO);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        #endregion
        ///*-------------------------------------/IMAGENES-------------------------------------*/

        ///*-------------------------------------ATRIBUTOS-------------------------------------*/
        #region Atributos

        // GET: Atributos/Create
        public ActionResult CreateAtributo()
        {
            ViewBag.CategoriaId = new SelectList(_blcategoria.ObtenerCategorias(), "CategoriaId", "Nombre");
            return View();
        }

        // POST: Atributos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAtributo([Bind(Include = "AtributoId,CategoriaId,Nombre")] AtributosDTO atributosDTO)
        {
            if (ModelState.IsValid)
            {
                _blatributo.AgregarAtributo(atributosDTO);
                return RedirectToAction("Index", new { id = this.Session["_tiendaSesion"] });
            }

            ViewBag.CategoriaId = new SelectList(_blcategoria.ObtenerCategorias(), "CategoriaId", "Nombre", atributosDTO.CategoriaId);
            return View(atributosDTO);
        }

        // GET: Atributos/Edit/5
        public ActionResult EditAtributo(int id)
        {
            AtributosDTO atributosDTO = _blatributo.ObtenerAtributo(id);
            if (atributosDTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(_blcategoria.ObtenerCategorias(), "CategoriaId", "Nombre", atributosDTO.CategoriaId);
            return View(atributosDTO);
        }

        // POST: Atributos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAtributo([Bind(Include = "AtributoId,CategoriaId,Nombre")] AtributosDTO atributosDTO)
        {
            if (ModelState.IsValid)
            {
                _blatributo.ActualizarAtributo(atributosDTO);
                return RedirectToAction("Index", new { id = this.Session["_tiendaSesion"] });
            }
            ViewBag.CategoriaId = new SelectList(_blcategoria.ObtenerCategorias(), "CategoriaId", "Nombre", atributosDTO.CategoriaId);
            return View(atributosDTO);
        }

        // GET: Atributos/Delete/5
        public ActionResult DeleteAtributo(int id)
        {
            AtributosDTO atributosDTO = _blatributo.ObtenerAtributo(id);
            if (atributosDTO == null)
            {
                return HttpNotFound();
            }
            return View(atributosDTO);
        }

        // POST: Atributos/Delete/5
        [HttpPost, ActionName("DeleteAtributo")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedAtributo(int id)
        {
            _blatributo.EliminarAtributo(id);
            return RedirectToAction("Index", new { id = this.Session["_tiendaSesion"] });
        }

        #endregion
        ///*-------------------------------------/ATRIBUTOS-------------------------------------*/

        ///*-------------------------------------TIPOPRODUCTOS-------------------------------------*/
        #region TipoProductos

        // GET: TipoProducto/Create
        public ActionResult CreateTipoProducto()
        {
            ViewBag.CategoriaId = new SelectList(_blcategoria.ObtenerCategorias(), "CategoriaId", "Nombre");
            return View();
        }

        // POST: TipoProducto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTipoProducto([Bind(Include = "TipoProductoId,CategoriaId,Titulo,Descripcion")] TipoProductoDTO tipoProductoDTO)
        {
            if (ModelState.IsValid)
            {
                _bltipo.AgregarTipoProducto(tipoProductoDTO);
                return RedirectToAction("Index", new { id = this.Session["_tiendaSesion"] });
            }

            ViewBag.CategoriaId = new SelectList(_blcategoria.ObtenerCategorias(), "CategoriaId", "Nombre", tipoProductoDTO.CategoriaId);
            return View(tipoProductoDTO);
        }

        // GET: TipoProducto/Edit/5
        public ActionResult EditTipoProducto(int id)
        {
            TipoProductoDTO tipoProductoDTO = _bltipo.ObtenerTipoProducto(id);
            if (tipoProductoDTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(_blcategoria.ObtenerCategorias(), "CategoriaId", "Nombre", tipoProductoDTO.CategoriaId);
            return View(tipoProductoDTO);
        }

        // POST: TipoProducto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTipoProducto([Bind(Include = "TipoProductoId,CategoriaId,Titulo,Descripcion")] TipoProductoDTO tipoProductoDTO)
        {
            if (ModelState.IsValid)
            {
                _bltipo.ActualizarTipoProducto(tipoProductoDTO);
                return RedirectToAction("Index", new { id = this.Session["_tiendaSesion"] });
            }
            ViewBag.CategoriaId = new SelectList(_blcategoria.ObtenerCategorias(), "CategoriaId", "Nombre", tipoProductoDTO.CategoriaId);
            return View(tipoProductoDTO);
        }

        // GET: TipoProducto/Delete/5
        public ActionResult DeleteTipoProducto(int id)
        {

            TipoProductoDTO tipoProductoDTO = _bltipo.ObtenerTipoProducto(id);
            if (tipoProductoDTO == null)
            {
                return HttpNotFound();
            }
            return View(tipoProductoDTO);
        }

        // POST: TipoProducto/Delete/5
        [HttpPost, ActionName("DeleteTipoProducto")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedTipoProducto(int id)
        {
            _bltipo.EliminarTipoProducto(id);
            return RedirectToAction("Index", new { id = this.Session["_tiendaSesion"] });
        }

        #endregion
        ///*-------------------------------------/TIPOPRODUCTOS-------------------------------------*/


    }
}