namespace SistemaConcursoA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;

    [Table("Semestre")]
    public partial class Semestre
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Semestre()
        {
            Concurso = new HashSet<Concurso>();
            PlanEstudio = new HashSet<PlanEstudio>();
        }

        [Key]
        public int semestre_id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(4)]
        public string anio { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime fechainicio { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime fechafin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Concurso> Concurso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlanEstudio> PlanEstudio { get; set; }

        public List<Semestre> Listar()  //retorna un collection
        {
            var semestres = new List<Semestre>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    semestres = db.Semestre.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return semestres;
        }

        //crear metodo listar grilla de anexgrid

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();
                    var query = db.Semestre.Where(x => x.semestre_id > 0);

                    //ordenar las columnas a mostrar

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
                    if (grilla.columna == "anio")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.anio)
                                                                : query.OrderBy(x => x.anio);
                    }
                    if (grilla.columna == "fechainicio")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.fechainicio)
                                                                : query.OrderBy(x => x.fechainicio);
                    }
                    if (grilla.columna == "fechafin")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.fechafin)
                                                                : query.OrderBy(x => x.fechafin);
                    }

                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "nombre" && f.valor != "")
                            query = query.Where(x => x.nombre.Contains(f.valor));
                    }


                    var semestre = query.Skip(grilla.pagina)
                                        .Take(grilla.limite)
                                        .ToList();
                    var total = query.Count();
                    grilla.SetData(
                        from s in semestre
                        select new
                        {
                            s.semestre_id,
                            s.nombre,
                            s.anio,
                            s.fechainicio,
                            s.fechafin
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


        //metodo obtener
        public Semestre Obtener(int id) //retorna un objeto
        {
            var semestre = new Semestre();
            try
            {
                using (var db = new ModeloDatos())
                {
                    semestre = db.Semestre
                        .Where(x => x.semestre_id == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return semestre;

        }

        //metodo buscar

        public List<Semestre> Buscar(string criterio) //retorna collection
        {
            var semestres = new List<Semestre>();
            // string estadosem = "";
            //if (criterio == "Activo") estadosem = "A";
            //if (criterio == "Inactivo") estadosem = "I";

            try
            {
                using (var db = new ModeloDatos())
                {
                    semestres = db.Semestre
                         .Where(x => x.nombre.Contains(criterio) || x.anio.Contains(criterio))
                         //|| x.estado=estadosem
                         .ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return semestres;

        }

        //metodo guardar
        public void Guardar()
        {
            try
            {
                using (var db = new ModeloDatos())//si existe solo modifica si no existe solo agrega
                {
                    if (this.semestre_id > 0)
                    {
                        db.Entry(this).State = EntityState.Modified; //si el valor es mayor que cero solo modifica
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added; //si el valor es  cero va a agregar
                    }
                    db.SaveChanges();
                }
            }

            catch (Exception ex)
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
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
