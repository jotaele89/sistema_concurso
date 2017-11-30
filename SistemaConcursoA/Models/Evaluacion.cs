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
    using System.Linq.Expressions;

    [Table("Evaluacion")]
    public partial class Evaluacion
    {
        [Key]
        public int evaluacion_id { get; set; }

        public int proyecto_id { get; set; }

        public int persona_id { get; set; }

        public int rango_id { get; set; }

        public int criterio_id { get; set; }

        public virtual Criterio Criterio { get; set; }

        public virtual Persona Persona { get; set; }

        public virtual Proyecto Proyecto { get; set; }

        public virtual RangoEvaluacion RangoEvaluacion { get; set; }

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla, int persona_id)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();

                    var categoriajurado = db.CategoriaJurado.Include("Categoria").Where(x => x.persona_id == persona_id); //obtiene la categoria del jurado

                    List<int> categoria = new List<int>();

                    foreach (var c in categoriajurado)
                    {
                        categoria.Add(Convert.ToInt32(c.categoria_id));
                    }

                    int[] myArray = categoria.ToArray();

                    var query = db.Proyecto.Include("Categoria").Include("Concurso").Where(p => categoria.Contains(p.categoria_id) && !p.estado.Contains("Inactivo"));

                    if (grilla.columna == "proyecto_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.proyecto_id)
                                                               : query.OrderBy(x => x.proyecto_id);
                    }

                    if (grilla.columna == "concurso")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.Concurso.concurso_id)
                                                               : query.OrderBy(x => x.Concurso.concurso_id);
                    }

                    if (grilla.columna == "categoria_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.categoria_id)
                                                               : query.OrderBy(x => x.categoria_id);
                    }

                    if (grilla.columna == "nombre_proyecto")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre_proyecto)
                                                               : query.OrderBy(x => x.nombre_proyecto);
                    }

                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "nombre" && f.valor != "")
                            query = query.Where(x => x.categoria_id.ToString().Contains(f.valor));

                        if (f.columna == "nombre_proyecto" && f.valor != "")
                            query = query.Where(x => x.nombre_proyecto.Contains(f.valor));

                        if (f.columna == "concurso" && f.valor != "")
                            query = query.Where(x => x.Concurso.concurso_id.ToString().Contains(f.valor));
                    }


                    var evaluacion = query.Skip(grilla.pagina)
                                      .Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in evaluacion
                                   select new
                                   {
                                       s.proyecto_id,
                                       concurso = s.Concurso.nombre,
                                       s.Categoria.nombre,
                                       s.nombre_proyecto
                                   }, total);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return grilla.responde();
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

        public List<Criterio> listarcriterio()
        {
            var criterio = new List<Criterio>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    criterio = db.Criterio.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return criterio;
        }

        public List<RangoEvaluacion> listarrangoevaluacion()
        {
            var rangoevaluacion = new List<RangoEvaluacion>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    rangoevaluacion = db.RangoEvaluacion.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return rangoevaluacion;
        }

        public Evaluacion Obtener(int id)
        {
            var evaluacion = new Evaluacion();

            try
            {
                using (var db = new ModeloDatos())
                {
                    evaluacion = db.Evaluacion.Include("Proyecto").
                        Where(x => x.Proyecto.proyecto_id == id).
                        SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return evaluacion;
        }
        public Evaluacion obtenerevaluacion(int id, int person_id)
        {
            var evaluacion = new Evaluacion();

            try
            {
                using (var db = new ModeloDatos())
                {
                    evaluacion = db.Evaluacion.Include("Proyecto").
                        Where(x => x.Proyecto.proyecto_id == id && x.persona_id==person_id).
                        FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return evaluacion;
        }

        public Proyecto obtenerproyecto(int id)
        {
            var proyecto = new Proyecto();

            try
            {
                using (var db = new ModeloDatos())
                {
                    proyecto = db.Proyecto.Include("Curso").
                        Where(x => x.proyecto_id == id).
                        SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyecto;
        }

        public List<Evaluacion> verificarevaluacionexistente(int id, int person_id)
        {
            var existente = new List<Evaluacion>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    existente = db.Evaluacion.
                        Where(x => x.proyecto_id == id && x.persona_id == person_id).
                        ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return existente;
        }

        public Proyecto categoriaproyecto(int id)
        {
            var existente = new Proyecto();

            try
            {
                using (var db = new ModeloDatos())
                {
                    existente = db.Proyecto.Include("Categoria").
                        Where(x => x.proyecto_id == id).
                        SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return existente;
        }

        static Expression<Func<TElement, bool>> BuildContainsExpression<TElement, TValue>(
    Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue> values)
        {
            if (null == valueSelector) { throw new ArgumentNullException("valueSelector"); }
            if (null == values) { throw new ArgumentNullException("values"); }
            ParameterExpression p = valueSelector.Parameters.Single();
            // p => valueSelector(p) == values[0] || valueSelector(p) == ...
            if (!values.Any())
            {
                return e => false;
            }
            var equals = values.Select(value => (Expression)Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue))));
            var body = equals.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal));
            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }

        public void Guardar(string[] criterio, string[] rango, ArrayList eval_id)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    if (eval_id.Count > 0)
                    {
                        for(int i=0; i < criterio.Count(); i++)
                        {
                            db.Database.ExecuteSqlCommand(
                            "update Evaluacion set " +
"proyecto_id=@proyecto_id, rango_id=@rango_id, criterio_id=@criterio_id where evaluacion_id=@evaluacion_id",
                        new SqlParameter("proyecto_id", this.proyecto_id),
                        new SqlParameter("rango_id", rango[i].ToString()),
                        new SqlParameter("criterio_id", criterio[i].ToString()),
                        new SqlParameter("evaluacion_id", eval_id[i].ToString())
                        );
                        }
                    }
                    else
                    {
                        for(int i=0; i < criterio.Count(); i++)
                        {
                            db.Database.ExecuteSqlCommand(
                            "insert Evaluacion values(" +
"@proyecto_id, @persona_id, @rango_id, @criterio_id)",
                        new SqlParameter("proyecto_id", this.proyecto_id),
                        new SqlParameter("persona_id", this.persona_id),
                        new SqlParameter("rango_id", rango[i].ToString()),
                        new SqlParameter("criterio_id", criterio[i].ToString())
                        );
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<ProyectoEstudiante> integrante(int id)
        {
            var integrante = new List<ProyectoEstudiante>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    integrante = db.ProyectoEstudiante.Include("Persona").
                        Where(x => x.proyecto_id == id).
                        ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return integrante;
        }
    }
}
