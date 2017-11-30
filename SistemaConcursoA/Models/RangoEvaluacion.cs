namespace SistemaConcursoA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;

    [Table("RangoEvaluacion")]
    public partial class RangoEvaluacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RangoEvaluacion()
        {
            Evaluacion = new HashSet<Evaluacion>();
        }

        [Key]
        public int rango_id { get; set; }

        public int concurso_id { get; set; }

        [StringLength(150)]
        public string nombre { get; set; }

        public int puntaje { get; set; }

        public virtual Concurso Concurso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluacion> Evaluacion { get; set; }

        public List<RangoEvaluacion> Listar()  //retorna un collection
        {
            var rangoevaluacion = new List<RangoEvaluacion>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    rangoevaluacion = db.RangoEvaluacion.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rangoevaluacion;
        }

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();

                    var query = db.RangoEvaluacion.Include("Concurso").Where(x => x.rango_id > 0 || x.Concurso.ESTADO.Contains("Activo"));

                    if (grilla.columna == "rango_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.rango_id)
                                                               : query.OrderBy(x => x.rango_id);
                    }

                    if (grilla.columna == "concurso")
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

                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "nombre" && f.valor != "")
                            query = query.Where(x => x.Concurso.concurso_id.ToString().Contains(f.valor));

                    }


                    var rangoevaluacion = query.Skip(grilla.pagina)
                                      .Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in rangoevaluacion
                                   select new
                                   {
                                       s.rango_id,
                                       s.Concurso.nombre,
                                       nombrera = s.nombre,
                                       s.puntaje
                                   }, total);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return grilla.responde();
        }

        //metodo obtener
        public RangoEvaluacion Obtener(int id) //retorna un objeto
        {
            var rangoevaluacion = new RangoEvaluacion();
            try
            {
                using (var db = new ModeloDatos())
                {
                    rangoevaluacion = db.RangoEvaluacion.Include("Concurso")
                        .Where(x => x.rango_id == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rangoevaluacion;

        }

        //metodo buscar

        public List<RangoEvaluacion> Buscar(string criterio) //retorna collection
        {
            var rangoevaluacion = new List<RangoEvaluacion>();
            //  string estadotipousu = "";
            // if (criterio == "Activo") estadotipousu = "A";
            //if (criterio == "Inactivo") estadotipousu = "I";

            try
            {
                using (var db = new ModeloDatos())
                {
                    rangoevaluacion = db.RangoEvaluacion
                         .Where(x => x.nombre.Contains(criterio))

                         .ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rangoevaluacion;

        }

        //metodo guardar
        public void Guardar()
        {
            try
            {
                using (var db = new ModeloDatos())//si existe solo modifica si no existe solo agrega
                {
                    if (this.rango_id > 0)
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

        public List<Concurso> listarconcurso() //retornar es un Collection
        {
            var concurso = new List<Concurso>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    concurso = db.Concurso.Where(x => x.ESTADO.Contains("Activo")).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return concurso;
        }
    }
}
