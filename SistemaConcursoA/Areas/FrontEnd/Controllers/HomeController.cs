using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaConcursoA.Areas.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        // GET: FrontEnd/Home
        public ActionResult Index()
        {
            return View();
        }

        public FileResult Descargar()
        {
            var ruta = Server.MapPath("~/Server/Docs/estructura.docx");
            return File(ruta, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "estructura.docx");
        }
    }
}