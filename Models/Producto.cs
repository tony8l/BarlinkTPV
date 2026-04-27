using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models
{
    public class Producto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Iva { get; set; }
        public string CategoriaId { get; set; }
        public bool EsVisible { get; set; }
    }
}
