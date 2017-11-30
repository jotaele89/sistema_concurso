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
    public class RangoEvaluacionController : Controller
    {
        private RangoEvaluacion rangoevaluacion = new RangoEvaluacion();
        // GET: TipoUsuario

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                ViewBag.concurso = rangoevaluacion.listarconcurso();
                return View(rangoevaluacion.Listar());
            }
            else {
                return View(rangoevaluacion.Buscar(criterio));
            }

        }

        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(rangoevaluacion.ListarGrilla(grid));
        }
        public ActionResult Ver(int id)
        {
            return View(rangoevaluacion.Obtener(id));
        }

        public ActionResult Buscar(string criterio)
        {
            return View(
                criterio == null || criterio == "" ? rangoevaluacion.Listar()
                : rangoevaluacion.Buscar(criterio)
                );
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            ViewBag.concurso = rangoevaluacion.listarconcurso();
            return View(
                id == 0 ? new RangoEvaluacion() //para generar un nuevo objeto
                : rangoevaluacion.Obtener(id) // devuelve un id del semestre
                );
        }

        public ActionResult Guardar(RangoEvaluacion model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Backend/RangoEvaluacion/Index/");
            }
            else {
                return View("~/Backend/RangoEvaluacion/Index/AgregarEditar.cshtml", model);
            }
        }


        public ActionResult Eliminar(int id)
        {
            rangoevaluacion.rango_id = id;
            rangoevaluacion.Eliminar();
            return Redirect("~/BackEnd/RangoEvaluacion"); //devuelve la vista index
        }


    }
}
