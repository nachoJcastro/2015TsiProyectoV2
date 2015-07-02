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
using PagedList;

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

        BlobStorageIIS _bls = new BlobStorageIIS();
        //IBLCategoria catIBL;
        //IBLAtributo atrIBL;
        public UsuarioSite user_sitio;
        private string valor_tenant;
        private SubastaSite sub_site;
        private int id_sub;


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
                sub_site.fecha_Inicio = DateTime.Now;
                sub_site.fecha_Cierre = (DateTime)subasta.fecha_Cierre;
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

                if (user_sitio.Email == null) return RedirectToAction("Login", "Account");

                valor_tenant = user_sitio.Dominio.ToString();
                //List<string> tipo = new List<string>();
                //tipo.Add("Subasta");
                //tipo.Add("Compra Directa");
                //ViewData["Tipo"] = tipo;
                var  lista_Origen= proIBL.ObtenerCategoriasPorTienda(user_sitio.idTienda);
                var lista_item_Cat = new List<SelectListItem>();
                lista_item_Cat.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "Seleccione una Categoria",
                    Selected = true 
                });
                foreach (var item in lista_Origen)
                {
                   lista_item_Cat.Add( new SelectListItem()
                    {
                         Value = item.CategoriaId.ToString(),
                         Text = item.Nombre,
                         Selected = false 
                    });
                }    
                ViewData["Categorias"] = lista_item_Cat;
               
                // ViewData["Productos"] = proIBL.ObtenerProductos();
                //  ViewData["Atributos"] = atrIBL.ObtenerAtributos();
                //   ViewBag.ListaAtributos = atrIBL.ObtenerAtributos();
                // ViewBag.CategoriaId = new SelectList(proIBL.ObtenerCategoriasPorTienda(user_sitio.idTienda), "CategoriaId","TiendaId", "Nombre");

            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        // JSON TRAE LISTA PRODUCTOS
        public JsonResult TipoProdList(string id)
        {

            user_sitio = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;

            List<SelectListItem> lista_item_Prod = new List<SelectListItem>();
            
            System.Diagnostics.Debug.WriteLine("Entro en obtener producto de cat id: "+id);
            //var ls = new[] { proIBL.ObtenerTipoProdCategoria(user.idTienda, idCategoria) };
            //return Json(ls, JsonRequestBehavior.AllowGet);
             
            try
            {
                if (user_sitio.idTienda == 0 || id == null) System.Diagnostics.Debug.WriteLine("Parametros consulta nulos");
                else System.Diagnostics.Debug.WriteLine("Parametros" + user_sitio.idTienda + "id sitio "+ id);
                int id_cat = Convert.ToInt32(id);
                System.Diagnostics.Debug.WriteLine("Parametros consulta: " + user_sitio.idTienda +"-  "+ id_cat.ToString());

                var productos = proIBL.ObtenerTipoProdCategoria(user_sitio.idTienda, id_cat);
                if (productos != null)
               {
                   System.Diagnostics.Debug.WriteLine(" Antes for each ");

                   lista_item_Prod.Add(new SelectListItem()
                   {
                       Value = "0",
                       Text = "Seleccione un Producto",
                       Selected = true
                   });


                   foreach (var item in productos)
                    {
                         lista_item_Prod.Add(new SelectListItem ()
                        {
                            Value = item.TipoProductoId.ToString(),
                            Text = item.Titulo,
                            Selected = false 
                        });
                    }
                   System.Diagnostics.Debug.WriteLine(" Salgo del foreach");

               }
               else System.Diagnostics.Debug.WriteLine("Lista Producto nula");
            }
	        catch (Exception)
	        {
		
		        throw;
	        }

            return Json(lista_item_Prod);
        }

        // DEVUELVO ARTICULOS 
        public List<SelectListItem> AtributoList(String id)
        {
            user_sitio = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
            List<SelectListItem> atributos = new List<SelectListItem>();
            System.Diagnostics.Debug.WriteLine("Entro en obtener atributo de cat id: " + id);
            //var ls = new[] { proIBL.ObtenerTipoProdCategoria(user.idTienda, idCategoria) };
            //return Json(ls, JsonRequestBehavior.AllowGet);
            try
            {
                if (user_sitio.idTienda == 0 || id == null) System.Diagnostics.Debug.WriteLine("Parametros consulta nulos");
                else System.Diagnostics.Debug.WriteLine("Parametros" + user_sitio.idTienda + "id sitio " + id);
                int id_cat = Convert.ToInt32(id);
                System.Diagnostics.Debug.WriteLine("Parametros consulta: " + user_sitio.idTienda + "-  " + id_cat.ToString());
                var lista_atributos = proIBL.ObtenerAtributosTipoProd(user_sitio.idTienda, id_cat);
                if (atributos != null)
                {
                    System.Diagnostics.Debug.WriteLine(" Antes for each ");
                    foreach (var item in lista_atributos)
                    {
                        atributos.Add(new SelectListItem()
                        {
                            Value = item.AtributoId.ToString(),
                            Text = item.Nombre,
                            Selected = false
                        });
                    }
                    System.Diagnostics.Debug.WriteLine(" Salgo del foreach");
                }
                else System.Diagnostics.Debug.WriteLine("Lista Atributo nula");
            }
            catch (Exception)
            {
                 throw;
            }
            return atributos ;
        }


        public List<AtributoSite> AtributoSiteList(String id)
        {
            user_sitio = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
            List<AtributoSite> atributos = new List<AtributoSite>();
            System.Diagnostics.Debug.WriteLine("Entro en obtener atributo de cat id: " + id);
            
            try
            {
                if (user_sitio.idTienda == 0 || id == null) System.Diagnostics.Debug.WriteLine("Parametros consulta nulos");
                else System.Diagnostics.Debug.WriteLine("Parametros" + user_sitio.idTienda + "id sitio " + id);
                int id_cat = Convert.ToInt32(id);
                System.Diagnostics.Debug.WriteLine("Parametros consulta: " + user_sitio.idTienda + "-  " + id_cat.ToString());
                var lista_atributos = proIBL.ObtenerAtributosTipoProd(user_sitio.idTienda, id_cat);
                if (lista_atributos != null)
                {
                    System.Diagnostics.Debug.WriteLine(" Antes for each ");
                    foreach (var item in lista_atributos)
                    {
                        atributos.Add(new AtributoSite()
                        {
                            Nombre=item.Nombre,
                            valor="",
                            IdAtributo=item.AtributoId

                        });
                    }
                    System.Diagnostics.Debug.WriteLine(" Salgo del foreach");
                }
                else System.Diagnostics.Debug.WriteLine("Lista Atributo nula");
            }
            catch (Exception)
            {
                throw;
            }
            return atributos;
        }




        // GET: Subastas
         public ActionResult CargarAtributo(String id)
        {
            if (id == null) System.Diagnostics.Debug.WriteLine(" id categoria nulo");
            else System.Diagnostics.Debug.WriteLine(" id categoria no nulo" + id);
            List<AtributoSite> atributos = new List<AtributoSite>();
            try
            {
                 System.Diagnostics.Debug.WriteLine(" antes de obtener atributos");
               // System.Diagnostics.Debug.WriteLine(" id categoria ="+id);
                user_sitio = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;

                int id_cat = Convert.ToInt32(id);
                var lista_atributos = proIBL.ObtenerAtributosTipoProd(user_sitio.idTienda, id_cat);

                foreach (var item in lista_atributos)
                {
                    AtributoSite atrib = new AtributoSite();
                    atrib.Nombre = item.Nombre;
                    atrib.IdAtributo = item.AtributoId;
                   // atributos.Add(atrib);
                }
             }
            catch (Exception)
            {
                
                throw;
            }
            return View(atributos);

        }


        //
        // GET: Subastas
         public ActionResult CreacionSubasta(String id_cat, String id_prod)
         {

            System.Diagnostics.Debug.WriteLine(" Entro a crea");
            SubastaSite sub_site = new SubastaSite();
            try
             {
                sub_site.id_Categoria = Convert.ToInt32(id_cat);
                sub_site.id_Producto = Convert.ToInt32(id_prod);
                sub_site.fecha_Cierre = DateTime.Now;
                // tipo subasta
                List<String> tipo_subasta = new List<String> { "Tipo de Venta", "Subasta", "Compra directa" };
                ViewData["Tipo"] = tipo_subasta;

                // Garantia
                List<String> garantia = new List<String> { "Si", "No" };
                ViewData["Garantia"] = garantia;

                sub_site.atributos = AtributoSiteList(id_cat);
                foreach (var item in sub_site.atributos)
                {
                    System.Diagnostics.Debug.WriteLine("Atributo  id atrib " + item.IdAtributo.ToString());
                }


               

            }
            catch (Exception)
            {
                throw;
            }
             return PartialView(sub_site);
         }



         public ActionResult Mapa()
         {
             return PartialView();
        }
        
        
        
        // POST: Subastas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult Create([Bind(Include = "id_Categoria,id_Producto,atributos,titulo,descripcion,tags,precio_Base,precio_Compra,garantia,coordenadas,direccion,fecha_Inicio,fecha_Cierre")]SubastaSite subasta_site, FormCollection form, HttpPostedFileBase portada)
        {
            Subasta subasta = new Subasta();
            

            if (subasta_site.fecha_Cierre != null) System.Diagnostics.Debug.WriteLine("fecha " + subasta_site.fecha_Cierre);

            else System.Diagnostics.Debug.WriteLine("Nulo");
            user_sitio = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
            
            subasta.id_Vendedor = usuIBL.ObtenerIdByEmail(user_sitio.Dominio, user_sitio.Email);
            subasta.estado = EstadoTransaccion.Activa;
            subasta.titulo = subasta_site.titulo;
            subasta.valor_Actual = (double)subasta_site.precio_Base;
            subasta.fecha_Inicio = (DateTime)System.DateTime.Now;
            if (subasta_site.fecha_Cierre != null)
            { subasta.fecha_Cierre = (DateTime)subasta_site.fecha_Cierre; }

            subasta.tags = subasta_site.tags;
            subasta.descripcion= subasta_site.descripcion;
            subasta.garantia = subasta_site.garantia;
            subasta.direccion = subasta_site.direccion;
            subasta.coordenadas = subasta_site.coordenadas;
            subasta.id_Categoria = (int)subasta_site.id_Categoria;
            subasta.id_Producto = (int)subasta_site.id_Producto;
            subasta.precio_Base = (double)subasta_site.precio_Base;
            subasta.precio_Compra = (double)subasta_site.precio_Compra;
            subasta.valor_Actual = (double)subasta_site.valor_Actual;
            string tipo = form["Tipo"];
            //string cat = form["Categorias"];
            //string prod = form["Productos"];
            //string atr = form["Atributos"];

            //string atr_sub = form["Atributos"];



           // int producto = int.Parse(prod);
           // subasta.id_Producto = producto;

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
                subasta.valor_Actual=(double)subasta.precio_Base;
                subasta.precio_Compra = (double)subasta.precio_Base;
                valor_tenant = user_sitio.Dominio.ToString();
                id_sub = subIBL.AgregarSubasta_ID(valor_tenant, subasta);
            }
            else
            {
                TipoFinalizacion tipoSub = TipoFinalizacion.Compra_directa;
                subasta.finalizado = tipoSub;
                subasta.precio_Base = (double)subasta.precio_Compra;
                subasta.valor_Actual = (double)subasta.precio_Compra;
                valor_tenant = user_sitio.Dominio.ToString();
                id_sub= subIBL.AgregarSubasta_ID(valor_tenant, subasta);
            }



            foreach (var item in subasta_site.atributos)
            {
              //  System.Diagnostics.Debug.WriteLine("Atributo idsub:" + id_sub.ToString() + " id atrib " + item.IdAtributo.ToString());
                if (item.valor != null)
                {
                    Atributo_Subasta atributo=new Atributo_Subasta();
                    atributo.id_Subasta=id_sub;
                    atributo.id_Atributo=item.IdAtributo;
                    atributo.valor=item.valor;
                    //atributo.Subasta = subasta;
                    atrSubIBL.AgregarAtributo_Subasta(valor_tenant, atributo);
                }
            }
             SubastaSite  sub_site = crearSubastaSite(subasta);
            

            return View("ImagenesSubasta", subasta);
            //return View("DetalleProducto", sub_site);
       }




        // MODELO PARA EL SITIO DE SUBASTA
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
                sub_site.valor_Actual = (double)subasta.valor_Actual;
                sub_site.precio_Base=(double)subasta.precio_Base;
                sub_site.precio_Compra = (double)subasta.precio_Compra;
                sub_site.Calificacion = subasta.Calificacion;
                sub_site.Favorito = subasta.Favorito;
                sub_site.id_Vendedor = subasta.id_Vendedor;
                sub_site.atributos = AtributoSiteList(subasta.id_Categoria.ToString());
                foreach (var atributo in sub_site.atributos)
                {
                    foreach (var atrib_subasta in sub_site.Atributo_Subasta)
                    {
                        if (atrib_subasta.id_Atributo == atributo.IdAtributo)
                        {

                            atributo.valor = atrib_subasta.valor;
                        }
                    }
                }
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
                usuIBL.ActualizarUsuario(valor_tenant, usuario);//le descuento plata al comprador

                int idVendedor = subasta.id_Vendedor;
                var vendedor = usuIBL.GetUsuario(valor_tenant, idVendedor);

                vendedor.billetera = vendedor.billetera + (double)subasta.precio_Compra;
                usuIBL.ActualizarUsuario(valor_tenant, vendedor);//le agrego plata al vededor

                subasta.estado = EstadoTransaccion.Cerrada;
                subasta.finalizado = TipoFinalizacion.Compra_directa;
                subasta.id_Comprador = idLogueado;
                subasta.fecha_Cierre = DateTime.Now;
                subIBL.ActualizarSubasta(valor_tenant, subasta);
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
                int idLogueado = usuIBL.ObtenerIdByEmail(valor_tenant, user_sitio.Email);
                var usuario = usuIBL.GetUsuario(valor_tenant, idLogueado);

                Subasta subasta = subIBL.ObtenerSubasta(valor_tenant, idSubasta);
                ViewBag.ListaImg = subIBL.ObtenerImagenes(valor_tenant, idSubasta);
                if (subasta == null)
                {
                    return HttpNotFound();
                }

                sub_site = crearSubastaSite(subasta);
                if(usuario != null){
                    sub_site.billeteraUsuario = usuario.billetera;
                }
                
            }
            catch (Exception)
            {
                throw;
            }

            return View(sub_site);
        }

        public ActionResult ImagenesSubasta(int id)
        {

            user_sitio = Session["usuario"] as UsuarioSite;
            valor_tenant = user_sitio.Dominio.ToString();
            Subasta sub = subIBL.ObtenerSubasta(valor_tenant, id);

            return View(sub);
        }

        public ActionResult SaveUploadedFile(Subasta sub)
        {
            user_sitio = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
            CloudBlobContainer blobContainer = _bls.GetContainerTienda(user_sitio.Dominio);
            valor_tenant = user_sitio.Dominio.ToString();
            Subasta subasta = subIBL.ObtenerSubasta(valor_tenant, sub.id);
            //bool isSavedSuccessfully = true;

            string fName = "";
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //Save file content goes here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {
                    var nombreFoto = user_sitio.Dominio + Guid.NewGuid().ToString() + "_subasta";
                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(nombreFoto);

                    Imagen imagen = new Imagen();
                    imagen.id_Subasta = subasta.id;
                    imagen.nombre = nombreFoto;
                    imagen.uri = blob.Uri.ToString();

                    subIBL.AgregarImagen(user_sitio.Dominio, imagen);
                    blob.UploadFromStream(file.InputStream);

                }

            }

            SubastaSite subSide = crearSubastaSite(subasta);
            return View("DetalleProducto", subSide);
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

            IEnumerable<ComentarioModel> modelList = new List<ComentarioModel>();
            List<Comentario> comentarios;
            comentarios = comIBL.ComentariosByProducto(valor_tenant, idSubasta);
            comentarios.Reverse();
            modelList = comentarios.Select(x =>
                                            new ComentarioModel()
                                            {
                                                id_Subasta = x.id_Subasta,
                                                id_Usuario = x.id_Usuario,
                                                nombreUsuario = usuIBL.GetNombreUsuario(valor_tenant, x.id_Usuario),
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

            IEnumerable<ComentarioModel> modelList = new List<ComentarioModel>();
            List<Comentario> comentarios;
            comentarios = comIBL.ComentariosByProducto(valor_tenant, idSubasta);
            comentarios.Reverse();
            modelList = comentarios.Select(x =>
                                            new ComentarioModel()
                                            {
                                                id_Subasta = x.id_Subasta,
                                                id_Usuario = x.id_Usuario,
                                                nombreUsuario = usuIBL.GetNombreUsuario(valor_tenant, x.id_Usuario),
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

        public ActionResult Categoria(int idCat, string SearchString, int? Tipo, string min, string max, int? page, int? rows)
        {
            user_sitio = Session["usuario"] as UsuarioSite;
            List<CategoriasDTO> cat = proIBL.ObtenerCategoriasPorTienda(user_sitio.idTienda);
            CategoriasDTO aux = cat.FirstOrDefault(c => c.CategoriaId == idCat);
            ViewBag.idCat = idCat;
            ViewBag.nomCat = aux.Nombre;
            if (page == null)
            {
                page = 1;
            }

            if (rows == null)
            {
                rows = 10;
            }

            var lista = subIBL.ObtenerSubastasPorCriterio(user_sitio.Dominio, idCat,SearchString,Tipo,min,max).ToList();
            //var lista = subIBL.ObtenerSubastasCompleto(user_sitio.Dominio, idCat, SearchString, Tipo, min, max, (int)page, (int)rows).ToList();
            //ViewBag.ListaSubastas = lista;
            var totalRows = lista.Count();
            var totalPages = (int)Math.Ceiling((double)totalRows / (int)rows);
            //var pageIndex = (page ?? 1) - 1;
            //var usersAsIPagedList = new StaticPagedList<Subasta>(lista, (int)pageIndex, totalPages, totalRows);
           var usersAsIPagedList = lista.ToPagedList((int)page, (int)rows);
            ViewBag.ListaSubastas = usersAsIPagedList;


            return View();
        }

        public ActionResult GetCategoriasFilters(int idCat)
        {
            ViewBag.idCat = idCat;
            return View();
        }

        public Paginacion<Subasta> GetCategoriasFiltersP(int idCat, string SearchString, int? Tipo, string min, string max, int? page, int? rows)
        {

            user_sitio = Session["usuario"] as UsuarioSite;
            ViewBag.idCat = idCat;
            //var lista = subIBL.ObtenerSubastasCompleto(user_sitio.Dominio, idCat, SearchString, Tipo, min, max,page,rows).ToList();
            if (page == null)
            {
                page = 1;
            }

            if (rows == null) {
                rows = 10;
            }

            var results = subIBL.ObtenerSubastasCompleto(user_sitio.Dominio, idCat, SearchString, Tipo, min, max, (int)page, (int)rows).ToList();
            var totalRows = results.Count();
            var totalPages = (int)Math.Ceiling((double)totalRows / (int)rows);

            var result = new Paginacion<Subasta>()
            {
                PageSize = (int)rows,
                TotalRows = totalRows,
                TotalPages = totalPages,
                CurrentPage = (int)page,
                Results = results
            };

            return result;
        }

        public JsonResult esFavorito(int idSubasta)
        {
            user_sitio = Session["usuario"] as UsuarioSite;
            var idUsuario = usuIBL.ObtenerIdByEmail(user_sitio.Dominio, user_sitio.Email);
            valor_tenant = user_sitio.Dominio.ToString();

            Boolean modelList = favIBL.esFavorito(valor_tenant, idSubasta, idUsuario);

            return Json(modelList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Comparar(int idSubasta, int idCat, string Tipo) 
        {
            int tipos = 0;
            if (Tipo == "Subasta")
            {
                tipos = 1;
            }
            else 
            {
                tipos = 2;            
            }

            user_sitio = Session["usuario"] as UsuarioSite;
            valor_tenant = user_sitio.Dominio.ToString();
            var sub = subIBL.ObtenerSubasta(valor_tenant, idSubasta);

            var lista = subIBL.ObtenerSubastasPorCriterio(user_sitio.Dominio, idCat,"", tipos, null, null).ToList();
            var lista_item_Cat = new List<SelectListItem>();
            lista_item_Cat.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Seleccione una publicacion",
                Selected = true
            });
            foreach (var item in lista)
            {
                lista_item_Cat.Add(new SelectListItem()
                {
                    Value = item.id.ToString(),
                    Text = item.titulo,
                    Selected = false
                });
            }
            ViewData["Subastas"] = lista_item_Cat;
            SubastaSite sub_site = crearSubastaSite(sub);
            return View(sub_site);
        }

        public ActionResult SubastaComparar(int idSubasta)
        {

            user_sitio = Session["usuario"] as UsuarioSite;
            valor_tenant = user_sitio.Dominio.ToString();
            var sub = subIBL.ObtenerSubasta(valor_tenant, idSubasta);
            SubastaSite sub_site = crearSubastaSite(sub);
            return View(sub_site);
        }


    }
}
