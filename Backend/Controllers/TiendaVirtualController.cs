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

namespace Backend.Controllers
{
    public class TiendaVirtualController : Controller
    {
        //IBLTiendaVirtual _bl = new BLTiendaVirtual();
        IBLTiendaVirtual _bl;

        public TiendaVirtualController(IBLTiendaVirtual bl)
        {
            this._bl = bl;
        }

        public TiendaVirtualController() : this(new BLTiendaVirtual())
        {

        }

       //var idDesarrollador = User.Identity.GetUserId();

        // GET: TiendaVirtual
        [Authorize]
        public ActionResult Index()
        {
            var idUser = User.Identity.GetUserId();
            var tiendas = _bl.ObtenerTiendaDelUsuario(idUser);
            return View(tiendas);
        }

        // GET: TiendaVirtual/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            TiendaVirtualDTO tiendaVirtualDTO = _bl.ObtenerTienda(id);
            if (tiendaVirtualDTO == null)
            {
                return HttpNotFound();
            }
            return View(tiendaVirtualDTO);
        }

        // GET: TiendaVirtual/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiendaVirtual/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
                        //if (!Directory.Exists(strMappath))
                        //{
                        //    DirectoryInfo di = Directory.CreateDirectory(strMappath);

                        //    DirectorySecurity dSecurity = di.GetAccessControl();

                        //    // Add the FileSystemAccessRule to the security settings. 
                        //    dSecurity.AddAccessRule(new FileSystemAccessRule(@"DomainName\AccountName", FileSystemRights.ReadData, AccessControlType.Allow));

                        //    // Set the new access settings.
                        //    di.SetAccessControl(dSecurity);
                        //}

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
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.ToString());
                    }

               
                    tiendaVirtualDTO.Fecha_creacion = System.DateTime.Now;
                    tiendaVirtualDTO.Estado = true;
                    tiendaVirtualDTO.StringConection = "StringConection";

                    _bl.AgregarTienda(tiendaVirtualDTO);
                    return RedirectToAction("Index");
                }
            }

            return View(tiendaVirtualDTO);
        }

        // GET: TiendaVirtual/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
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
        [Authorize]
        public ActionResult Edit([Bind(Include = "TiendaVitualId,Nombre,Dominio,Descripcion,Logo,Fecha_creacion,Estado,Css,StringConection")] TiendaVirtualDTO tiendaVirtualDTO, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {
                //string strMappath = "~/imagenes/" + tiendaVirtualDTO.Nombre;
                string strMappath = "~/imagenes/";

                //if (!Directory.Exists(strMappath))
                //{
                //    DirectoryInfo di = Directory.CreateDirectory(strMappath);
                //}

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
        [Authorize]
        public ActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
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
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            //TiendaVirtualDTO tiendaVirtualDTO = _bl.ObtenerTienda(id);
            _bl.EliminarTienda(id);
            //await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
