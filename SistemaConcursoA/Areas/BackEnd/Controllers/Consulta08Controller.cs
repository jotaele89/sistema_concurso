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
    public class Consulta08Controller : Controller
    {
        // GET: BackEnd/Consulta08
        private Concurso concurso = new Concurso();
        private Proyecto proyecto = new Proyecto();

        // GET: Consulta08
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
                    if (grafico.Equals("gpiramide"))
                    {
                        Session["grafico"] = "gpiramide";
                        return View(concurso.ConsultaTotalProyecto());
                    }
                    else if (grafico.Equals("gmcirculo"))
                    {
                        Session["grafico"] = "gmcirculo";
                        return View(concurso.ConsultaTotalProyecto());
                    }
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