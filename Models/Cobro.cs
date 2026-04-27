using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models
{
    public class Cobro
    {
        public string Id { get; set; }
        public string TicketId { get; set; }
        public string EmpleadoId { get; set; }
        public string MetodoPago { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal ImporteEntregado { get; set; }
        public decimal Devolucion { get; set; }
        public DateTime FechaCobro { get; set; }
    }
}
