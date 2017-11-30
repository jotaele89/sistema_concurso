namespace SistemaConcursoA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;

    [Table("Criterio")]
    public partial class Criterio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Criterio()
        {
            Evaluacion = new HashSet<Evaluacion>();
        }

        [Key]
        public int criterio_id { get; set; }

        public int concurso_id { get; set; }

        [StringLength(150)]
        public string nombre { get; set; }

        public int puntaje { get; set; }

        [StringLength(10)]
        public string estado { get; set; }

        public virtual Concurso Concurso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluacion> Evaluacion { get; set; }

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();

                    var query = db.Criterio.Include("Concurso").Where(x => x.criterio_id > 0 || x.Concurso.ESTADO.Contains("Activo"));

                    if (grilla.columna == "criterio_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.criterio_id)
                                                               : query.OrderBy(x => x.criterio_id);
                    }

                    if (grilla.columna == "concurso_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.concurso_id)
                                                               : query.OrderBy(x => x.concurso_id);
                    }

                    if (grilla.columna == "nombre")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre)
                                                               : query.OrderBy(x => x.nombre);
                    }

                    if (grilla.columna == "puntaje")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.puntaje)
                                                               : query.OrderBy(x => x.puntaje);
                    }

                    if (grilla.columna == "estado")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.estado)
                                                               : query.OrderBy(x => x.estado);
                    }

                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "nombre" && f.valor != "")
                            query = query.Where(x => x.Concurso.concurso_id.ToString().Contains(f.valor));
                    }


                    var planestudio = query.Skip(grilla.pagina)
                                      .Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in planestudio
                                   select new
                                   {
                                       s.criterio_id,
                                       s.Concurso.nombre,

                                       nombrecriterio = s.nombre,
                                       s.puntaje,
                                       s.estado
                                   }, total);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return grilla.responde();
        }

        public List<Concurso> listarconcurso() //retornar es un Collection
        {
            var concurso = new List<Concurso>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    concurso = db.Concurso.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return concurso;
        }

        public List<Curso> listarcursoplan(int id)
        {
            var curso = new List<Curso>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    curso = db.Curso.Where(x => x.plan_id == id).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return curso;
        }


        public Criterio Obtener(int id) //retornar un objeto
        {
            var criterio = new Criterio();

            try
            {
                using (var db = new ModeloDatos())
                {
                    criterio = db.Criterio.Include("Concurso").
                        Where(x => x.criterio_id == id).
                        SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return criterio;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    //this.semestre_id es como un "boolean"
                    if (this.criterio_id > 0)
                    {
                        db.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added;
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Eliminar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    db.Entry(this).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
