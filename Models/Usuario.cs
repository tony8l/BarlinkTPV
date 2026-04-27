using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public RolUsuario Rol { get; set; }
        public bool Conectado { get; set; }
    }

    public enum RolUsuario
    { 
        Camarero,
        Encargado
    }
}
