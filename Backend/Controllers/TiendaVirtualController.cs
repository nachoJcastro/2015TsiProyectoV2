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
using Microsoft.WindowsAzure.Storage.Blob;
using System.Drawing;
using System.Collections;
using Crosscutting.EntityTenant;
using Crosscutting.Enum;

namespace Backend.Controllers
{   
    public class TiendaVirtualController : Controller
    {

        IBLTiendaVirtual _bl;
        BlobStorageService _bss = new BlobStorageService();

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
                    CloudBlobContainer blobContainer = _bss.GetContainerTienda(tiendaVirtualDTO.Dominio);
                    if (logo != null)
                    {
                        if (logo.ContentLength > 0)
                        {
                            var nombreFoto = tiendaVirtualDTO.Dominio + Guid.NewGuid().ToString() + "_logo";
                            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(nombreFoto);
                            blob.UploadFromStream(logo.InputStream);
                            tiendaVirtualDTO.Logo = blob.Uri.ToString();

                        }
                    }
                    else
                    {
                        var path = Server.MapPath(@"~/Imagenes/tiendadefault.png");
                        var nombreFoto = tiendaVirtualDTO.Dominio + Guid.NewGuid().ToString() + "_logo";
                        //byte[] imgDefault = System.IO.File.ReadAllBytes(path);
                        FileStream fs = new FileStream(path, FileMode.Create);
                        CloudBlockBlob blob = blobContainer.GetBlockBlobReference(nombreFoto);
                        blob.UploadFromStream(fs);
                        tiendaVirtualDTO.Logo = blob.Uri.ToString();
                    }


                    ////string strMappath = "~/imagenes/" + tiendaVirtualDTO.Nombre;
                    //string strMappath = "~/imagenes/";
                    //try
                    //{
                        
                    //    if (logo != null)
                    //    {
                    //        //var nombreFoto = juego.Nombre + "_" + Guid.NewGuid().ToString() + "_" + Path.GetFileName(foto.FileName);
                    //        var nombreFoto = tiendaVirtualDTO.Nombre + Guid.NewGuid().ToString() + Path.GetExtension(logo.FileName);
                    //        var rutaFoto = Path.Combine(Server.MapPath(strMappath), nombreFoto);
                    //        logo.SaveAs(rutaFoto);
                    //        tiendaVirtualDTO.Logo = strMappath + nombreFoto;
                    //    }
                    //    else
                    //    {
                    //        tiendaVirtualDTO.Logo = "~/Imagenes/tiendadefault.png";
                    //    }
                    //}
                    //catch (Exception e)
                    //{
                    //    Console.WriteLine("The process failed: {0}", e.ToString());
                    //}

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
                CloudBlobContainer blobContainer = _bss.GetContainerTienda(tiendaVirtualDTO.Dominio);


                if (logo.ContentLength > 0)
                {

                    //Elminar foto anterior
                    //TiendaVirtualDTO old = _bl.ObtenerTienda(tiendaVirtualDTO.TiendaVitualId);
                    //CloudBlockBlob blobOld = blobContainer.GetBlockBlobReference("Nombreblob");
                    //blobOld.Delete();


                    var nombreFoto = tiendaVirtualDTO.Dominio + Guid.NewGuid().ToString() + "_logo";
                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(nombreFoto);
                    blob.UploadFromStream(logo.InputStream);
                    tiendaVirtualDTO.Logo = blob.Uri.ToString();

                }
                else
                {
                    TiendaVirtualDTO old = _bl.ObtenerTienda(tiendaVirtualDTO.TiendaVitualId);

                    tiendaVirtualDTO.Logo = old.Logo;
                }


                //string strMappath = "~/imagenes/";

                //// guardar imagen
                //if (logo != null)
                //{
                //    //var nombreFoto = juego.Nombre + "_" + Guid.NewGuid().ToString() + "_" + Path.GetFileName(foto.FileName);
                //    var nombreFoto = tiendaVirtualDTO.Nombre + Guid.NewGuid().ToString() + Path.GetExtension(logo.FileName);
                //    var rutaFoto = Path.Combine(Server.MapPath(strMappath), nombreFoto);
                //    logo.SaveAs(rutaFoto);
                //    tiendaVirtualDTO.Logo = strMappath + nombreFoto;
                //}
                //else
                //{
                //    tiendaVirtualDTO.Logo = "~/Imagenes/tiendadefault.png";
                //}

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


