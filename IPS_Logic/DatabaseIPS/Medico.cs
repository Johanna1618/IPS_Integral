using System;
using System.Collections.Generic;

#nullable disable

namespace IPS_Logic.DatabaseIPS
{
    public partial class Medico
    {
        public Medico()
        {
            CitaMedicas = new HashSet<CitaMedica>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public int IdPersona { get; set; }
        public int IdServicio { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual Servicio IdServicioNavigation { get; set; }
        public virtual ICollection<CitaMedica> CitaMedicas { get; set; }
    }
}
