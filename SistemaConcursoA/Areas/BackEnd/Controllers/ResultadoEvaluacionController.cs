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
    public class ResultadoEvaluacionController : Controller
    {
        ResultadoEvaluacion evaluacion = new ResultadoEvaluacion();
        Proyecto proyecto = new Proyecto();
        Proyecto categoriaa = new Proyecto();
        // GET: BackEnd/ResultadoEvaluacion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GenerarResultado()
        {
            evaluacion.generarresultados();
            return Redirect("~/Backend/ResultadoEvaluacion/Index");
        }

        public ActionResult VerResultados(int id = 0, string concurso_id = "", string categoria = "")
        {
            if (concurso_id != "" || categoria != "")
            {
                Session["id_concurso"] = id;
                Session["concurso"] = concurso_id;
                Session["categoria"] = categoria;
                ViewBag.concurso = categoriaa.listarconcurso();
                ViewBag.categoria = categoriaa.listarcategoriaa(id);
                ViewBag.proyecto = proyecto.listarproyectoganador(concurso_id, categoria);
                return View();
            }
            else
            {
                Session["id_concurso"] = id;
                Session["concurso"] = concurso_id;
                Session["categoria"] = categoria;
                ViewBag.concurso = categoriaa.listarconcurso();
                ViewBag.categoria = categoriaa.listarcategoriaa(id);
                return View();
            }
        }

        public ActionResult Penalidad(int id)
        {
            return View(evaluacion.obtenerproyecto(id));
        }

        public ActionResult ModificarResultado(ResultadoEvaluacion model)
        {
            ModelState.Remove("categoria_id");
            ModelState.Remove("puntaje");
            if (ModelState.IsValid)
            {
                model.Modificar();
                return Redirect("~/Backend/ResultadoEvaluacion/VerResultados");
            }
            else
            {
                return View("~/Backend/ResultadoEvaluacion/Penalidad.cshtml", model);
            }
        }
    }
}