using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models
{
    public class InformeCaja
    {
        public DateTime Fecha { get; set; }
        public decimal TotalCaja { get; set; }
        public decimal TotalEfectivo { get; set; }
        public decimal TotalTarjeta { get; set; }
    }
}
