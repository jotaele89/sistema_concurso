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
    public class TipoUsuarioController : Controller
    {
        private TipoUsuario tipousuario = new TipoUsuario();
        // GET: TipoUsuario

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(tipousuario.Listar());
            }
            else {
                return View(tipousuario.Buscar(criterio));
            }

        }

        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(tipousuario.ListarGrilla(grid));
        }
        public ActionResult Ver(int id)
        {
            return View(tipousuario.Obtener(id));
        }

        public ActionResult Buscar(string criterio)
        {
            return View(
                criterio == null || criterio == "" ? tipousuario.Listar()
                : tipousuario.Buscar(criterio)
                );
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new TipoUsuario() //para generar un nuevo objeto
                : tipousuario.Obtener(id) // devuelve un id del semestre
                );
        }

        public ActionResult Guardar(TipoUsuario model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Backend/TipoUsuario/Index/");
            }
            else {
                return View("~/Backend/TipoUsuario/Index/AgregarEditar.cshtml", model);
            }
        }


        public ActionResult Eliminar(int id)
        {
            tipousuario.tipousuario_id = id;
            tipousuario.Eliminar();
            return Redirect("~/BackEnd/TipoUsuario"); //devuelve la vista index
        }


    }
}
