using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaConcursoA.Models;

namespace SistemaConcursoA.Areas.FrontEnd.Controllers
{
    public class Consulta03Controller : Controller
    {

        Proyecto proyecto = new Proyecto();
        public ActionResult Index(int id = 0, string concurso_id = "", string categoria = "")
        {
            if (concurso_id != "" || categoria != "")
            {
                Session["id_concurso"] = id;
                ViewBag.categoria = proyecto.listarcategoriaa(id);
                ViewBag.concurso = proyecto.listarconcursoc3();
                ViewBag.proyecto = proyecto.listarproyectoantiguo(concurso_id, categoria);
                return View();
            }
            else
            {
                Session["id_concurso"] = id;
                ViewBag.categoria = proyecto.listarcategoriaa(id);
                ViewBag.concurso = proyecto.listarconcursoc3();
                return View();
            }
        }
    }
}