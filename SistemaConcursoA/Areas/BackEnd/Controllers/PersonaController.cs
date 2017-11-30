using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaConcursoA.Models;
using SistemaConcursoA.Filters;

namespace SistemaConcursoA.Areas.BackEnd.Controllers
{
    [Autenticado]
    public class PersonaController : Controller
    {
        // GET: BackEnd/Persona
        private Persona persona = new Persona();
        //private TipoPersona tepersona = new TipoPersona();
        public ActionResult Index()
        {
            ViewBag.tipo = persona.listartipo();
            return View();
        }

        public ActionResult AgregarEditar(int id = 0)//int id es el criterio
        {
            ViewBag.tipo = persona.listartipo();   //para el combo
            return View(
                id == 0 ? new Persona()//generar un nuevo semestre
                : persona.Obtener(id)//devuelve un registro por el ID
                );
        }


        public ActionResult Guardar(Persona model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/BackEnd/Persona");//devuelve por default index
            }
            else
            {
                return View("~/BackEnd/Persona/AgregarEditar.cshtml", model);
            }
        }
        public ActionResult Eliminar(int id)
        {
            persona.persona_id = id;
            persona.EliminarP();
            return Redirect("~/BackEnd/Persona");//devuelve por default index
        }

        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(persona.ListarGrilla(grid));
        }

        public ActionResult Ver(int id)
        {
            return View(persona.Obtener(id));
        }
    }
}