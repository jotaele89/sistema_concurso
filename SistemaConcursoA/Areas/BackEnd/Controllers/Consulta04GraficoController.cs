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
    public class Consulta04GraficoController : Controller
    {
        Concurso concurso = new Concurso();

        public ActionResult Index(string grafico)
        {
            try
            {
                Session["grafico"]="";
                if (grafico.Equals(""))
                { 
                    return View();
                }
                else
                {
                    if(grafico.Equals("gbarra"))
                    {
                        ViewBag.puntaje = concurso.listarpuntaje();
                        ViewBag.concurso = concurso.listarconcurso();
                        Session["grafico"] = "gbarra";
                        return View(concurso.consulta04graficobarra());
                    }
                    else if(grafico.Equals("garea"))
                    {
                        ViewBag.puntaje = concurso.listarpuntaje();
                        ViewBag.concurso = concurso.listarconcurso();
                        Session["grafico"] = "garea";
                        return View(concurso.consulta04graficobarra());
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            catch(Exception e)
            {
                return View();
            }
        }
    }
}