        [Authorize(Roles = "Usuario")]
        public ActionResult Upload(int id)
        {
            TiendaVirtualDTO tienda = _bl.ObtenerTienda(id);
            //CloudBlobContainer blobContainer = _bss.GetContainerTienda(tienda.Dominio);
            //List<string> blobs = new List<string>();

            
            //foreach (var blob in blobContainer.ListBlobs())
            //{
            //    blobs.Add(blob.Uri.ToString());
            //}

            return View(tienda);
        }

        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public ActionResult Upload(TiendaVirtualDTO tienda, HttpPostedFileBase image)
        {

                TiendaVirtualDTO tiendaDTO = _bl.ObtenerTienda(tienda.TiendaVitualId);
                var nombreFoto = tienda.Dominio + Guid.NewGuid().ToString() + "_portada";
                CloudBlobContainer blobContainer = _bss.GetContainerTienda(tienda.Dominio);
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(nombreFoto);
                
                ImagenesDTO imagen = new ImagenesDTO();
                imagen.TiendaId = tiendaDTO.TiendaVitualId;
                imagen.Nombre = nombreFoto;
                imagen.UrlImagenMediana = blob.Uri.ToString();
                _bl.AgregarImagenTienda(imagen);
                

                blob.UploadFromStream(image.InputStream);

                return RedirectToAction("Upload", "TiendaVirtual", new { id = tiendaDTO.TiendaVitualId });
        }


        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public string DeleteImage(string Name, string Uri, int id)
        {
            TiendaVirtualDTO tienda = _bl.ObtenerTienda(id);

            Uri uri = new Uri(Uri);
            string filename = Path.GetFileName(uri.LocalPath);
            CloudBlobContainer blobContainer = _bss.GetContainerTienda(tienda.Dominio);
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);

            _bl.EliminarImagenTienda(tienda.TiendaVitualId, Name);

            blob.Delete();

            return "File delete";
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Reportes() {

           // ViewData["Tiendas"] = _bl.ObtenerTiendas().ToList();



            ViewBag.tiendas = _bl.ObtenerTiendas().ToList();
            return View();
        }

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public ActionResult Reportes(Reporte rep)
        //{
        //    List<Subasta> subastas;
        //    List<Usuario> usuarios;
        //    TipoReporte e = (TipoReporte)Enum.Parse(typeof(TipoReporte), "Usuario");

        //    if (rep.tipo.Equals(e))
        //    {
        //        usuarios = _bl.ReportUsers(rep.dominio, rep.fechaini, rep.fechafin);
        //        //return Json(new { Data = usuarios }, JsonRequestBehavior.AllowGet)
        //        ViewBag.listaUser = usuarios;
                
        //    }
        //    else {
        //        subastas = _bl.ReportSubasta(rep.dominio, rep.fechaini, rep.fechafin);
        //        ViewBag.listaSub = subastas;
                

        //        //return Json(new { Data = subastas }, JsonRequestBehavior.AllowGet)
        //    }
        //    return RedirectToAction("Reportes", "TiendaVirtual");
        //}


        public ActionResult ReportesAjax(Reporte rep)
        {
            List<Subasta> subastas;
            List<Usuario> usuarios;

            IEnumerable<UsuarioReporte> modelList = new List<UsuarioReporte>();
            IEnumerable<SubastaReporte> modelList2 = new List<SubastaReporte>();

            TipoReporte e = (TipoReporte)Enum.Parse(typeof(TipoReporte), "Usuario");
            //DateTime fechai = Convert.ToDateTime(fechaini);
            //DateTime fechaf = Convert.ToDateTime(fechafin);

            if (rep.tipo.Equals(e))
            {
                usuarios = _bl.ReportUsers(rep.dominio, rep.fechaini, rep.fechafin).ToList();
                modelList = usuarios.Select(x =>
                                            new UsuarioReporte()
                                            {
                                                tipo = "Usuario",
                                                nick = x.nick,
                                                nombre = x.nombre,
                                                apellido = x.apellido,
                                                email = x.email,
                                                fecha_Alta = x.fecha_Alta
                                                
                                            });
                return Json(modelList, JsonRequestBehavior.AllowGet);
                

            }
            else
            {
                subastas = _bl.ReportSubasta(rep.dominio, rep.fechaini, rep.fechafin).ToList();
                modelList2 = subastas.Select(x =>
                                            new SubastaReporte()
                                            {
                                                tipo = "Subasta",
                                                titulo = x.titulo,
                                                precio_Base = x.precio_Base,
                                                fecha_Inicio = x.fecha_Inicio

                                            });
                return Json(modelList2, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult Charts() {
            ViewBag.tiendas = _bl.ObtenerTiendas().ToList();
            return View();
        }

        public ActionResult GenerarChart(Reporte rep) {

            int cant_u = _bl.ReportUsers(rep.dominio, rep.fechaini, rep.fechafin).Count();
            int cant_t = _bl.ReportSubasta(rep.dominio, rep.fechaini, rep.fechafin).Count();

            System.Diagnostics.Debug.WriteLine("Cantid usuarios" + cant_u.ToString());
            System.Diagnostics.Debug.WriteLine("Cantid transacciones" + cant_t.ToString());

            IEnumerable<Charts> modelList = new List<Charts> 
            { 
                new Charts()
                {
                    name = "Usuarios",
                    cantidad = cant_u
                },
                new Charts()
                {
                    name = "Transacciones",
                    cantidad = cant_t
                }
            
            };

            foreach (var item in modelList)
            {
                System.Diagnostics.Debug.WriteLine("Json que retorno item :" + item.cantidad.ToString() +"--" +item.name.ToString());   
            }

           
            return Json(modelList, JsonRequestBehavior.AllowGet);
  
        }

    }
}
