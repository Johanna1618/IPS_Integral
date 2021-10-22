using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IPS_Entity.Helper.Enum;

namespace IPS_Entity.Entity
{
    public class ConvenioEntity
    {
        public int Id { get; set; }
        //public int TipoConvenio { get; set; } // Original
        public TipoConvenio TipoConvenio { get; set; } // nuenvo tipo Enum
        public DateTime HorarioA { get; set; } // allow null
        public DateTime HorarioB { get; set; } // allow null
        public int Mensualidad { get; set; } // allow null
        public int Descuento { get; set; } // allow null
    }
}
