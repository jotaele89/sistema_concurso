namespace SistemaConcursoA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections;

    [Table("Concurso")]
    public partial class Concurso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Concurso()
        {
            Categoria = new HashSet<Categoria>();
            Criterio = new HashSet<Criterio>();
            Premio = new HashSet<Premio>();
            Proyecto = new HashSet<Proyecto>();
            RangoEvaluacion = new HashSet<RangoEvaluacion>();
            ResultadoGanadorCategoria = new HashSet<ResultadoGanadorCategoria>();
        }

        [Key]
        public int concurso_id { get; set; }

        public int semestre_id { get; set; }

        public int plan_id { get; set; }

        [Required]
        [StringLength(150)]
        public string nombre { get; set; }

        [Column(TypeName = "text")]
        public string descripcion { get; set; }

        [Column(TypeName = "date")]
        public DateTime fechaconcurso { get; set; }

        public TimeSpan horainicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime fechaconcursoinicioregistro { get; set; }

        [Column(TypeName = "date")]
        public DateTime fechaconcursofinregistro { get; set; }

        public TimeSpan horainicioregistro { get; set; }

        public TimeSpan horafinregistro { get; set; }

        [StringLength(10)]
        public string tiempoexposicion { get; set; }

        [Required]
        [StringLength(10)]
        public string ESTADO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Categoria> Categoria { get; set; }

        public virtual PlanEstudio PlanEstudio { get; set; }

        public virtual Semestre Semestre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Criterio> Criterio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Premio> Premio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proyecto> Proyecto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RangoEvaluacion> RangoEvaluacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResultadoGanadorCategoria> ResultadoGanadorCategoria { get; set; }

        public List<Concurso> Listar() //retornar es un Collection
        {
            var concurso = new List<Concurso>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    concurso = db.Concurso.Include("Semestre").Include("PlanEstudio").ToList(); //relacionando la tabla usuario con tabla persona
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return concurso;
        }

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {

                    grilla.Inicializar();
                    var query = db.Concurso.Include("Semestre").Include("PlanEstudio").Where(x => x.semestre_id > 0).Where(x => x.plan_id > 0);

                    //ordenar las columnas a mostrar

                    if (grilla.columna == "concurso_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.concurso_id)
                                                                : query.OrderBy(x => x.concurso_id);
                    }
                    if (grilla.columna == "semestre_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.semestre_id)
                                                                : query.OrderBy(x => x.semestre_id);
                    }
                    if (grilla.columna == "plan_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.plan_id)
                                                                : query.OrderBy(x => x.plan_id);
                    }
                    if (grilla.columna == "nombre")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre)
                                                                : query.OrderBy(x => x.nombre);
                    }

                    if (grilla.columna == "fechaconcursoinicioregistro")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.fechaconcursoinicioregistro)
                                                                : query.OrderBy(x => x.fechaconcursoinicioregistro);
                    }

                    if (grilla.columna == "fechaconcursofinregistro")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.fechaconcursofinregistro)
                                                                : query.OrderBy(x => x.fechaconcursofinregistro);
                    }

                    if (grilla.columna == "ESTADO")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.ESTADO)
                                                                : query.OrderBy(x => x.ESTADO);
                    }



                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "nombre" && f.valor != "")
                            query = query.Where(x => x.nombre.Contains(f.valor));
                    }


                    var concurso = query.Skip(grilla.pagina)
                                        .Take(grilla.limite)
                                        .ToList();
                    var total = query.Count();
                    grilla.SetData(
                        from s in concurso
                        select new
                        {
                            s.concurso_id,
                            nombresemestre = s.Semestre.nombre,
                            nombreplan = s.PlanEstudio.nombre,
                            s.nombre,

                            s.fechaconcursoinicioregistro,
                            s.fechaconcursofinregistro,

                            s.ESTADO

                        },
                        total
                        );

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return grilla.responde();
        }



        public Concurso Obtener(int id) //retornar un objeto
        {
            var concurso = new Concurso();

            try
            {
                using (var db = new ModeloDatos())
                {
                    concurso = db.Concurso.Include("Semestre").Include("PlanEstudio") //relacionando la tabla usuario con tabla persona
                    .Where(x => x.concurso_id == id)
                    .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return concurso;
        }

        public List<Concurso> Buscar(string criterio)
        {
            var concurso = new List<Concurso>();

            string estadosem = "";

            if (criterio.Equals("Activo"))
            {
                estadosem = "A";
            }

            if (criterio.Equals("Inactivo"))
            {
                estadosem = "I";
            }

            try
            {
                using (var db = new ModeloDatos())
                {
                    concurso = db.Concurso
                        .Where(x => x.nombre.Contains(criterio) || x.ESTADO.Contains(criterio))
                        //|| x.estado = estadosem
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return concurso;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    //this.semestre_id es como un "boolean"
                    if (this.concurso_id > 0)
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

        public List<PlanEstudio> listarplan()
        {
            var plan = new List<PlanEstudio>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    plan = db.PlanEstudio.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return plan;
        }

        //consulta04graficos
        public List<Categoria> consulta04graficobarra()
        {
            List<Categoria> concurso = new List<Categoria>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    concurso = db.Categoria.Include(x => x.Concurso).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return concurso;
        }

        public List<ResultadoGanadorCategoria> listarpuntaje()
        {
            List<ResultadoGanadorCategoria> puntaje = new List<ResultadoGanadorCategoria>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    puntaje = db.ResultadoGanadorCategoria.Include("Categoria").OrderBy(x => x.concurso_id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return puntaje;
        }

        public List<Concurso> listarconcurso()
        {
            List<Concurso> concurso = new List<Concurso>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    concurso = db.Concurso.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return concurso;
        }

        public ArrayList listarcategorias()
        {
            var cat = new ArrayList();
            try
            {
                using (var db = new ModeloDatos())
                {
                    var resultado = from Categoria in db.Categoria
                                    group Categoria by new
                                    {
                                        Categoria.nombre
                                    } into g
                                    select new
                                    {
                                        g.Key.nombre,
                                        cantidad = g.Count()
                                    };

                    foreach (var r in resultado)
                    {
                        cat.Add(r.nombre);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return cat;
        }

        //consulta08graficos

        public string[,] ConsultaTotalProyecto()
        {
            string[,] proyectos;
            try
            {
                using (var db = new ModeloDatos())
                {
                    var con = (from p in db.Concurso
                               join c in db.Proyecto on p.concurso_id equals c.concurso_id into r_join
                               from c in r_join.DefaultIfEmpty()
                               group new { p, c } by new
                               {
                                   p.concurso_id,
                                   p.nombre
                               } into g
                               select new
                               {
                                   id_curso = (int?)g.Key.concurso_id,
                                   g.Key.nombre,
                                   Cantidad = g.Count(p => p.c.concurso_id != null)
                               }).ToList();
                    proyectos = new string[con.Count(), 2];
                    int count = 0;
                    foreach (var p in con)
                    {
                        proyectos[count, 0] = Convert.ToString(p.nombre);
                        proyectos[count, 1] = Convert.ToString(p.Cantidad);
                        count++;
                    }
                }
                return proyectos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //consulta10
        public List<Categoria> consulta10()
        {
            List<Categoria> concurso = new List<Categoria>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    concurso = db.Categoria.Include(x => x.Concurso).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return concurso;
        }

        public List<Categoria> consulta102()
        {
            List<Categoria> concurso = new List<Categoria>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    concurso = db.Categoria.Include(x => x.Concurso).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return concurso;
        }
        public int obtenercantcategoria(int semestre, int categoria)
        {
            List<Categoria> proyecto = new List<Categoria>();
            int cancat = 0;
            try
            {
                using (var db = new ModeloDatos())
                {
                    proyecto = db.Categoria.Include("Concurso").Where(x => x.Concurso.semestre_id == semestre && x.categoria_id == categoria).ToList();
                    cancat = proyecto.Count;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cancat;
        }

        public int obtenercantproy(int semestre, int categoria)
        {
            List<Proyecto> proyecto = new List<Proyecto>();
            int cantidad = 0;
            try
            {
                using (var db = new ModeloDatos())
                {
                    proyecto = db.Proyecto.Include("Concurso").Where(x => x.Concurso.semestre_id == semestre && x.categoria_id == categoria).ToList();
                    cantidad = proyecto.Count;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cantidad;
        }
    }
}
