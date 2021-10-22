using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPS_Entity.Entity
{
    class MedicoEntity : PersonaEntity
    {
        public int IdMedico { get; set; }
        public string Codigo { get; set; }
        //public int IdPersona { get; set; }
        //public int IdServicio { get; set; }
    }
}
