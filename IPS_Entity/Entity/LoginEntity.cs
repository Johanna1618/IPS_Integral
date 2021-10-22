using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPS_Entity.Entity
{
    public class LoginEntity
    {
        // Pendiente para cambiar por cédula
        [Required(ErrorMessage = "Identificacion Requerida")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "Contraseña Requerida")]
        public string Contraseña { get; set; }
    }
}
