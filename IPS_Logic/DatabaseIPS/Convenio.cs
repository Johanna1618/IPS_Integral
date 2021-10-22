using System;
using System.Collections.Generic;

#nullable disable

namespace IPS_Logic.DatabaseIPS
{
    public partial class Convenio
    {
        public Convenio()
        {
            Pacientes = new HashSet<Paciente>();
        }

        public int Id { get; set; }
        public int TipoConvenio { get; set; }
        public DateTime? HorarioA { get; set; }
        public DateTime? HorarioB { get; set; }
        public int? Mensualidad { get; set; }
        public int? Descuento { get; set; }

        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
