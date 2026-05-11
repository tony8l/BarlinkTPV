using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models.DTOs
{
    public class Ajustes
    {
        public string Id { get; set; } = string.Empty;
        public string UsuarioId { get; set; } = string.Empty;
        public Tema Tema { get; set; }
        public Idioma Idioma { get; set; }
    }

    public enum Tema
    {
        Predeterminado,
        Claro,
        Oscuro
    }

    public enum Idioma
    {
        ES,
        ENG
    }
}
