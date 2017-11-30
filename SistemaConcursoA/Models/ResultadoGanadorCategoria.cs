namespace SistemaConcursoA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Collections;

    [Table("ResultadoGanadorCategoria")]
    public partial class ResultadoGanadorCategoria
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int categoria_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int proyecto_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int concurso_id { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int puntaje { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(10)]
        public string estado { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual Concurso Concurso { get; set; }

        public virtual Proyecto Proyecto { get; set; }

        public List<ResultadoEvaluacion> listarproyecto(string criterio)
        {
            var proyecto = new List<ResultadoEvaluacion>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    if (criterio == null || criterio == "")
                    {
                        proyecto = db.ResultadoEvaluacion.Include("Proyecto").Include("Categoria").OrderByDescending(x => (x.puntaje - x.penalidad)).ToList();
                    }
                    else
                    {
                        proyecto = db.ResultadoEvaluacion.Include("Proyecto").Include("Categoria").Where(x => x.categoria_id.ToString().Contains(criterio)).OrderByDescending(x => (x.puntaje - x.penalidad)).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyecto;
        }

        public List<Categoria> listarcategoria()
        {
            var categoria = new List<Categoria>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    categoria = db.Categoria.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return categoria;
        }

        //public void generarresultadosganador()
        //{
        //    var resultado = new List<ResultadoEvaluacion>();
        //    var proyecto = new List<Proyecto>();
        //    int penalidad = 0;

        //    try
        //    {
        //        using (var db = new ModeloDatos())
        //        {

        //            db.Database.ExecuteSqlCommand(
        //                "delete from ResultadoGanadorCategoria"
        //                );

        //            resultado = db.ResultadoEvaluacion.Include("Categoría").Include("Proyecto").ToList();
        //            proyecto = db.Proyecto.ToList();
        //            foreach (var i in proyecto)
        //            {
        //                foreach (var c in resultado)
        //                {
        //                    if (i.proyecto_id == c.proyecto_id)
        //                    {
        //                        if(c.Categoria.nombre.Contains("Categoria A"))
        //                        {
        //                            puntaje = c.puntaje - c.penalidad;
        //                            proy_id = c.proyecto_id;

        //                            if (ja == 1)
        //                            {
        //                                maa = puntaje;
        //                                ja++;
        //                            }

        //                            if (puntaje > maa)
        //                            {
        //                                maa = puntaje;
        //                                pia = proy_id;
        //                                cata = c.categoria_id;
        //                            }

        //                            puntaje = 0;
        //                        }
        //                        if (c.Categoria.nombre.Contains("Categoria B"))
        //                        {
        //                            puntaje = c.puntaje - c.penalidad;
        //                            proy_id = c.proyecto_id;

        //                            if (ja == 1)
        //                            {
        //                                maa = puntaje;
        //                                ja++;
        //                            }

        //                            if (puntaje > maa)
        //                            {
        //                                maa = puntaje;
        //                                pia = proy_id;
        //                                cata = c.categoria_id;
        //                            }

        //                            puntaje = 0;
        //                        }
        //                        if (c.Categoria.nombre.Contains("Categoria C"))
        //                        {
        //                            puntaje = c.puntaje - c.penalidad;
        //                            proy_id = c.proyecto_id;

        //                            if (ja == 1)
        //                            {
        //                                maa = puntaje;
        //                                ja++;
        //                            }

        //                            if (puntaje > maa)
        //                            {
        //                                maa = puntaje;
        //                                pia = proy_id;
        //                                cata = c.categoria_id;
        //                            }

        //                            puntaje = 0;
        //                        }
        //                        if (c.Categoria.nombre.Contains("Categoria D") || c.Categoria.nombre.Contains("Categoria Libre"))
        //                        {
        //                            puntaje = c.puntaje - c.penalidad;
        //                            proy_id = c.proyecto_id;

        //                            if (ja == 1)
        //                            {
        //                                maa = puntaje;
        //                                ja++;
        //                            }

        //                            if (puntaje > maa)
        //                            {
        //                                maa = puntaje;
        //                                pia = proy_id;
        //                                cata = c.categoria_id;
        //                            }

        //                            puntaje = 0;
        //                        }
        //                    }
        //                }
        //            }




        //                db.Database.ExecuteSqlCommand(
        //                "insert into ResultadoEvaluacion values(@proyecto_id,@categoria_id,@puntaje,@penalidad)",
        //                new SqlParameter("proyecto_id", i.proyecto_id),
        //                new SqlParameter("categoria_id", i.categoria_id),
        //                new SqlParameter("puntaje", (puntaje / 3)),
        //                new SqlParameter("penalidad", penalidad)
        //                );
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}

        public List<Concurso> listarconcursoactual()
        {
            var categoria = new List<Concurso>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    categoria = db.Concurso.Where(x => x.ESTADO.Equals("Activo")).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return categoria;
        }

        public void generarresultados(List<Concurso> concurso)
        {
            var proyecto = new List<Proyecto>();
            var categoria = new List<Categoria>();
            double puntaje = 0;

            try
            {
                using (var db = new ModeloDatos())
                {
                    foreach(var c in concurso)
                    {
                        int idcon = c.concurso_id;
                        db.Database.ExecuteSqlCommand(
                                                "delete from ResultadoGanadorCategoria where concurso_id = "
                                                + c.concurso_id);

                        categoria = db.Categoria.Where(x => x.concurso_id == c. concurso_id).ToList();
                        foreach(var s in categoria)
                        {
                            //var resultado = from ResultadoEvaluacion in db.ResultadoEvaluacion
                            //            where
                            //              ResultadoEvaluacion.categoria_id == s.categoria_id &&
                            //              ResultadoEvaluacion.puntaje ==
                            //                (from ResultadoEvaluacion0 in db.ResultadoEvaluacion
                            //                 select new
                            //                 {
                            //                     Column1 = (ResultadoEvaluacion0.puntaje - ResultadoEvaluacion0.penalidad)
                            //                 }).Max(p => p.Column1)
                            //            select new
                            //            {
                            //                proyecto_id = ResultadoEvaluacion.proyecto_id,
                            //                categoria_id = ResultadoEvaluacion.categoria_id,
                            //                puntaje = ResultadoEvaluacion.puntaje,
                            //                penalidad = ResultadoEvaluacion.penalidad
                            //            };

                            var resultado = db.ResultadoEvaluacion.Include("Proyecto").Where(x => x.categoria_id == s.categoria_id && x.Proyecto.concurso_id == c.concurso_id).ToList();

                            int cat = 0;
                            int proy = 0;
                            int punta = 0;

                            foreach (var p in resultado)
                            {
                                cat = p.categoria_id;
                                proy = p.proyecto_id;
                                punta = p.puntaje - p.penalidad;

                                if (resultado.Count() != 0)
                                {
                                    db.Database.ExecuteSqlCommand(
                                "insert into ResultadoGanadorCategoria values(@categoria_id,@proyecto_id,@concurso_id,@puntaje,@estado)",
                                new SqlParameter("categoria_id", cat),
                                new SqlParameter("proyecto_id", proy),
                                new SqlParameter("concurso_id", idcon),
                                new SqlParameter("puntaje", punta),
                                new SqlParameter("estado", "Activo")
                                );
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
