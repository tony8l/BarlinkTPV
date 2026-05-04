using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models
{
    public class Cobro
    {
        public string Id { get; set; } = string.Empty;
        public string TicketId { get; set; } = string.Empty;
        public string CodigoTicket { get; set; } = string.Empty;
        public string EmpleadoId { get; set; } = string.Empty;
        public MetodoPago MetodoPago { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal ImporteEntregado { get; set; }
        public decimal Devolucion { get; set; }
        public DateTime FechaCobro { get; set; }
    }

    public enum MetodoPago
    {
        Tarjeta,
        Efectivo
    }
}
