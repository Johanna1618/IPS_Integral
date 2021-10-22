using System;
using System.Collections.Generic;

#nullable disable

namespace IPS_Logic.DatabaseIPS
{
    public partial class MedicoPorSede
    {
        public int IdSede { get; set; }
        public int IdMedico { get; set; }

        public virtual Sede IdSede1 { get; set; }
        public virtual Medico IdSedeNavigation { get; set; }
    }
}
