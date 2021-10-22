using System;
using System.Collections.Generic;

#nullable disable

namespace IPS_Logic.DatabaseIPS
{
    public partial class Servicio
    {
        public Servicio()
        {
            Medicos = new HashSet<Medico>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? CostoAfiliado { get; set; }
        public int? CostoNoAfiliado { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
    }
}
