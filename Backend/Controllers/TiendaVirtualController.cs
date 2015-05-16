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
using System.Diagnostics;
using System.Text;

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
                    String rutaCarpetafull = @"C:\contenidoSite\img\";
                    String rutaCarpetafullcss = @"C:\contenidoSite\css\";

                    try
                    {
                        //Si no existe el directorio se crea
                        if (!Directory.Exists(rutaCarpetafull))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(rutaCarpetafull);

                        }

                        if (!Directory.Exists(rutaCarpetafullcss))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(rutaCarpetafullcss);

                        }

                        // guardar imagen
                        if (logo != null)
                        {
                            //var nombreFoto = juego.Nombre + "_" + Guid.NewGuid().ToString() + "_" + Path.GetFileName(foto.FileName);
                            var nombreFoto = tiendaVirtualDTO.Nombre + Guid.NewGuid().ToString() + Path.GetExtension(logo.FileName);
                            var rutaFoto = Path.Combine(rutaCarpetafull, nombreFoto);
                            logo.SaveAs(rutaFoto);
                            tiendaVirtualDTO.Logo = rutaCarpetafull + nombreFoto;
                        }
                        else
                        {
                            tiendaVirtualDTO.Logo = @"C:\contenidoSite\tiendadefault.png";
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.ToString());
                    }
                    

                    tiendaVirtualDTO.Css = @"C:\contenidoSite\Site.css";
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
                 String rutaCarpetafull = @"C:\contenidoSite\img\";
                  
                //Si no existe el directorio se crea
                if (!Directory.Exists(rutaCarpetafull))
                {
                    DirectoryInfo di = Directory.CreateDirectory(rutaCarpetafull);

                }
                        

                // guardar imagen
                if (logo != null)
                {
                    //var nombreFoto = juego.Nombre + "_" + Guid.NewGuid().ToString() + "_" + Path.GetFileName(foto.FileName);
                    var nombreFoto = tiendaVirtualDTO.Nombre + Guid.NewGuid().ToString() + Path.GetExtension(logo.FileName);
                    var rutaFoto = Path.Combine(rutaCarpetafull, nombreFoto);
                    logo.SaveAs(rutaFoto);
                    tiendaVirtualDTO.Logo = rutaCarpetafull + nombreFoto;
                }
                else
                {
                    //tiendaVirtualDTO.Logo = "~/Imagenes/tiendadefault.png";
                    tiendaVirtualDTO.Logo = @"C:\contenidoSite\tiendadefault.png";
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

        // GET: TiendaVirtual/Estilo
       [Authorize]
        public ActionResult Estilo(int id)
        {

            this.Session["_tvid"] = id;
            ViewBag.TiendaV = this.Session["_tvid"];
            
            return View();
        }

       [HttpPost]
       [Authorize]
       public ActionResult Estilo(Estilo css, HttpPostedFileBase texto)
       {
           if (ModelState.IsValid)
           {
               var idTienda = int.Parse(this.Session["_tvid"].ToString());
               TiendaVirtualDTO tienda = _bl.ObtenerTienda(idTienda);

               String rutaCarpetafull = @"C:\contenidoSite\css\";
               //Si no existe el directorio se crea
               if (!Directory.Exists(rutaCarpetafull))
               {
                   DirectoryInfo di = Directory.CreateDirectory(rutaCarpetafull);
                   
               }


               if (texto != null)
               {
                   var nombre = tienda.Nombre + Path.GetFileName(texto.FileName);
                   var ruta = Path.Combine(rutaCarpetafull,nombre);
                   
                   //Se crea el archivo css
                   texto.SaveAs(ruta);

                   //Se guarda en la base
                   _bl.EditarCss(idTienda, ruta);
               }


               return RedirectToAction("Index");
           }


           return RedirectToAction("Index", "Home");
       }

        [Authorize]
        public FileContentResult CssDownload(int id)
        {
            var idTienda = int.Parse(this.Session["_tvid"].ToString());
            TiendaVirtualDTO tienda = _bl.ObtenerTienda(idTienda);
            var contentType = "text/css";

            if (System.IO.File.Exists(tienda.Css))
            {
                String dir = tienda.Css;
                using (StreamReader sr = new StreamReader(dir))
                {
                    String line = sr.ReadToEnd();
                    var bytes = Encoding.UTF8.GetBytes(line);
                    var result = new FileContentResult(bytes, contentType);
                    result.FileDownloadName = "Site.css";
                    return result;
                }
            }
            else
            {

                var content = "/*SOLO MODIFICAR EL CSS DENTRO DE LOS ID Y CLASES*/"
                                + "/*NO MODIFICAR LOS NOMBRES DE LAS CLASES*/"
                                + "/*NO MODIFICAR LOS NOMBRES DE LOS ID*/"
                                + "body { background-color : #FFF; }"
                                + "h1,h2 { color: red; }"
                                + "p { color:blue; }";
                var bytes = Encoding.UTF8.GetBytes(content);
                var result = new FileContentResult(bytes, contentType);
                result.FileDownloadName = "Site.css";
                return result;
            }
            
        }


    }
}
