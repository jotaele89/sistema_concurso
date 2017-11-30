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
    public class Consulta02Controller : Controller
    {
        Proyecto proyecto = new Proyecto();
        // GET: BackEnd/Consulta02
        public ActionResult Index(int id = 0, string concurso_id = "", string categoria = "")
        {
            if (concurso_id != "" || categoria != "")
            {
                Session["id_concurso"] = id;
                ViewBag.categoria = proyecto.listarcategoriaa(id);
                ViewBag.concurso = proyecto.listarconcurso();
                ViewBag.proyecto = proyecto.listarproyecto(concurso_id, categoria);
                return View();
            }
            else
            {
                Session["id_concurso"] = id;
                ViewBag.categoria = proyecto.listarcategoriaa(id);
                ViewBag.concurso = proyecto.listarconcurso();
                return View();
            }
        }
    }
}