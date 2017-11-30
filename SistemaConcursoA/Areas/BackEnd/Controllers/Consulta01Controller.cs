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
    public class Consulta01Controller : Controller
    {
        Proyecto proyecto = new Proyecto();
        // GET: BackEnd/Consulta01
        public ActionResult Index()
        {
            ViewBag.concurso = proyecto.listarconcurso();
            ViewBag.categoria = proyecto.listarcategoria();
            return View();
        }

        public ActionResult Ver(int id)
        {
            ViewBag.proyecto=proyecto.Obtener(id);
            ViewBag.docente=proyecto.docente(id);
            ViewBag.integrantes=proyecto.integrantes(id);
            return View();
        }

        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(proyecto.Consulta01(grid));
        }
    }
}