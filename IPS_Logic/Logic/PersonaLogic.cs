using IPS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using IPS_Logic.DatabaseIPS;
using static IPS_Entity.Helper.Enum;

namespace IPS_Logic.Logic
{
    public class PersonaLogic
    {
        DB_IPSContext _IPSContext = new DB_IPSContext();

        // Verificar sesión ----------------
        public bool VerifySession(LoginEntity loginEntity)
        {

            // var paciente = _IPSContext.Pacientes.Where(x => x.Id == loginEntity.Id).FirstOrDefault();
            var paciente = _IPSContext.Personas.Where(x => x.Cedula == loginEntity.Cedula).FirstOrDefault();
            if (paciente == null) return false;
            var person = _IPSContext.Personas.Where(x => x.Contraseña == loginEntity.Contraseña).FirstOrDefault();
            if (person == null) return false;

            return true;
        }

        // ResponseBaseEntity --------------
        
        public ResponseBaseEntity CreatePaciente(PacienteEntity pacienteEntity)
        {
            using (var dbContextTransaction = _IPSContext.Database.BeginTransaction())
            {
                try
                {
                    var paciente = _IPSContext.Pacientes.Where(x => x.Id == pacienteEntity.Id).FirstOrDefault();
                    if (paciente != null)
                    {
                        return GetResponseBaseEntity("Ya existe un usuario con esa cédula", TypeMessage.danger)
;                   }

                    _IPSContext.Personas.Add(ConvertPacienteEntityToPersona(pacienteEntity));
                    _IPSContext.Pacientes.Add(ConvertPacienteEntityToPaciente(pacienteEntity));
                    _IPSContext.SaveChanges();
                    dbContextTransaction.Commit();

                    return GetResponseBaseEntity("Paciente registrado con exito!", TypeMessage.success);
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return GetResponseBaseEntity(ex.Message, TypeMessage.danger);
                }
            }
        }
        

        private ResponseBaseEntity GetResponseBaseEntity(string message, TypeMessage typeMessage)
        {
            ResponseBaseEntity responseBaseEntity = new ResponseBaseEntity();
            responseBaseEntity.Message = message;
            responseBaseEntity.Type = typeMessage;
            return responseBaseEntity;
        }

        // Conversores para el tipo Paciente ------------------------
        private Paciente ConvertPacienteEntityToPaciente(PacienteEntity pacienteEntity) // el del convenio
        {
            Paciente paciente = new Paciente();
            paciente.Id = pacienteEntity.Id;
            // TipoConvenio que viene de la llave foranea
            paciente.IdConvenioNavigation.TipoConvenio = (int)pacienteEntity.TipoConvenio; // Revisar

            return paciente;
        }

        private Persona ConvertPacienteEntityToPersona(PacienteEntity pacienteEntity) // persona-Persona
        {
            Persona persona = new Persona();

            persona.Id = pacienteEntity.Id;
            persona.Nombre = pacienteEntity.Nombre;
            persona.Apellidos = pacienteEntity.Apellidos;
            persona.Cedula = pacienteEntity.Cedula;
            persona.Contraseña = pacienteEntity.Contraseña;

            return persona;
        }

        private PacienteEntity ConvertPersonaToPacienteEntity(Persona persona) // Paciente-paciente
        {
            PacienteEntity pacienteEntity = new PacienteEntity();

            pacienteEntity.Id = persona.Id;
            pacienteEntity.Nombre = persona.Nombre;
            pacienteEntity.Apellidos = persona.Apellidos;
            pacienteEntity.Cedula = persona.Cedula;
            pacienteEntity.Contraseña = persona.Contraseña;

            return pacienteEntity;
        } 

        //private Persona ConvertPacienteEntityToPersona(PacienteEntity pacienteEntity) // Repetido ?? Persona-persona
        //{
        //    Persona persona = new Persona();

        //    persona.Id = pacienteEntity.Id;
        //    persona.Nombre = pacienteEntity.Nombre;
        //    persona.Apellidos = pacienteEntity.Apellidos;
        //    persona.Cedula = pacienteEntity.Cedula;
        //    persona.Contraseña = pacienteEntity.Contraseña;

        //    return persona;
        //}

