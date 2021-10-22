using IPS_Entity.Entity;
using IPS_Logic.Logic;
using IPS_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static IPS_Entity.Helper.Enum;

namespace IPS_Web.Controllers
{
    public class HomeController : Controller // se puede heredar este controller a otros
    {
        private readonly ILogger<HomeController> _logger;
        private PersonaLogic personaLogic = new PersonaLogic(); // private solo para estas vistas

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //this._logger = logger;
        }

        //--------------
        public IActionResult Index()
        { 
            return View();
        }

        //--------------
        public IActionResult Login(string loginError="")
        {

            //HttpContext.Session.Clear(); // Revisar

            if (!string.IsNullOrEmpty(loginError))
            {
                ViewBag.LoginError = loginError;
            }
            return View();
        }

        [HttpPost]
        public IActionResult Home(LoginEntity loginEntity)
        {
            var session = HttpContext.Session.GetString("token");

            if (session == null)
            {
                if (personaLogic.VerifySession(loginEntity))
                {
                    HttpContext.Session.SetString("token", loginEntity.Cedula);
                    VerifySession();
                }
                else
                {
                    return RedirectToAction("Login", "Home", routeValues: new { loginError = "El usuario no existe" });
                }
            }

            return View();
        }

        public IActionResult SignUp() // Registro
        {
            List<SelectListItem> listTipoConvenio = new List<SelectListItem>();
            foreach (int item in Enum.GetValues(typeof(TipoConvenio)))
            {
                listTipoConvenio.Add(new SelectListItem { Value = ((int)item).ToString(), Text = Enum.ToObject(typeof(TipoConvenio), (int)item).ToString() });
            }

            ViewBag.ListCompanyDocumentType = listTipoConvenio;
            return View();
        }

        [HttpPost]
        public IActionResult SignUpCreate(PacienteEntity pacienteEntity)
        {
            var responseBase = personaLogic.CreatePaciente(pacienteEntity);
            ViewBag.Message = responseBase.Message;
            ViewBag.Type = Enum.ToObject(typeof(TypeMessage), (int)responseBase.Type).ToString();

            return View();
        }

        //-----------------------

        private void VerifySession()
        {
            var session = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(session))
            {
                HttpContext.Session.Clear();
                ViewBag.LoginError = "El usuario no esta logeado";
            }
        }

        //public IActionResult CreateAppointment()
        //{
        //    VerifySession();
        //    return View();
        //}





        // Lista personas, Create, Edit, search ----------------

        public IActionResult ListaPersonas(string nombre = "")
        {
            List<PersonaEntity> listPersonEntities = new List<PersonaEntity>();
            if (string.IsNullOrEmpty(nombre))
            {
                listPersonEntities = personaLogic.GetAllPeople();
            }
            else
            {
                listPersonEntities = personaLogic.GetAllPeople().Where(x => x.Nombre.ToUpper().Contains(nombre.ToUpper())).ToList();
            }
            return View(listPersonEntities);
        }
        
        public IActionResult Create() // vista sola
        {
            return View();
        }

        // Post: oculta | Get: muestra
        [HttpPost]
        public IActionResult Create(PersonaEntity personaEntity) 
        {
            var person = personaLogic.AddPerson(personaEntity); 

            ViewBag.Message = person.Message;
            ViewBag.Type = person.Type;

            return View(personaEntity); 

        }

        public IActionResult Edit(string cedula)
        {
            var person = personaLogic.GetPersonForCedula(cedula);

            ViewBag.Message = person.Message;
            ViewBag.Type = person.Type;

            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(PersonaEntity personaEntity)
        {
            var person = personaLogic.UpdatePerson(personaEntity);

            ViewBag.Message = person.Message;
            ViewBag.Type = person.Type;

            return View(personaEntity);
        }

    }
}
