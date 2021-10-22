using System;
using System.Collections.Generic;

#nullable disable

namespace IPS_Logic.DatabaseIPS
{
    public partial class Persona
    {
        public Persona()
        {
            Medicos = new HashSet<Medico>();
            Pacientes = new HashSet<Paciente>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public string Contraseña { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
