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
        public int Iva { get; set; }
        public decimal SubTotal => (PrecioUd / (1+(Iva/100m))) * Cantidad;
        public decimal Total => PrecioUd * Cantidad;
    }
}
