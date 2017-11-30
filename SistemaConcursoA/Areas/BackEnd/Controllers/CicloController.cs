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
    public class CicloController : Controller
    {
        private Ciclo ciclo  = new Ciclo();
        // GET: BackEnd/Ciclo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgregarEditar(int id = 0)//int id es el criterio
        {

            return View(
                id == 0 ? new Ciclo()//generar un nuevo semestre
                : ciclo.Obtener(id)//devuelve un registro por el ID
                );
        }

        public ActionResult Guardar(Ciclo model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/BackEnd/Ciclo");//devuelve por default Index
            }
            else
            {
                return View("~/views/BackEnd/Ciclo/AgregarEditar.cshtml", model);
            }

        }

        public ActionResult Eliminar(int id)
        {
            ciclo.ciclo_id = id;
            ciclo.Eleminar();
            return Redirect("~/BackEnd/Ciclo");
        }

        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(ciclo.ListarGrilla(grid));
        }

        public ActionResult Ver(int id)
        {
            return View(ciclo.Obtener(id));
        }
    }
}