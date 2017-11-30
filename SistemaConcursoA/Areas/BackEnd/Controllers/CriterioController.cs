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
    public class CriterioController : Controller
    {
     
        private Criterio criterio = new Criterio();
        // GET: BackEnd/PlanEstudio
        public ActionResult Index()
        {
            ViewBag.concurso = criterio.listarconcurso();
            return View();
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            ViewBag.concurso = criterio.listarconcurso();
            return View(
                id == 0 ? new Criterio()
                : criterio.Obtener(id)
                );
        }

        public ActionResult Guardar(Criterio model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/BackEnd/Criterio");
            }
            else
            {
                return View("~/BackEnd/Views/Criterio/AgregarEditar.cshtml", model);
            }
        }

        public ActionResult Eliminar(int id)
        {
            criterio.criterio_id = id;
            criterio.Eliminar();
            return Redirect("~/BackEnd/Criterio");
        }

        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(criterio.ListarGrilla(grid));
        }

        public ActionResult Ver(int id)
        {
            return View(criterio.Obtener(id));
          //  ViewBag.curso = planestudio.listarcursoplan(id);
          
        }
    }
}