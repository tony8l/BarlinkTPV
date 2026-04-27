using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Singleton
{
    public class GlobalData
    {
        public string NombreUsuario {  get; set; }

        public void CerrarSesion()
        {
            NombreUsuario = null;
        }
    }
}
