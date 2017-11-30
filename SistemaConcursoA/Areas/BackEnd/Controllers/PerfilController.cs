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
    public class PerfilController : Controller
    {
        private Usuario usuario = new Usuario();
        // GET: Perfil
        public ActionResult Index()
        {
            return View(usuario.Obtener(SessionHelper.GetUser())); //le vamos a pasar el id del usuario que esta logueado
        }

        public JsonResult Actualizar(Usuario model, HttpPostedFileBase Foto)
        {
            var rm = new ResponseModel();

            //retirar para que no se actualicen
            ModelState.Remove("usuario_id");
            ModelState.Remove("persona_id");
            ModelState.Remove("tipousuario");
            ModelState.Remove("clave");
            ModelState.Remove("estado");


            if (ModelState.IsValid)
            {
                rm = model.GuardarPerfil(Foto);
            }
            rm.href = Url.Content("/BackEnd/Home/Index");
            return Json(rm);
        }
    }
}