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
    public class PlanEstudioController : Controller//
    {
        private PlanEstudio planestudio = new PlanEstudio();
        // GET: BackEnd/PlanEstudio
        public ActionResult Index()
        {
            ViewBag.semestre = planestudio.listarsemestre();
            return View();
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            ViewBag.semestre = planestudio.listarsemestre();
            return View(
                id == 0 ? new PlanEstudio()
                : planestudio.Obtener(id)
                );
        }

        public ActionResult Guardar(PlanEstudio model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/BackEnd/PlanEstudio");
            }
            else
            {
                return View("~/BackEnd/Views/PlanEstudio/AgregarEditar.cshtml", model);
            }
        }

        public ActionResult Eliminar(int id)
        {
            planestudio.plan_id = id;
            planestudio.Eliminar();
            return Redirect("~/BackEnd/PlanEstudio");
        }

        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(planestudio.ListarGrilla(grid));
        }

        public ActionResult Ver(int id)
        {
            ViewBag.curso = planestudio.listarcursoplan(id);
            return View(planestudio.Obtener(id));
        }
    }
}