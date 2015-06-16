using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Entity
{
    public class SubastaAux
    {
      
        public int id { get; set; }

        public int? id_Comprador { get; set; }

         public string nombreComprador { get; set; }

        public int id_Vendedor { get; set; }

        public string nombreVendedor { get; set; }

        public int id_Categoria { get; set; }

        public int id_Producto { get; set; }

        public string titulo { get; set; }

        public string descripcion { get; set; }

        public double? precio_Base { get; set; }

        public double? precio_Compra { get; set; }

        public double valor_Actual { get; set; }
        public string portada { get; set; }

        public DateTime fecha_Inicio { get; set; }
        public DateTime fecha_Cierre { get; set; }
    }
}
