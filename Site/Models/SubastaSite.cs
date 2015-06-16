using Crosscutting.EntityTenant;
using Crosscutting.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public  class SubastaSite
    {
       
        /*public Subasta()
        {
            Atributo_Subasta = new HashSet<Atributo_Subasta>();
            Calificacion = new HashSet<Calificacion>();
            Imagen = new HashSet<Imagen>();
            Oferta = new HashSet<Oferta>();
            Favorito = new HashSet<Favorito>();
            Comentario = new HashSet<Comentario>();
        }*/

        public List<AtributoSite> atributos { get; set; }
        
        public int id { get; set; }

        public int? id_Comprador { get; set; }

        public string nick_Comprador { get; set; }

        public int id_Vendedor { get; set; }

        public string nick_Vendedor { get; set; }

        public int id_Categoria { get; set; }
        
        public string nombre_producto { get; set; }

        public int id_Producto { get; set; }

        [Display(Name = "Título")]
        public string titulo { get; set; }

        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        [Display(Name = "Tags")]
        public string tags { get; set; }

        [Display(Name = "Precio Base")]
        public double precio_Base { get; set; }

        [Display(Name = "Precio de Compra")]
        public double precio_Compra { get; set; }

        [Display(Name = "Garantía")]
        public string garantia { get; set; }

        [Display(Name = "Coordenadas")]
        public string coordenadas { get; set; }

        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        public EstadoTransaccion estado { get; set; }

        [Display(Name = "Tipo de Venta")]
        public string tipo_venta { get; set; }

        public TipoFinalizacion finalizado { get; set; }

        [Display(Name = "Valor Actual")]
        public double valor_Actual { get; set; }

        [Display(Name = "Imagen de Portada")]
        public string portada { get; set; }

        [Display(Name = "Fecha de inicio")]
        public DateTime fecha_Inicio { get; set; }


        
        [Display(Name = "Fecha de Finalización")]
        public DateTime? fecha_Cierre { get; set; }

        public double billeteraUsuario { get; set; }

        public virtual ICollection<Atributo_Subasta> Atributo_Subasta { get; set; }


        public virtual ICollection<Calificacion> Calificacion { get; set; }

        public virtual ICollection<Imagen> Imagen { get; set; }

        public virtual ICollection<Oferta> Oferta { get; set; }

        public virtual ICollection<Favorito> Favorito { get; set; }

        public virtual ICollection<Comentario> Comentario { get; set; }

        public virtual Usuario Vendedor { get; set; }
    }
}