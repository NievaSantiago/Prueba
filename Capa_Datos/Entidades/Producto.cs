using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Entidades
{
   public class Producto
    {   
        public long Codigo { get; set; }
        public string Nombre { get; set; }
        public  double Precio { get; set; }
        public int Stock { get; set; }
        public int Marca { get; set; }
        public int Categoria { get; set; }
        public string MarcaS { get; set; }
        public string CategoriaS { get; set; }
    }
}
