using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Models
{
    public class Mesa
    {
        public string Id { get; set; } = string.Empty;
        public string CategoriaMesa { get; set; } = string.Empty;
        public string CodigoMesa { get; set; } = string.Empty;
        public EstadoMesa EstadoMesa { get; set; }
        public bool Bloqueada { get; set; }
        public bool MesaHabilitada 
        { 
            get { return !Bloqueada; }
            set;
        }
    }

    public enum EstadoMesa
    {
        Libre,
        Ocupada,
        Cobrando
    }


}
