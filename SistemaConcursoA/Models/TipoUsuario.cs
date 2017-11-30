namespace SistemaConcursoA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;
    [Table("TipoUsuario")]
    public partial class TipoUsuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoUsuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        public int tipousuario_id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Column(TypeName = "text")]
        public string descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }

        public List<TipoUsuario> Listar()  //retorna un collection
        {
            var tipousuario = new List<TipoUsuario>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    tipousuario = db.TipoUsuario.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return tipousuario;
        }

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();

                    var query = db.TipoUsuario.Where(x => x.tipousuario_id > 0);

                    if (grilla.columna == "tipousuario_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.tipousuario_id)
                                                               : query.OrderBy(x => x.tipousuario_id);
                    }

                    if (grilla.columna == "nombre")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre)
                                                               : query.OrderBy(x => x.nombre);
                    }

                    if (grilla.columna == "descripcion")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.descripcion)
                                                               : query.OrderBy(x => x.descripcion);
                    }

                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "nombre" && f.valor != "")
                            query = query.Where(x => x.nombre.Contains(f.valor));
                    }


                    var tipousuario = query.Skip(grilla.pagina)
                                      .Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in tipousuario
                                   select new
                                   {
                                       s.tipousuario_id,
                                       s.nombre,
                                       s.descripcion
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
        public TipoUsuario Obtener(int id) //retorna un objeto
        {
            var tipousuario = new TipoUsuario();
            try
            {
                using (var db = new ModeloDatos())
                {
                    tipousuario = db.TipoUsuario
                        .Where(x => x.tipousuario_id == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return tipousuario;

        }

        //metodo buscar

        public List<TipoUsuario> Buscar(string criterio) //retorna collection
        {
            var tipousuario = new List<TipoUsuario>();
            string estadotipousu = "";
            if (criterio == "Activo") estadotipousu = "A";
            if (criterio == "Inactivo") estadotipousu = "I";

            try
            {
                using (var db = new ModeloDatos())
                {
                    tipousuario = db.TipoUsuario
                         .Where(x => x.nombre.Contains(criterio) || x.descripcion.Contains(criterio))

                         .ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return tipousuario;

        }

        //metodo guardar
        public void Guardar()
        {
            try
            {
                using (var db = new ModeloDatos())//si existe solo modifica si no existe solo agrega
                {
                    if (this.tipousuario_id > 0)
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
