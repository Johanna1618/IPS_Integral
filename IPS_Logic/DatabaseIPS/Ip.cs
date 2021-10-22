using System;
using System.Collections.Generic;

#nullable disable

namespace IPS_Logic.DatabaseIPS
{
    public partial class Ip
    {
        public Ip()
        {
            Sedes = new HashSet<Sede>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Sede> Sedes { get; set; }
    }
}
