using Crosscutting.EntityTenant;
using Crosscutting.Enum;
using System;
using System.Collections.Generic;
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


        
        public int id { get; set; }

        public int? id_Comprador { get; set; }

        public string nick_Comprador { get; set; }

        public int id_Vendedor { get; set; }

        public string nick_Vendedor { get; set; }

        public int id_Categoria { get; set; }
        
        public string nombre_producto { get; set; }

        public int id_Producto { get; set; }

        public string titulo { get; set; }

        public string descripcion { get; set; }

        public string tags { get; set; }

        public double? precio_Base { get; set; }

        public double? precio_Compra { get; set; }

        public string garantia { get; set; }

        public string coordenadas { get; set; }

        public EstadoTransaccion estado { get; set; }

        public string tipo_venta { get; set; }

        public TipoFinalizacion finalizado { get; set; }

        public double valor_Actual { get; set; }

        public string portada { get; set; }

        public DateTime? fecha_Inicio { get; set; }

        public DateTime? fecha_Cierre { get; set; }

         public virtual ICollection<Atributo_Subasta> Atributo_Subasta { get; set; }


        public virtual ICollection<Calificacion> Calificacion { get; set; }

        public virtual ICollection<Imagen> Imagen { get; set; }

        public virtual ICollection<Oferta> Oferta { get; set; }

        public virtual ICollection<Favorito> Favorito { get; set; }

        public virtual ICollection<Comentario> Comentario { get; set; }

        public virtual Usuario Vendedor { get; set; }
    }
}