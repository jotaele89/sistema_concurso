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

    [Table("PlanEstudio")]
    public partial class PlanEstudio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlanEstudio()
        {
            Concurso = new HashSet<Concurso>();
            Curso = new HashSet<Curso>();
        }

        [Key]
        public int plan_id { get; set; }

        public int? semestre_id { get; set; }

        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(10)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Concurso> Concurso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Curso> Curso { get; set; }

        public virtual Semestre Semestre { get; set; }

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();

                    var query = db.PlanEstudio.Include("Semestre").Where(x => x.plan_id > 0);

                    if (grilla.columna == "plan_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.plan_id)
                                                               : query.OrderBy(x => x.plan_id);
                    }

                    if (grilla.columna == "semestre_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.semestre_id)
                                                               : query.OrderBy(x => x.semestre_id);
                    }

                    if (grilla.columna == "nombre")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre)
                                                               : query.OrderBy(x => x.nombre);
                    }

                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "nombre" && f.valor != "")
                            query = query.Where(x => x.semestre_id.ToString() == f.valor);
                    }


                    var planestudio = query.Skip(grilla.pagina)
                                      .Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in planestudio
                                   select new
                                   {
                                       s.plan_id,
                                       s.Semestre.nombre,
                                       nombreplan = s.nombre
                                   }, total);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return grilla.responde();
        }

        public List<Semestre> listarsemestre() //retornar es un Collection
        {
            var semestre = new List<Semestre>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    semestre = db.Semestre.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return semestre;
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


        public PlanEstudio Obtener(int id) //retornar un objeto
        {
            var planestudio = new PlanEstudio();

            try
            {
                using (var db = new ModeloDatos())
                {
                    planestudio = db.PlanEstudio.Include("Semestre").
                        Where(x => x.plan_id == id).
                        SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return planestudio;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    //this.semestre_id es como un "boolean"
                    if (this.plan_id > 0)
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
