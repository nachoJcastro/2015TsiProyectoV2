﻿using System;
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
using Microsoft.Web.Administration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure;
using DNSManager;
using System.Text.RegularExpressions;

namespace Backend.Controllers
{   
    public class TiendaVirtualController : Controller
    {

        IBLTiendaVirtual _bl;
        BlobStorageServiceIIS _bss = new BlobStorageServiceIIS();
        //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

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
            ViewBag.numero = tiendas.Count();
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
                 var tiendanumber = _bl.ObtenerTiendaDelUsuario(idUser).Count();
                 
                 return View();
                 //return Content("<script language='javascript' type='text/javascript'>mensaje('Tienda Virtual', 'Solo se permite crear una tienda virtual por usuario', 'error');</script>"); }
                    
            }
            catch (Exception)
            {
                
                throw;
            }
            
            
            
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

                    var dominio_temp = tiendaVirtualDTO.Dominio.ToLower();
                    var dominio = "";
                    try
                    {
                        dominio = Regex.Replace(dominio_temp, @"\s+", "");
                        tiendaVirtualDTO.Dominio = dominio;
                    }
                    catch (Exception e)
                    {

                        throw e;
                    }

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

                    //Controlo que el dominio sea en minuscula
                   
                    

                   // tiendaVirtualDTO.Dominio = dominio;
                    tiendaVirtualDTO.Css = fileContents.ToString();
                    tiendaVirtualDTO.Fecha_creacion = System.DateTime.Now;
                    tiendaVirtualDTO.Estado = true;
                    tiendaVirtualDTO.StringConection = "StringConection";

                    _bl.AgregarTienda(tiendaVirtualDTO);
                    // IIS AGREGO SITIO
                    //127.0.0.1:80:sitio.chebay.com
                    //ServerManager iisManager = new ServerManager();
                    //iisManager.Sites.Add(dominio, "http", "127.0.0.1:80:"+dominio+".chebay.com", "C:\\inetpub\\wwwroot\\Site");
                    //iisManager.CommitChanges(); 


