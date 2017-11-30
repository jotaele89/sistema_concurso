using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaConcursoA.Models;

namespace SistemaConcursoA.Areas.FrontEnd.Controllers
{
    public class ProyectoController : Controller
    {
        private Proyecto proyecto = new Proyecto();
        private Documento documento = new Documento();
        private Persona persona = new Persona();
        private ProyectoEstudiante proyectoestudiante = new ProyectoEstudiante();


        // GET: FrontEnd/Proyecto
        public ActionResult Index()
        {
       
            return View();
        }
        public ActionResult Error()
        {

            return View();
        }

        public ActionResult Registrar(int id = 0)

        {
            Session["id_concurso"] = id;
            ViewBag.concurso = proyecto.listarconcursofe();

            var concurso = proyecto.listarconcursofe();

            ViewBag.curso = proyecto.listarcursoo();
            ViewBag.categoria = proyecto.listarcategoriaa(id);

            return View(new Proyecto());
        }

        public ActionResult Agregar(Proyecto model, string[] categoria_id = null)
        {
            System.DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            //string fecha = dt.ToString("yyyyMMdd");
            System.DateTime dtt = Convert.ToDateTime(DateTime.Now.ToShortTimeString());

            Concurso profi = new Concurso();
            profi = proyecto.obtenerfecha(model.concurso_id);
            if (dt >= profi.fechaconcursoinicioregistro && dt <= profi.fechaconcursofinregistro)
            {

                DateTime hora1 = DateTime.Parse(profi.horainicioregistro.ToString());

                DateTime hora2 = DateTime.Parse(profi.horafinregistro.ToString());

                if (dtt.TimeOfDay >= hora1.TimeOfDay && dtt.TimeOfDay <= hora2.TimeOfDay)
                {
                    var cate = proyecto.listarcategoria2(model.concurso_id);

                    foreach (var i in cate)
                    {
                        foreach (var c in categoria_id)
                        {
                            if (c.ToString().Equals(i.nombre))
                            {
                                model.categoria_id = i.categoria_id;
                                break;
                            }
                        }
                    }

                    ModelState.Remove("estado");
                    ModelState.Remove("fecharegistro");
                    ModelState.Remove("horaregistro");
                    ModelState.Remove("Categoria");
                    ModelState.Remove("Concurso");
                    ModelState.Remove("Curso");

                    if (model.curso_cod == null || model.curso_cod == "")
                    {
                        model.curso_cod = "Libre";
                    }

                    model.AgregarPP();
                    return Redirect("~/FrontEnd/Proyecto/Participante/");
                }
                else { return Redirect("~/FrontEnd/Proyecto/Error/"); }
            }
            else
            {
                return Redirect("~/FrontEnd/Proyecto/Error/");
            }
        }



        public ActionResult Participante(int id = 0)
        {
            Session["codproyecto"] = proyectoestudiante.listarcodproyecto();

            ViewBag.proyecto = proyectoestudiante.listarproyecto(Convert.ToInt32(Session["codproyecto"]));
            ViewBag.persona = proyectoestudiante.listarintegrantes(Convert.ToInt32(Session["codproyecto"].ToString()));

            return View(
                id == 0 ? new ProyectoEstudiante()
                : proyectoestudiante.Obtener(id)
                );
        }

        public ActionResult EliminarIntegrante(int id)
        {
            Session["codproyecto"] = proyectoestudiante.listarcodproyecto();

            proyectoestudiante.eliminarintregrante(Convert.ToInt32(Session["codproyecto"].ToString()),id);
            return Redirect("~/FrontEnd/Proyecto/Participante/");
        }

        public ActionResult Buscar(string criterio)
        {
            return View(
              criterio == null || criterio == "" ? persona.Listar()
              : persona.Buscar(criterio)
              );
        }
        public ActionResult Guardarp(ProyectoEstudiante model)
        {

            if (ModelState.IsValid)
            {
                model.Guardarp();
                return Redirect("~/FrontEnd/Proyecto/Participante/");
            }
            else
            {
                ViewBag.showSuccessAlert = true;
                return View("~/FrontEnd/Views/Proyecto/Registrar.cshtml", model);
            }

        }

        public ActionResult GuardarParticipante(string codigo)
        {

            proyectoestudiante.GuardarIntegrante(codigo, Session["codproyecto"].ToString());
            return Redirect("~/FrontEnd/Proyecto/Participante/");

        }
        //tabla participante
        public ActionResult Eliminar(int id)
        {
            proyectoestudiante.proyectoestudiante_id = id;
            proyectoestudiante.Eliminar();
            return Redirect("~/Frontend/Proyecto/Participante/");
        }

        public JsonResult CargarGrilla(int id, AnexGRID grid)
        {
            return Json(proyectoestudiante.ListarGrilla(id, grid));
        }


        //documento

        public ActionResult Archivo(int id = 0)
        {

            ViewBag.proyecto = proyectoestudiante.listarproyecto(Convert.ToInt32(Session["codproyecto"]));


            return View(
                id == 0 ? new Documento()
                : documento.Obtener(id)
                );
        }



        public JsonResult GuardarDocumento(Documento model,HttpPostedFileBase rar)
        {


            var rm = new ResponseModel();

            //retirar para que no se actualicen

            ModelState.Remove("nombre");
            ModelState.Remove("extension");
            ModelState.Remove("tamanio");
            ModelState.Remove("estado");

            if (ModelState.IsValid)
            {
                rm = model.GuardarDocumento(rar);
            }

            var cantidad = proyecto.cantidadparticipante(model.proyecto_id);

            int cant = 0;

            foreach(var c in cantidad)
            {
                cant++;
            }

            if(cant<2 || cant>4)
            {
                rm.SetResponse(false, "¡Debe ingresar 2 a 4 participantes!");
                return Json(rm);
            }
            else
            {
                if (rm.response)
                {
                    rm.href = Url.Content("/Frontend/Home/Index");
                    return Json(rm);
                }
                else
                {
                    rm.SetResponse(false, "¡Ocurrió un error!");
                    return Json(rm);
                }
            }

        }

        //eliminar proyecto
        public ActionResult EliminarProyecto(int id)
        {
            proyecto.EliminarRegistro(id);
            return Redirect("~/Frontend/Home/");
        }
    }
}