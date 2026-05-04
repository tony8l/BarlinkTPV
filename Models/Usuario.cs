using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models
{
    public class Usuario
    {
        public string Id { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public RolUsuario Rol { get; set; }
        public bool Conectado { get; set; }
        public bool Activado { get; set; }
    }

    public enum RolUsuario
    { 
        Camarero,
        Encargado
    }
}
