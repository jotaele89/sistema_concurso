using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaConcursoA.Models;

namespace SistemaConcursoA.Areas.BackEnd.Controllers
{
    public class Consulta12Controller : Controller
    {
        Concurso concurso = new Concurso();
        Proyecto categoriaa = new Proyecto();
        public ActionResult Index(string grafico)
        {
            try
            {
                Session["grafico"] = "";
                if (grafico.Equals(""))
                {
                    return View();
                }
                else
                {
                    if (grafico.Equals("gbarra"))
                    {
                        //ViewBag.puntaje = concurso.listarpuntaje();
                        ViewBag.semestre = concurso.listarsemestre();
                        Session["grafico"] = "gbarra";
                        ViewBag.proyecto = categoriaa.listarproyectoo(Session["concurso"].ToString(), Session["categoria"].ToString());
                        return View(concurso.consulta10());
                    }
                    //else if (grafico.Equals("garea"))
                    //{
                    //    ViewBag.puntaje = concurso.listarpuntaje();
                    //    ViewBag.concurso = concurso.listarconcurso();
                    //    Session["grafico"] = "garea";
                    //    return View(concurso.consulta04graficobarra());
                    //}
                    else
                    {
                        return View();
                    }
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}