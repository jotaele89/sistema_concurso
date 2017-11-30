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
    public class ConcursoController : Controller
    {
        // GET: BackEnd/Concurso
        private Concurso concurso = new Concurso();
        // GET: TipoUsuario

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(concurso.Listar());
            }
            else
            {
                return View(concurso.Buscar(criterio));
            }

        }


        public ActionResult Index2(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(concurso.Listar());
            }
            else
            {
                return View(concurso.Buscar(criterio));
            }
        }


        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(concurso.ListarGrilla(grid));
        }
        public ActionResult Ver(int id)
        {
            return View(concurso.Obtener(id));
        }




        public ActionResult Buscar(string criterio)
        {
            return View(
                criterio == null || criterio == "" ? concurso.Listar()
                : concurso.Buscar(criterio)
                );
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            ViewBag.semestre = concurso.listarsemestre();
            ViewBag.plan = concurso.listarplan();
            return View(
                id == 0 ? new Concurso() //para generar un nuevo objeto
                : concurso.Obtener(id) // devuelve un id del semestre
                );
        }

        public ActionResult Guardar(Concurso model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/BackEnd/Concurso");
            }
            else
            {
                return View("~/BackEnd/Views/Concurso/AgregarEditar.cshtml", model);
            }
        }


        public ActionResult Eliminar(int id)
        {
            concurso.concurso_id = id;
            concurso.Eliminar();
            return Redirect("~/BackEnd/Concurso"); //devuelve la vista index
        }
    }
}