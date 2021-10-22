using System;
using System.Collections.Generic;

#nullable disable

namespace IPS_Logic.DatabaseIPS
{
    public partial class Sede
    {
        public Sede()
        {
            CitaMedicas = new HashSet<CitaMedica>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCiudad { get; set; }
        public int IdIps { get; set; }

        public virtual Ciudad IdCiudadNavigation { get; set; }
        public virtual Ips IdIpsNavigation { get; set; }
        public virtual ICollection<CitaMedica> CitaMedicas { get; set; }
    }
}
