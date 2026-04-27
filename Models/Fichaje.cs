using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models
{
    class Fichaje
    {
        public string Id { get; set; }
        public string EmpleadoId { get; set; }
        public string TipoFichaje { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
