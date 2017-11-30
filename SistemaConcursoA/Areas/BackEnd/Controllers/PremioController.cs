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
    public class PremioController : Controller
    {
        private Premio premio = new Premio();



        // GET: BackEnd/Premio
        public ActionResult Index()
        {
         ViewBag.concurso = premio.listarconcurso();
  
            return View();
        }
        public ActionResult AgregarEditar(int id = 0)
        {
            ViewBag.concurso = premio.listarconcurso();
            ViewBag.categoria = premio.listarcategoria();
            return View(
                id == 0 ? new Premio()
                : premio.Obtener(id)
                );

        }
        public ActionResult Guardar(Premio model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Backend/Premio");

            }
            else
            {
                return View("~/Backend/Views/Premio/AgregarEditar.cshtml", model);
            }
        
                       
        }
        public ActionResult Eliminar(int id)
        {
            premio.premio_id = id;
            premio.Eliminar();
            return Redirect("~/BackEnd/Premio");

        }
        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(premio.ListarGrilla(grid));
        }


        public ActionResult Ver(int id)
        {
            
            return View(premio.Obtener(id));
        }


        
    
   
    


        



    }
}