        public PacienteEntity GetPacienteEntityForId(int id)
        {

            var person = _IPSContext.Personas.Where(x => x.Id == id).FirstOrDefault();

            if (person != null)
            {
                return ConvertPersonaToPacienteEntity(person);
            }

            return new PacienteEntity();
        }





        // Métodos de Lista Personas ------------------

        public List<PersonaEntity> GetAllPeople()
        {
            List<PersonaEntity> listPersonEntities = new List<PersonaEntity>();

           var listPersonDataBase = _IPSContext.Personas.ToList(); // Trae una lista desde la BD

            foreach (var PersonDataBase in listPersonDataBase)
            {
                PersonaEntity personaEntity = new PersonaEntity();

                listPersonEntities.Add(ConvertPersonDatabaseToPersonEntity(PersonDataBase));
            }

            return listPersonEntities;
        }

        public PersonaEntity AddPerson(PersonaEntity personaEntity)
        {
            try
            {

            
            if (GetAllPeople().Where(x => x.Cedula == personaEntity.Cedula).Any()) {

                PersonaEntity persona = new PersonaEntity();
                persona.Message = "Ya existe un usuario con esa cédula";
                persona.Type = "danger";
                return persona;
            }

                _IPSContext.Personas.Add(ConvertPersonEntityToPersonDatabase(personaEntity)); // conversión Ent to Db
                _IPSContext.SaveChanges();

                personaEntity.Message = "Persona guardada con exito";
                personaEntity.Type = "success";

                return personaEntity; 
            }
            catch (Exception ex)
            {
                PersonaEntity persona = new PersonaEntity();
                persona.Message = ex.Message;
                return persona;
            }
            
        }

        // filtrando por cédula
        public PersonaEntity GetPersonForCedula(string cedula)
        {
            var personEntity = GetAllPeople().Where(x => x.Cedula == cedula).FirstOrDefault();

            if (personEntity == null)
            {
                PersonaEntity persona = new PersonaEntity();
                persona.Message = "No existe una persona con esa cédula";
                persona.Type = "danger";
                return persona;
            }

            return personEntity;
        }

        // Actualizando con la cédula
        public PersonaEntity UpdatePerson(PersonaEntity personaEntity)
        {
            try
            {
                var personDataBase = _IPSContext.Personas.Where(x => x.Cedula == personaEntity.Cedula).FirstOrDefault();

                if (personDataBase == null)
                {

                    PersonaEntity persona = new PersonaEntity();
                    persona.Message = "Ya existe un usuario con esa cédula";
                    persona.Type = "danger";
                    return persona;
                }

                personDataBase.Id = personaEntity.Id;
                personDataBase.Nombre = personaEntity.Nombre;
                personDataBase.Apellidos = personaEntity.Apellidos;
                personDataBase.Cedula = personaEntity.Cedula;
                personDataBase.Contraseña = personaEntity.Contraseña;

                _IPSContext.Personas.Update(personDataBase);
                _IPSContext.SaveChanges();
                personaEntity.Message = "Persona actualizada con exito";
                personaEntity.Type = "success";

                return personaEntity; 
            }
            catch (Exception ex)
            {
                PersonaEntity persona = new PersonaEntity();
                persona.Message = ex.Message;
                return persona;
            }
        }

        // Conversores del tipo Persona ---------------------
        private Persona ConvertPersonEntityToPersonDatabase(PersonaEntity personaEntity) // Ent to Db
        {
            Persona persona = new Persona();

            persona.Id = personaEntity.Id;
            persona.Nombre = personaEntity.Nombre;
            persona.Apellidos = personaEntity.Apellidos;
            persona.Cedula = personaEntity.Cedula;
            persona.Contraseña = personaEntity.Contraseña;

            return persona;
        }

        private PersonaEntity ConvertPersonDatabaseToPersonEntity(Persona persona) // Db to Ent
        {
            PersonaEntity personaEntity = new PersonaEntity();

            personaEntity.Id = persona.Id;
            personaEntity.Nombre = persona.Nombre;
            personaEntity.Apellidos = persona.Apellidos;
            personaEntity.Cedula = persona.Cedula;
            personaEntity.Contraseña = persona.Contraseña;

            return personaEntity;
        }

    }

}
