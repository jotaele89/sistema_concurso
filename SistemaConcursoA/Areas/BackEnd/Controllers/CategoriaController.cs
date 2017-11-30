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
    public class CategoriaController : Controller//
    {
        private Categoria categoria = new Categoria();
        public ActionResult Index()
        {
            ViewBag.concurso = categoria.listarconcurso();
            return View();
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            ViewBag.jurado = categoria.listarjurado();
            ViewBag.categoriajurado = categoria.listarjuradocategoria(id);
            ViewBag.concurso = categoria.listarconcurso();
            return View(
                id == 0 ? new Categoria()
                : categoria.Obtener(id)
                );
        }

        public ActionResult Guardar(Categoria model, int[] jurado = null, string[] nuevo = null)
        {
            string g = "";
            if (nuevo != null)
            {
                foreach (var n in nuevo)
                {
                    g = n;
                }
            }

            if (g.Equals("si"))
            {
                //para guardar en otra tabla que tiene relacion
                if (jurado != null)
                {
                    foreach (var d in jurado)
                        model.CategoriaJurado.Add(new CategoriaJurado { categoria_id = model.categoria_id, persona_id = d });
                }
                //fin

                if (ModelState.IsValid)
                {
                    model.GuardarNuevo();
                    return Redirect("~/Backend/Categoria/Index/");
                }
                else
                {
                    return View("~/Backend/Categoria/Index/AgregarEditar.cshtml", model);
                }
            }
            else
            {
                if (jurado != null)
                {
                    foreach (var d in jurado)
                        model.CategoriaJurado.Add(new CategoriaJurado { categoria_id = model.categoria_id, persona_id = d });
                }

                if (ModelState.IsValid)
                {
                    model.GuardarModificar();
                    return Redirect("~/Backend/Categoria/Index/");
                }
                else
                {
                    return View("~/Backend/Categoria/Index/AgregarEditar.cshtml", model);
                }
            }
        }

        public ActionResult Eliminar(int id)
        {
            categoria.categoria_id = id;
            categoria.Eliminar();
            return Redirect("~/BackEnd/Categoria");
        }

        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(categoria.ListarGrilla(grid));
        }

        public ActionResult Ver(int id)
        {
            ViewBag.categoriajurado = categoria.listarjuradocategoria(id);
            return View(categoria.Obtener(id));
        }
    }
}