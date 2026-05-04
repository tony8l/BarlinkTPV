using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models
{
    public class Fichaje
    {
        public string Id { get; set; } = string.Empty;
        public string EmpleadoId { get; set; } = string.Empty;
        public string DniEmpleado { get; set; } = string.Empty;
        public string NombreEmpleado { get; set; } = string.Empty;
        public TipoFichaje TipoFichaje { get; set; }
        public DateTime FechaHora { get; set; }
    }

    public enum TipoFichaje
    {
        Entrada,
        Salida
    }
}
