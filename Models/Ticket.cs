using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models
{
    public class Ticket
    {
        public string Id { get; set; } = string.Empty;
        public string CodigoTicket { get; set; } = string.Empty;
        public string MesaId { get; set; } = string.Empty;
        public DateTime FechaApertura { get; set; }
        public DateTime? FechaCierre { get; set; }
        public EstadoTicket Estado { get; set; }
        public List<LineaTicket> Lineas { get; set; } = new();
        public decimal Subtotal { get; set; }
        public decimal TotalIva { get; set; }
        public decimal Total { get; set; }
    }

    public enum EstadoTicket
    {
        Abierto,
        Cerrado
    }
}
