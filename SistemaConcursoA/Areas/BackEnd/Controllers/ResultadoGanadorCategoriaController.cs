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
    public class ResultadoGanadorCategoriaController : Controller
    {
        ResultadoGanadorCategoria ganador = new ResultadoGanadorCategoria();
        Proyecto proyecto = new Proyecto();
        // GET: BackEnd/ResultadoGanadorCategoria

        public ActionResult Index(int id = 0, string concurso_id = "", string categoria = "")
        {
            return View();
        }

        public ActionResult GenerarResultado()
        {
            var concurso = ganador.listarconcursoactual();
            ganador.generarresultados(concurso);
            return Redirect("~/Backend/ResultadoGanadorCategoria/Index");
        }

        public ActionResult VerResultados(int id = 0, string concurso_id = "", string categoria = "")
        {
            if (concurso_id != "" || categoria != "")
            {
                Session["id_concurso"] = id;
                Session["concurso"] = concurso_id;
                Session["categoria"] = categoria;
                ViewBag.concurso = proyecto.listarconcurso();
                ViewBag.categoria = proyecto.listarcategoriaa(id);
                ViewBag.proyecto = proyecto.listarproyectoganador(concurso_id, categoria);
                return View();
            }
            else
            {
                Session["id_concurso"] = id;
                Session["concurso"] = concurso_id;
                Session["categoria"] = categoria;
                ViewBag.concurso = proyecto.listarconcurso();
                ViewBag.categoria = proyecto.listarcategoriaa(id);
                return View();
            }
        }
    }
}