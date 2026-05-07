using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models
{
    public class Producto
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Iva { get; set; }
        public string CategoriaId { get; set; } = string.Empty;
        public bool EsVisible { get; set; }
        public string NombreImagen { get; set; } = string.Empty;
    }
}
