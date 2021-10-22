using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPS_Entity.Entity
{
    public class PersonaEntity : BaseEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }

        [Required(ErrorMessage ="Cédula requerida")]
        public string Cedula { get; set; }

        [Required(ErrorMessage ="Contraseña requerida")]
        public string Contraseña { get; set; }
    }
}
