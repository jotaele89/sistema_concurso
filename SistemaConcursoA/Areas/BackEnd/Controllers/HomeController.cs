using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaConcursoA.Filters;

namespace SistemaConcursoA.Areas.BackEnd.Controllers
{
    [Autenticado]
    public class HomeController : Controller
    {
        // GET: BackEnd/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}