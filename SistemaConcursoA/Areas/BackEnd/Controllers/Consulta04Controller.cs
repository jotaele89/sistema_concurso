﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaConcursoA.Models;
using SistemaConcursoA.Filters;

namespace SistemaConcursoA.Areas.BackEnd.Controllers
{
    [Autenticado]
    public class Consulta04Controller : Controller
    {
        Categoria jurados = new Categoria();
        Proyecto categoriaa = new Proyecto();
        // GET: BackEnd/Consulta04
        public ActionResult Index(int id = 0, string concurso_id = "", string categoria = "")
        {
            if (concurso_id != "" || categoria != "")
            {
                Session["id_concurso"] = id;
                Session["concurso"] = concurso_id;
                Session["categoria"] = categoria;
                ViewBag.concurso = categoriaa.listarconcurso();
                ViewBag.categoria = categoriaa.listarcategoriaa(id);
                ViewBag.categoriajurado = jurados.listarjuradocategoria2(concurso_id, categoria);
                return View();
            }
            else
            {
                Session["id_concurso"] = id;
                Session["concurso"] = concurso_id;
                Session["categoria"] = categoria;
                ViewBag.concurso = categoriaa.listarconcurso();
                ViewBag.categoria = categoriaa.listarcategoriaa(id);
                return View();
            }
        }
    }
}