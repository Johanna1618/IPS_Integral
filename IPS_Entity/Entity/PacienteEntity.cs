using System;
using System.Collections.Generic;
using static IPS_Entity.Helper.Enum;

#nullable disable

namespace IPS_Entity.Entity
{
    public partial class PacienteEntity : PersonaEntity
    {

        public int IdPaciente { get; set; }
        public TipoConvenio TipoConvenio { get; set; } // Cambiado

        //public int IdConvenio { get; set; } 

        //public int IdPersona { get; set; } ya está por herencia

    }
}