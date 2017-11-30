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
    public class UsuarioController : Controller
    {
        private Usuario usuario = new Usuario();
        // GET: TipoUsuario

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(usuario.Listar());
            }
            else
            {
                return View(usuario.Buscar(criterio));
            }

        }


        public ActionResult Index2(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(usuario.Listar());
            }
            else
            {
                return View(usuario.Buscar(criterio));
            }
        }


        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(usuario.ListarGrilla(grid));
        }
        public ActionResult Ver(int id)
        {
            return View(usuario.Obtener(id));
        }




        public ActionResult Buscar(string criterio)
        {
            return View(
                criterio == null || criterio == "" ? usuario.Listar()
                : usuario.Buscar(criterio)
                );
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            ViewBag.persona = usuario.listarpersona();
            ViewBag.tipousuario = usuario.listartipousuario();
            return View(
                id == 0 ? new Usuario() //para generar un nuevo objeto
                : usuario.Obtener(id) // devuelve un id del semestre
                );
        }

        public ActionResult Guardar(Usuario model)
        {
            if(model.avatar == null || model.avatar == "")
            {
                model.avatar="user_default.png";
            }
            if(model.clave == null || model.clave == "")
            {
                ModelState.Remove("clave");
            }
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/BackEnd/Usuario");
            }
            else
            {
                return View("~/BackEnd/Views/Usuario/AgregarEditar.cshtml", model);
            }
        }


        public ActionResult Eliminar(int id)
        {
            usuario.usuario_id = id;
            usuario.Eliminar();
            return Redirect("~/BackEnd/Usuario"); //devuelve la vista index
        }
    }
}