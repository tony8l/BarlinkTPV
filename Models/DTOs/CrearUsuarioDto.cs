using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models.DTOs
{
    public class CrearUsuarioDto
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public RolUsuario Rol { get; set; }
        public bool Conectado { get; set; } = false;
        public bool Activado { get; set; } = true;
    }
}
