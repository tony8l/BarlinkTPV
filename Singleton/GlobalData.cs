using BarlinkTPV.Models;
using BarlinkTPV.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Singleton
{
    public class GlobalData
    {
        public string IdUsuario { get; set; }
        public string NombreUsuario {  get; set; }
        public string DniUsuario {  get; set; }
        public RolUsuario? RolUsuario { get; set; }
        public TipoFichaje? UltimoTipoFichaje { get; set; }

        
        public string AjustesActualesId { get; set; }
        public Tema TemaActual { get; set; }
        public Idioma IdiomaActual {  get; set; }

        public void CerrarSesion()
        {
            IdUsuario = string.Empty;
            NombreUsuario = string.Empty;
            DniUsuario = string.Empty;
            RolUsuario = null;
            UltimoTipoFichaje = null;
        }
    }
}
