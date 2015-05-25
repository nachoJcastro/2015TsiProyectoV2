using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Entity
{

    public class ImagenesDTO
    {
        [Key]
        public int ImageneId { get; set; }

        public int TiendaId { get; set; }
        public virtual TiendaVirtualDTO TiendaVirtual { get; set; }

        public string Nombre { get; set; }
        public string UrlImagenMediana { get; set; }

        public bool EsPortada { get; set; }

        //public HttpPostedFileWrapper ImagenSubida { get; set; }

        public bool ImagenEliminada { get; set; }

    }
}
