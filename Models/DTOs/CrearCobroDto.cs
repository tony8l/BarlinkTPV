using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models.DTOs
{
    public class CrearCobroDto
    {
        public string TicketId { get; set; }

        public string EmpleadoId { get; set; }

        public MetodoPago MetodoPago { get; set; }

        public decimal ImporteEntregado { get; set; }
    }
}
