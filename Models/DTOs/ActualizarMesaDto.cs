using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models.DTOs
{
    public class ActualizarMesaDto
    {
        public string? CategoriaMesa { get; set; }
        public string? CodMesa { get; set; }
        public EstadoMesa? EstadoMesa { get; set; }
        public bool? Bloqueada { get; set; }
    }
}
