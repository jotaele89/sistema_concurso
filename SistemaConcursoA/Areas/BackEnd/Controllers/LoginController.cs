using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaConcursoA.Models;
using SistemaConcursoA.Filters;

namespace SistemaConcursoA.Areas.BackEnd.Controllers
{
    public class LoginController : Controller
    {
        private Usuario usuario = new Usuario();

        [NoLogin]

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Validar(string Usuario, string Password)
        {
            var rm = usuario.ValidarLogin(Usuario, Password);

            if (rm.response)
            {
                rm.href = Url.Content("/BackEnd/Home/Index");
            }

            return Json(rm);
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();

            return Redirect("~/Backend/Login");
        }
    }
}