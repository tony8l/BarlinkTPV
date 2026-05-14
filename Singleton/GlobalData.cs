using BarlinkTPV.Models;
using BarlinkTPV.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Singleton
{
    public class GlobalData
    {
        // Singleton del usuario loggeado
        public string IdUsuario { get; set; }
        public string NombreUsuario {  get; set; }
        public string DniUsuario {  get; set; }
        public RolUsuario? RolUsuario { get; set; }
        public TipoFichaje? UltimoTipoFichaje { get; set; }

        
        // Singleton de los ajustes del usuario loggeado
        public string AjustesActualesId { get; set; }
        public Tema TemaActual { get; set; }
        public Idioma IdiomaActual {  get; set; }

        // Método para limpiar la sesión al cerrár sesión
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
