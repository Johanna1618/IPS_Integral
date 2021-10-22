using System;
using System.Collections.Generic;

#nullable disable

namespace IPS_Logic.DatabaseIPS
{
    public partial class CitaMedica
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public bool TipoCita { get; set; }
        public int IdSede { get; set; }
        public float ValorCita { get; set; }
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }

        public virtual Medico IdMedicoNavigation { get; set; }
        public virtual Paciente IdPacienteNavigation { get; set; }
        public virtual Sede IdSedeNavigation { get; set; }
    }
}
