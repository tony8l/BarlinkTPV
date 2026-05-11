using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models.DTOs
{
    public class CrearAjusteDto
    {
        public string UsuarioId { get; set; }
        public Tema Tema { get; set; }
        public Idioma Idioma { get; set; }
    }
}
