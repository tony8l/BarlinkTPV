using BarlinkTPV.Services;
using System;
using BarlinkTPV.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BarlinkTPV.Models
{
    public class InformeVentas
    {
        public DateTime Fecha { get; set; }
        public decimal TotalCaja { get; set; }
        public decimal TotalEfectivo { get; set; }
        public decimal TotalTarjeta { get; set; }
    } 
}
