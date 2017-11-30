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

    [Table("TipoPersona")]
    public partial class TipoPersona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoPersona()
        {
            Persona = new HashSet<Persona>();
        }

        [Key]
        public int tipopersona_id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(10)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Persona> Persona { get; set; }

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();

                    var query = db.TipoPersona.Where(x => x.tipopersona_id > 0);

                    if (grilla.columna == "tipopersona_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.tipopersona_id)
                                                               : query.OrderBy(x => x.tipopersona_id);
                    }

                    if (grilla.columna == "nombre")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre)
                                                               : query.OrderBy(x => x.nombre);
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
                            query = query.Where(x => x.nombre.Contains(f.valor));
                    }


                    var planestudio = query.Skip(grilla.pagina)
                                      .Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in planestudio
                                   select new
                                   {
                                       s.tipopersona_id,
                                       s.nombre,
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

        public TipoPersona Obtener(int id)
        {
            var tiposs = new TipoPersona();

            try
            {
                using (var db = new ModeloDatos())
                {
                    tiposs = db.TipoPersona.
                        Where(x => x.tipopersona_id == id).
                        SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return tiposs;
        }

        //metodo guardar
        public void Guardar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    if (this.tipopersona_id > 0)
                    {
                        db.Entry(this).State = EntityState.Modified;
                    }
                    //si no existiera
                    else
                    {
                        db.Entry(this).State = EntityState.Added;
                    }
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        //metodo eliminar
        public void Eleminar()
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
