using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Backend.Models;
using Crosscutting.Entity;
using BusinessLogicLayer;
using BusinessLogicLayer.Controllers;
using System.IO;
using System.Security.AccessControl;
using Microsoft.AspNet.Identity;
using System.Web.Hosting;

namespace Backend.Controllers
{   
    public class TiendaVirtualController : Controller
    {

        IBLTiendaVirtual _bl;

        public TiendaVirtualController(IBLTiendaVirtual bl)
        {
            this._bl = bl;
        }

        public TiendaVirtualController() : this(new BLTiendaVirtual())
        {

        }

        // GET: TiendaVirtual
        [Authorize(Roles="Usuario")]
        public ActionResult Index()
        {
            var idUser = User.Identity.GetUserId();
            var tiendas = _bl.ObtenerTiendaDelUsuario(idUser);
            return View(tiendas);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminUser()
        {
            var tiendas = _bl.ObtenerTiendas();
            return View(tiendas);
        }

        // GET: TiendaVirtual/Details/5
        [Authorize(Roles = "Usuario")]
        public ActionResult Details(int id)
        {

            TiendaVirtualDTO tiendaVirtualDTO = _bl.ObtenerTienda(id);
            if (tiendaVirtualDTO == null)
            {
                return HttpNotFound();
            }
            return View(tiendaVirtualDTO);
        }

        // GET: TiendaVirtual/Create
        [Authorize(Roles = "Usuario")]
        public ActionResult Create()
        {
            try
            {
                 var idUser = User.Identity.GetUserId();
                 var tienda= _bl.ObtenerTiendaDelUsuario(User.Identity.GetUserName());

                    
            }
            catch (Exception)
            {
                
                throw;
            }
            
            
            return View();
        }

        // POST: TiendaVirtual/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Usuario")]
        public ActionResult Create([Bind(Include = "TiendaVitualId,Nombre,Dominio,Descripcion,Logo,Fecha_creacion,Estado,Css,StringConection")] TiendaVirtualDTO tiendaVirtualDTO, HttpPostedFileBase logo)
        {
            tiendaVirtualDTO.UsuarioId = User.Identity.GetUserId();

            var num = _bl.ObtenerTiendaDelUsuario(tiendaVirtualDTO.UsuarioId).Count();
            if (num < 1)
            {
            
                if (ModelState.IsValid)
                {
                    //string strMappath = "~/imagenes/" + tiendaVirtualDTO.Nombre;
                    string strMappath = "~/imagenes/";
                    try
                    {
                        
                        if (logo != null)
                        {
                            //var nombreFoto = juego.Nombre + "_" + Guid.NewGuid().ToString() + "_" + Path.GetFileName(foto.FileName);
                            var nombreFoto = tiendaVirtualDTO.Nombre + Guid.NewGuid().ToString() + Path.GetExtension(logo.FileName);
                            var rutaFoto = Path.Combine(Server.MapPath(strMappath), nombreFoto);
                            logo.SaveAs(rutaFoto);
                            tiendaVirtualDTO.Logo = strMappath + nombreFoto;
                        }
                        else
                        {
                            tiendaVirtualDTO.Logo = "~/Imagenes/tiendadefault.png";
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.ToString());
                    }

                    var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/Content/Site.css"));

                    tiendaVirtualDTO.Css = fileContents.ToString();
                    tiendaVirtualDTO.Fecha_creacion = System.DateTime.Now;
                    tiendaVirtualDTO.Estado = true;
                    tiendaVirtualDTO.StringConection = "StringConection";

                    _bl.AgregarTienda(tiendaVirtualDTO);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(tiendaVirtualDTO);
        }

        // GET: TiendaVirtual/Edit/5
        [Authorize(Roles = "Usuario")]
        public ActionResult Edit(int id)
        {

            TiendaVirtualDTO tiendaVirtualDTO = _bl.ObtenerTienda(id);
            if (tiendaVirtualDTO == null)
            {
                return HttpNotFound();
            }
            return View(tiendaVirtualDTO);
        }

        // POST: TiendaVirtual/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Usuario")]
        public ActionResult Edit([Bind(Include = "TiendaVitualId,Nombre,Dominio,Descripcion,Logo,Fecha_creacion,Estado,Css,StringConection")] TiendaVirtualDTO tiendaVirtualDTO, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {

                string strMappath = "~/imagenes/";

                // guardar imagen
                if (logo != null)
                {
                    //var nombreFoto = juego.Nombre + "_" + Guid.NewGuid().ToString() + "_" + Path.GetFileName(foto.FileName);
                    var nombreFoto = tiendaVirtualDTO.Nombre + Guid.NewGuid().ToString() + Path.GetExtension(logo.FileName);
                    var rutaFoto = Path.Combine(Server.MapPath(strMappath), nombreFoto);
                    logo.SaveAs(rutaFoto);
                    tiendaVirtualDTO.Logo = strMappath + nombreFoto;
                }
                else
                {
                    tiendaVirtualDTO.Logo = "~/Imagenes/tiendadefault.png";
                }

                _bl.ActualizarTiendas(tiendaVirtualDTO);
                return RedirectToAction("Index");
            }
            return View(tiendaVirtualDTO);
        }

        // GET: TiendaVirtual/Delete/5
        [Authorize(Roles = "Usuario")]
        public ActionResult Delete(int id)
        {

            TiendaVirtualDTO tiendaVirtualDTO = _bl.ObtenerTienda(id);
            if (tiendaVirtualDTO == null)
            {
                return HttpNotFound();
            }
            return View(tiendaVirtualDTO);
        }

        // POST: TiendaVirtual/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Usuario")]
        public ActionResult DeleteConfirmed(int id)
        {

            _bl.EliminarTienda(id);

            return RedirectToAction("Index");
        }

        // GET: TiendaVirtual/Estilo
       [Authorize(Roles = "Usuario")]
        public ActionResult Estilo(int id)
        {
            
            try
            {
                System.Diagnostics.Debug.WriteLine("ID tienda :" + id);
                var tienda = _bl.ObtenerTienda(id);
                var model = new Estilo();
                model.idTienda = id;
                model.texto = tienda.Css;
                return View(model);
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Usuario")]
        public ActionResult Estilo([Bind(Include = "texto,idTienda")] Estilo css)
        {
            if (ModelState.IsValid)
            {
                 //CREAR CSS EN DIRECTORIO
                try
                {
                    System.Diagnostics.Debug.WriteLine("ID tienda :" + css.idTienda);
                    System.Diagnostics.Debug.WriteLine("Ruta :" + css.texto);
                    
                    //string ruta = tienda.Nombre+ ".css";
                    _bl.EditarCss(css.idTienda, css.texto);
                    return RedirectToAction("Index", "TiendaVirtual");
                }
                catch (Exception)
                {
                    
                    throw;
                }
               
            }

            return View(css);
        }



    }
}
