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
    public class JuradoController : Controller//
    {
        private Persona jurado = new Persona();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            ViewBag.listarusuario = jurado.listarusuario(id);
            ViewBag.listarpersona = jurado.listarpersona(id);
            return View(
                id == 0 ? new Persona()
                : jurado.ObtenerJurado(id)
                );
        }

        public ActionResult Buscar(string criterio)
        {
            if(criterio != null)
            {
                var resultado = jurado.buscarpersona(criterio);
                if (resultado == null)
                {
                    return Redirect("~/BackEnd/Jurado/Buscar");
                }
                else
                {
                    return Redirect("~/BackEnd/Jurado/AgregarEditar/" + resultado.persona_id);
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Guardar(Persona model, string[] usuario_id = null, string[] nombre_usuario = null, string[] clave = null, string[] estado_usario = null)
        {
            int idtipousuario = jurado.obtenertipousuario();
            ModelState.Remove("tipopersona_id");
            ModelState.Remove("dni");
            ModelState.Remove("codigo");
            ModelState.Remove("email");
            ModelState.Remove("celular");
            if (usuario_id == null)
            {
                    foreach (var c in nombre_usuario)
                    {
                        foreach (var d in clave)
                        {
                            foreach (var e in estado_usario)
                            {
                                model.Usuario.Add(new Usuario { usuario_id = 0, tipousuario = idtipousuario, nombre = c, clave = d, estado = e });
                            }
                        }
                }

                if (ModelState.IsValid)
                {
                    model.GuardarN();
                    return Redirect("~/Backend/Jurado/Index/");
                }
                else
                {
                    return View("~/Backend/Jurado/AgregarEditar.cshtml", model);
                }
            }
            else
            {
                foreach (var a in usuario_id)
                {
                        foreach (var c in nombre_usuario)
                        {
                            foreach (var d in clave)
                            {
                                foreach (var e in estado_usario)
                                {
                                    model.Usuario.Add(new Usuario { usuario_id = Convert.ToInt32(a), tipousuario = idtipousuario, nombre = c, clave = d, estado = e });
                                }
                            }
                        }
                }


                if (ModelState.IsValid)
                {
                    model.GuardarN();
                    return Redirect("~/Backend/Jurado/Index/");
                }
                else
                {
                    return View("~/Backend/Jurado/AgregarEditar.cshtml", model);
                }
            }
        }

        public ActionResult Eliminar(int id)
        {
            jurado.persona_id = id;
            jurado.Eliminar();
            return Redirect("~/BackEnd/Jurado");
        }

        public JsonResult CargarGrillaJurado(AnexGRID grid)
        {
            return Json(jurado.ListarGrillaJurado(grid));
        }

        public ActionResult Ver(int id)
        {
            ViewBag.categoriajurado = jurado.listarcategoriajurado(id);
            ViewBag.usuariojurado = jurado.listarusuariojurado(id);
            return View(jurado.ObtenerJurado(id));
        }
    }
}