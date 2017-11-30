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

    [Table("ResultadoEvaluacion")]
    public partial class ResultadoEvaluacion
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int proyecto_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int categoria_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int puntaje { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int penalidad { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual Proyecto Proyecto { get; set; }

        public void generarresultados()
        {
            var resultado = new List<Evaluacion>();
            var proyecto = new List<Proyecto>();
            double puntaje = 0;
            int penalidad = 0;

            try
            {
                using (var db = new ModeloDatos())
                {

                    db.Database.ExecuteSqlCommand(
                        "delete from ResultadoEvaluacion"
                        );

                    var flag = false;
                    resultado = db.Evaluacion.Include("Persona").Include("RangoEvaluacion").ToList();
                    proyecto = db.Proyecto.ToList();
                    foreach (var i in proyecto)
                    {
                        puntaje = 0;
                        penalidad = 0;
                        foreach (var c in resultado)
                        {
                            if(i.proyecto_id==c.proyecto_id)
                            {
                                flag = true;
                                puntaje = puntaje + Convert.ToDouble(c.RangoEvaluacion.puntaje);
                            }
                        }
                        if(flag)
                        {
                            db.Database.ExecuteSqlCommand(
                        "insert into ResultadoEvaluacion values(@proyecto_id,@categoria_id,@puntaje,@penalidad)",
                        new SqlParameter("proyecto_id", i.proyecto_id),
                        new SqlParameter("categoria_id", i.categoria_id),
                        new SqlParameter("puntaje", (puntaje/3)),
                        new SqlParameter("penalidad", penalidad)
                        );
                            flag = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<ResultadoEvaluacion> listarproyecto(string criterio)
        {
            var proyecto = new List<ResultadoEvaluacion>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    if (criterio == null || criterio == "")
                    {
                        proyecto = db.ResultadoEvaluacion.Include("Proyecto").Include("Categoria").ToList();
                    }
                    else
                    {
                        proyecto = db.ResultadoEvaluacion.Include("Proyecto").Include("Categoria").Where(x => x.categoria_id.ToString().Contains(criterio)).ToList();
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

        public ResultadoEvaluacion obtenerproyecto(int id)
        {
            var proyecto = new ResultadoEvaluacion();

            try
            {
                using (var db = new ModeloDatos())
                {
                    proyecto = db.ResultadoEvaluacion.Include("Proyecto").Where(x => x.proyecto_id == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyecto;
        }

        public void Modificar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    db.Database.ExecuteSqlCommand(
                        "update ResultadoEvaluacion set penalidad = @penalidad where proyecto_id = @proyecto_id",
                        new SqlParameter("penalidad", this.penalidad),
                        new SqlParameter("proyecto_id", this.proyecto_id)
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
