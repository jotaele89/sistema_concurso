using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaConcursoA.Models;
using SistemaConcursoA.Filters;
using System.Collections;

namespace SistemaConcursoA.Areas.BackEnd.Controllers
{
    [Autenticado]
    public class EvaluacionController : Controller
    {
        Evaluacion evaluacion = new Evaluacion();
        Proyecto proyecto = new Proyecto();
        // GET: BackEnd/Evaluacion
        public ActionResult Index()
        {
            ViewBag.concurso = proyecto.listarconcurso();
            //filtrar que solo vea los proyectos de la categoria asignada al jurado AQUI!
            Usuario usuario = new Usuario().Obtener(SistemaConcursoA.Models.SessionHelper.GetUser());
            Session["persona_id"] = usuario.persona_id;
            ViewBag.categoria = evaluacion.listarcategoria();
            return View();
        }

        public ActionResult Evaluar(int id)
        {
            Session["id_proyecto"] = id.ToString();
            ViewBag.proyecto = evaluacion.obtenerproyecto(id);
            ViewBag.criterio = evaluacion.listarcriterio();
            ViewBag.rangoevaluacion = evaluacion.listarrangoevaluacion();
            var existente = evaluacion.verificarevaluacionexistente(id, Convert.ToInt32(Session["persona_id"]));
            ViewBag.verexiste = evaluacion.verificarevaluacionexistente(id, Convert.ToInt32(Session["persona_id"]));
            Proyecto obtenerproyecto = ViewBag.proyecto;
            Evaluacion obtenerproyectoev = evaluacion.obtenerevaluacion(id, Convert.ToInt32(Session["persona_id"]));
            ViewBag.proyectocategoria = evaluacion.categoriaproyecto(id);
            ViewBag.integrantes = evaluacion.integrante(id);

            if (existente.Count == 0)
            {
                return View(new Evaluacion());
            }
            else
            {
                Evaluacion ev = new Evaluacion();
                ev.proyecto_id = obtenerproyecto.proyecto_id;
                ev.persona_id = obtenerproyectoev.persona_id;
                return View(ev);
            }
        }

        public ActionResult Guardar(Evaluacion model, string[] criterio = null, string[] rangoevaluacion = null)
        {
            var existente = evaluacion.verificarevaluacionexistente(Convert.ToInt32(Session["id_proyecto"]), Convert.ToInt32(Session["persona_id"]));

            ArrayList ex = new ArrayList(); 

            foreach(var c in existente)
            {
                ex.Add(c.evaluacion_id);
            }

            ModelState.Remove("evaluacion_id");
            ModelState.Remove("rango_id");
            ModelState.Remove("criterio_id");
            ModelState.Remove("Criterio");
            ModelState.Remove("Proyecto");
            ModelState.Remove("RangoEvaluacion");
            if (ModelState.IsValid)
            {
                model.Guardar(criterio, rangoevaluacion, ex);
                return Redirect("~/Backend/Evaluacion/Index/");
            }
            else
            {
                return View("~/Backend/Evaluacion/AgregarEditar.cshtml", model);
            }
        }


        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(evaluacion.ListarGrilla(grid, Convert.ToInt32(Session["persona_id"])));//Convert.ToInt32(Session["persona_id"].ToString())));
        }
    }
}