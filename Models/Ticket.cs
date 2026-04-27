using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models
{
    public class Ticket
    {
        public string Id { get; set; }
        public string MesaId { get; set; }
        public DateTime FechaApertura { get; set; }
        public bool Cobrado { get; set; }
        public List<LineaTicket> Lineas { get; set; } = new();
    }
}