                    //ServerManager manager = new ServerManager();
                    try
                    {   
                        IHosts _hosts = new Hosts();

                        _hosts.AgregarSitio(dominio);
                        
                        // Add this site.
                        /*Site hrSite = manager.Sites.Add(name, "http", "*:80:", path);
                        // The site will need to be started manually.
                        hrSite.ServerAutoStart = false;
                        manager.CommitChanges();
                        Console.WriteLine("Site " + name + " added to ApplicationHost.config file.");*/
                    
                    
                            /*ServerManager serverMgr = new ServerManager();
                            string strWebsitename = dominio; // abc
                            string strApplicationPool = dominio.ToUpper();  // set your deafultpool :4.0 in IIS
                            string strhostname = dominio+".chebay.com"; //abc.com
                            string stripaddress = "127.0.0.1";// ip address
                            string bindinginfo = stripaddress + ":80:" + strhostname;*/
 
                            //check if website name already exists in IIS
                            //ServerManager serverMgr = new ServerManager();
                            //Site mySite = serverMgr.Sites.Add(dominio.ToUpper(), "C:\\inetpub\\wwwroot\\Site", 80);
                            //serverMgr.ApplicationPools.Add("DefaultAppPool");
                            //mySite.ApplicationDefaults.ApplicationPoolName = "DefaultAppPool";
                            //mySite.TraceFailedRequestsLogging.Enabled = true;
                            //mySite.TraceFailedRequestsLogging.Directory = "C:\\inetpub\\wwwroot\\site";
                           //serverMgr.CommitChanges();


                           /* Site mySite = serverMgr.Sites.Add(strWebsitename.ToString(), "http", bindinginfo, "C:\\inetpub\\wwwroot");
                            mySite.ApplicationDefaults.ApplicationPoolName = strApplicationPool;
                            mySite.TraceFailedRequestsLogging.Enabled = true;
                            mySite.TraceFailedRequestsLogging.Directory = "C:\\inetpub\\wwwroot\\Site";
                            serverMgr.CommitChanges();*/
                            //lblmsg.Text = "New website  " + strWebsitename + " added sucessfully";
                            //lblmsg.ForeColor = System.Drawing.Color.Green;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    // IIS AGREGO SITIO
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


                if (logo!=null)
                {
                    if(logo.ContentLength > 0){

                        //Elminar foto anterior
                        //TiendaVirtualDTO old = _bl.ObtenerTienda(tiendaVirtualDTO.TiendaVitualId);
                        //CloudBlockBlob blobOld = blobContainer.GetBlockBlobReference("Nombreblob");
                        //blobOld.Delete();


                        var nombreFoto = tiendaVirtualDTO.Dominio + Guid.NewGuid().ToString() + "_logo";
                        CloudBlockBlob blob = blobContainer.GetBlockBlobReference(nombreFoto);
                        blob.UploadFromStream(logo.InputStream);
                        tiendaVirtualDTO.Logo = blob.Uri.ToString();
                    }
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
        public ActionResult Upload(int id, string mensaje,string tipo)
        {
            TiendaVirtualDTO tienda = _bl.ObtenerTienda(id);
            //CloudBlobContainer blobContainer = _bss.GetContainerTienda(tienda.Dominio);
            //List<string> blobs = new List<string>();

            
            //foreach (var blob in blobContainer.ListBlobs())
            //{
            //    blobs.Add(blob.Uri.ToString());
            //}
            if (mensaje == null) mensaje = "";
            if (tipo == null) tipo = "";
            ViewBag.Imagenes = _bl.ObtenerImagenes(id);
            ViewBag.mensaje = mensaje;
            ViewBag.tipo = tipo;
            return View(tienda);
        }

        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public ActionResult Upload(TiendaVirtualDTO tienda, HttpPostedFileBase image)
        {
            TiendaVirtualDTO tiendaDTO = _bl.ObtenerTienda(tienda.TiendaVitualId);
            if (image.ContentLength < 2097152)
            {
                
                var nombreFoto = tienda.Dominio + Guid.NewGuid().ToString() + "_portada";
                CloudBlobContainer blobContainer = _bss.GetContainerTienda(tienda.Dominio);
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(nombreFoto);

                ImagenesDTO imagen = new ImagenesDTO();
                imagen.TiendaId = tiendaDTO.TiendaVitualId;
                imagen.Nombre = nombreFoto;
                imagen.UrlImagenMediana = blob.Uri.ToString();
                _bl.AgregarImagenTienda(imagen);


                blob.UploadFromStream(image.InputStream);

                return RedirectToAction("Upload", "TiendaVirtual", new { id = tiendaDTO.TiendaVitualId, mensaje = "Se agrego la imagen correctamente", tipo = "success" });
            }
            else
            {
                return RedirectToAction("Upload", "TiendaVirtual", new { id = tiendaDTO.TiendaVitualId, mensaje = "Tamaño maximo 2 MB", tipo="warning" });
               
            }
  
        }


        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public ActionResult DeleteImage(string Name, string Uri, int id)
        {
            TiendaVirtualDTO tienda = _bl.ObtenerTienda(id);

            Uri uri = new Uri(Uri);
            string filename = Path.GetFileName(uri.LocalPath);
            CloudBlobContainer blobContainer = _bss.GetContainerTienda(tienda.Dominio);
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);

            bool ok=_bl.EliminarImagenTienda(tienda.TiendaVitualId, Name);

            if (ok)
            {
                blob.Delete();
                ViewBag.mensaje = "Prueba";
                ViewBag.tipo = "Prueba";
                return Content("<script language='javascript' type='text/javascript'>mensaje('Tienda Virtual', 'Solo se permite crear una tienda virtual por usuario', 'error');</script>"); 
                    
            }
            else 
            {
                ViewBag.mensaje = "";
                ViewBag.tipo = "";
                return Content("<script language='javascript' type='text/javascript'>mensaje('Tienda Virtual', 'Solo se permite crear una tienda virtual por usuario', 'error');</script>");
            }

            
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
            List<SubastaAux> subastas;
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
                                                nickComprador = x.nombreComprador,
                                                nickVendedor = x.nombreVendedor,
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

        public ActionResult ChartsLineal()
        {
            ViewBag.tiendas = _bl.ObtenerTiendas().ToList();
            return View();
        }


        public ActionResult ReportesAjaxPrueba(Reporte rep)
        {
            List<ReporteLineal> subastas;
            List<ReporteLineal> usuarios;

            IEnumerable<ReporteLineal> modelList = new List<ReporteLineal>();
            IEnumerable<ReporteLineal> modelList2 = new List<ReporteLineal>();

            TipoReporte e = (TipoReporte)Enum.Parse(typeof(TipoReporte), "Usuario");


            if (rep.tipo.Equals(e))
            {
                usuarios = _bl.ReportUsersLineal(rep.dominio, rep.fechaini, rep.fechafin).ToList();
                modelList = usuarios.Select(x =>
                                            new ReporteLineal()
                                            {
                                                tipo = "Usuario",
                                                Fecha = x.Fecha,
                                                cantidad = x.cantidad

                                            });
                return Json(modelList, JsonRequestBehavior.AllowGet);


            }
            else
            {
                subastas = _bl.ReportSubastaLineal(rep.dominio, rep.fechaini, rep.fechafin).ToList();
                modelList2 = subastas.Select(x =>
                                            new ReporteLineal()
                                            {
                                                tipo = "Subasta",
                                                Fecha = x.Fecha,
                                                cantidad = x.cantidad

                                            });
                return Json(modelList2, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult GenerarChartLineal(Reporte rep)
        {
            List<ReporteLineal> subastas;
            List<ReporteLineal> usuarios;

            IEnumerable<ReporteLineal> modelList = new List<ReporteLineal>();
            IEnumerable<ReporteLineal> modelList2 = new List<ReporteLineal>();

            TipoReporte e = (TipoReporte)Enum.Parse(typeof(TipoReporte), "Usuario");
            //DateTime fechai = Convert.ToDateTime(fechaini);
            //DateTime fechaf = Convert.ToDateTime(fechafin);

            if (rep.tipo.Equals(e))
            {
                usuarios = _bl.ReportUsersLineal(rep.dominio, rep.fechaini, rep.fechafin).ToList();
                modelList = usuarios.Select(x =>
                                            new ReporteLineal()
                                            {
                                                
                                                Fecha = x.Fecha,
                                                cantidad = x.cantidad

                                            });
                return Json(modelList, JsonRequestBehavior.AllowGet);


            }
            else
            {
                subastas = _bl.ReportSubastaLineal(rep.dominio, rep.fechaini, rep.fechafin).ToList();
                modelList2 = subastas.Select(x =>
                                            new ReporteLineal()
                                            {
                                                
                                                Fecha = x.Fecha,
                                                cantidad = x.cantidad

                                            });
                return Json(modelList2, JsonRequestBehavior.AllowGet);
            }

            //int cant_u = _bl.ReportUsers(rep.dominio, rep.fechaini, rep.fechafin).Count();
            //int cant_t = _bl.ReportSubasta(rep.dominio, rep.fechaini, rep.fechafin).Count();

            //System.Diagnostics.Debug.WriteLine("Cantid usuarios" + cant_u.ToString());
            //System.Diagnostics.Debug.WriteLine("Cantid transacciones" + cant_t.ToString());

            //IEnumerable<Charts> modelList = new List<Charts> 
            //{ 
            //    new Charts()
            //    {
            //        name = "Usuarios",
            //        cantidad = cant_u
            //    },
            //    new Charts()
            //    {
            //        name = "Transacciones",
            //        cantidad = cant_t
            //    }
            
            //};

            //foreach (var item in modelList)
            //{
            //    System.Diagnostics.Debug.WriteLine("Json que retorno item :" + item.cantidad.ToString() + "--" + item.name.ToString());
            //}


            //return Json(modelList, JsonRequestBehavior.AllowGet);

        }

        [Authorize(Roles = "Admin")]
        public ActionResult ReportesSubastas()
        {

            // ViewData["Tiendas"] = _bl.ObtenerTiendas().ToList();



            ViewBag.tiendas = _bl.ObtenerTiendas().ToList();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ReportesUsuarios()
        {

            // ViewData["Tiendas"] = _bl.ObtenerTiendas().ToList();



            ViewBag.tiendas = _bl.ObtenerTiendas().ToList();
            return View();
        }

        public ActionResult ReportesDetalle(Reporte rep)
        {
            List<SubastaAux> subastas;
            List<UsuarioAux> usuarios;


            IEnumerable<UsuarioReporte> modelList = new List<UsuarioReporte>();
            IEnumerable<SubastaReporte> modelList2 = new List<SubastaReporte>();

            TipoReporte e = (TipoReporte)Enum.Parse(typeof(TipoReporte), "Usuario");
            //DateTime fechai = Convert.ToDateTime(fechaini);
            //DateTime fechaf = Convert.ToDateTime(fechafin);

            if (rep.tipo.Equals(e))
            {
                usuarios = _bl.DetUsers(rep.dominio, rep.fechaini).ToList();
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
                subastas = _bl.DetSub(rep.dominio, rep.fechaini).ToList();
                modelList2 = subastas.Select(x =>
                                            new SubastaReporte()
                                            {
                                                tipo = "Subasta",
                                                titulo = x.titulo,
                                                nickComprador = x.nombreComprador,
                                                nickVendedor = x.nombreVendedor,
                                                precio_Base = x.precio_Base,
                                                fecha_Inicio = x.fecha_Inicio

                                            });
                return Json(modelList2, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult ExisteDominio(string dominio)
        {
            string retorno;
            bool existe = _bl.ExisteDominio(dominio);
            if(existe) retorno="true";
            else retorno="false";

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
    }
}
