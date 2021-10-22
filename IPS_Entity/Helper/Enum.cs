using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPS_Entity.Helper
{
    public class Enum 
    {
        public enum TipoConvenio
        {
            // TipoConvenio cambiado en DB bool x int
            Afiliado = 1,
            Particular = 2
        }

        public enum TypeMessage
        {
            success = 1,
            danger = 2,
            info = 3,
            warning = 4
        }
    }
}
