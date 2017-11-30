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
    public class ProyectoController : Controller//
    {
        private Proyecto proyecto = new Proyecto();
        public ActionResult Index()
        {
            ViewBag.concurso = proyecto.listarconcurso();
            ViewBag.categoria = proyecto.listarcategoria();
            return View();
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            ViewBag.concurso = proyecto.listarconcurso();
            ViewBag.curso = proyecto.listarcurso();
            ViewBag.categoria = proyecto.listarcategoria();
            ViewBag.ordenproyecto = proyecto.listarordenproyecto(id);

            return View(
                id == 0 ? new Proyecto()
                : proyecto.Obtener(id)
                );
        }

        public ActionResult Guardar(Proyecto model, int[] orden = null, string[] orden_hora = null, string[] nfecharegistro = null, string[] nhoraregistro = null, string[] estado_orden = null)
        {
            if (orden != null)
            {
                foreach (var o in orden)
                {
                    foreach (var oh in orden_hora)
                    {
                        foreach (var ord in estado_orden)
                        {
                            model.OrdenProyecto.Add(new OrdenProyecto { proyecto_id = model.proyecto_id, orden = o, horaexposicion = System.TimeSpan.Parse(oh), estado = ord });
                        }
                    }
                }
            }

            if(nfecharegistro != null)
            {
                foreach (var c in nfecharegistro)
                {
                    model.fecharegistro = Convert.ToDateTime(c);
                }
            }

            if(nhoraregistro != null)
            {
                foreach (var c in nhoraregistro)
                {
                    model.horaregistro = Convert.ToDateTime(c);
                }
            }

            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Backend/Proyecto/Index/");
            }
            else
            {
                return View("~/Backend/Categoria/Proyecto/AgregarEditar.cshtml", model);
            }
        }

        public ActionResult Eliminar(int id)
        {
            proyecto.proyecto_id = id;
            proyecto.Eliminar();
            return Redirect("~/BackEnd/Proyecto");
        }

        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(proyecto.ListarGrilla(grid));
        }

        public ActionResult Ver(int id)
        {
            ViewBag.ordenproyecto = proyecto.listarordenproyecto(id);
            ViewBag.proyectoparticipante = proyecto.listarproyectoparticipante(id);
            ViewBag.documentoproyecto = proyecto.listardocumento(id);
            return View(proyecto.Obtener(id));
        }

        public FileResult Descargar(string nombrearchivo)
        {
            var ruta = Server.MapPath("~/Server/Docs/" + nombrearchivo + ".rar");
            return File(ruta, "application/x-rar-compressed", nombrearchivo + ".rar");
        }
    }
}