using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models
{
    public class LineaTicket
    {
        public string ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioUd { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal => PrecioUd * Cantidad;
        public int Iva { get; set; }
        public decimal Total => SubTotal * (1 + (Iva / 100m));
    }
}
