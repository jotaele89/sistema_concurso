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
    public class Consulta09Controller : Controller
    {
        // GET: BackEnd/Consulta09
        private Persona persona = new Persona();
        private TipoPersona tipopersona = new TipoPersona();

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
                    if (grafico.Equals("gpiramidei"))
                    {
                        Session["grafico"] = "gpiramidei";
                        return View(persona.ConsultaTotalPersona());
                    }
                    else if (grafico.Equals("gbb"))
                    {
                        Session["grafico"] = "gbb";
                        return View(persona.ConsultaTotalPersona());
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