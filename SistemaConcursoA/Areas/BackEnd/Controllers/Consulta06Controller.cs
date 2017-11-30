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
    public class Consulta06Controller : Controller
    {
        Categoria proyectocat = new Categoria();
        Proyecto categoriaa = new Proyecto();
        // GET: BackEnd/Consulta06
        public ActionResult Index(int id = 0, string concurso_id = "", string categoria = "")
        {
            if (concurso_id != "" || categoria != "")
            {
                Session["id_concurso"] = id;
                ViewBag.concurso = categoriaa.listarconcurso();
                ViewBag.categoria = categoriaa.listarcategoriaa(id);
                ViewBag.proyecto = proyectocat.proyectocategoria(concurso_id, categoria);
                ViewBag.proyectocant = proyectocat.proyectocategoriacant(concurso_id, categoria);
                return View();
            }
            else
            {
                Session["id_concurso"] = id;
                ViewBag.concurso = categoriaa.listarconcurso();
                ViewBag.categoria = categoriaa.listarcategoriaa(id);
                return View();
            }
        }
    }
}