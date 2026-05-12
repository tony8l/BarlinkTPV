using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models.DTOs
{
    public class ActualizarUsuarioDto
    {
        public string? Dni { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public RolUsuario? Rol { get; set; }
        public bool? Activado { get; set; }
    }
}
