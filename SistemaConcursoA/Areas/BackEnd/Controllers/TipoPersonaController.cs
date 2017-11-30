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
    public class TipoPersonaController : Controller
    {
        // GET: TipoPersona
        private TipoPersona tipopp = new TipoPersona();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgregarEditar(int id = 0)//int id es el criterio
        {

            return View(
                id == 0 ? new TipoPersona()//generar un nuevo semestre
                : tipopp.Obtener(id)//devuelve un registro por el ID
                );
        }

        public ActionResult Guardar(TipoPersona model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/BackEnd/TipoPersona");//devuelve por default Index
            }
            else
            {
                return View("~/views/BackEnd/TipoPersona/AgregarEditar.cshtml", model);
            }

        }

        public ActionResult Eliminar(int id)
        {
            tipopp.tipopersona_id = id;
            tipopp.Eleminar();
            return Redirect("~/BackEnd/TipoPersona");
        }

        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(tipopp.ListarGrilla(grid));
        }

        public ActionResult Ver(int id)
        {
            return View(tipopp.Obtener(id));
        }
    }
}