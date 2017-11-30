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

    [Table("Categoria")]
    public partial class Categoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Categoria()
        {
            CategoriaJurado = new HashSet<CategoriaJurado>();
            Premio = new HashSet<Premio>();
            Proyecto = new HashSet<Proyecto>();
            ResultadoEvaluacion = new HashSet<ResultadoEvaluacion>();
            ResultadoGanadorCategoria = new HashSet<ResultadoGanadorCategoria>();
        }

        [Key]
        public int categoria_id { get; set; }

        public int concurso_id { get; set; }

        [StringLength(100)]
        public string nombre { get; set; }

        [Column(TypeName = "text")]
        public string descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoriaJurado> CategoriaJurado { get; set; }

        public virtual Concurso Concurso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Premio> Premio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proyecto> Proyecto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResultadoEvaluacion> ResultadoEvaluacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResultadoGanadorCategoria> ResultadoGanadorCategoria { get; set; }
        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();

                    var query = db.Categoria.Include("Concurso").Where(x => x.categoria_id > 0 || x.Concurso.ESTADO.Contains("Activo"));

                    if (grilla.columna == "categoria_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.categoria_id)
                                                               : query.OrderBy(x => x.categoria_id);
                    }

                    if (grilla.columna == "concurso")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.concurso_id)
                                                               : query.OrderBy(x => x.concurso_id);
                    }

                    if (grilla.columna == "nombrecat")
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
                        if (f.columna == "nombrecat" && f.valor != "")
                            query = query.Where(x => x.nombre.ToString().Contains(f.valor));
                        if (f.columna == "concurso" && f.valor != "")
                            query = query.Where(x => x.Concurso.concurso_id.ToString().Contains(f.valor));
                    }


                    var planestudio = query.Skip(grilla.pagina)
                                      .Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in planestudio
                                   select new
                                   {
                                       s.categoria_id,
                                       concurso = s.Concurso.nombre,
                                       nombrecat = s.nombre,
                                       s.descripcion
                                   }, total);
                }
            }
            catch (Exception )
            {
                throw;
            }

            return grilla.responde();
        }

        public List<CategoriaJurado> listarjuradocategoria(int id)
        {
            var persona = new List<CategoriaJurado>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.CategoriaJurado.Include("Persona").Where(x => x.categoria_id == id).ToList();
                }
            }
            catch (Exception )
            {
                throw;
            }

            return persona;
        }

        public List<Usuario> listarjurado()
        {
            var persona = new List<Usuario>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.Usuario.Include("Persona").Include("TipoUsuario1").Where(x => x.TipoUsuario1.nombre.Contains("Jurado")).ToList();
                }
            }
            catch (Exception )
            {
                throw;
            }

            return persona;
        }

        public Categoria Obtener(int id)
        {
            var categoria = new Categoria();

            try
            {
                using (var db = new ModeloDatos())
                {
                    categoria = db.Categoria.Include("Concurso").
                        Where(x => x.categoria_id == id).
                        SingleOrDefault();
                }
            }
            catch (Exception )
            {
                throw;
            }

            return categoria;
        }

        public void GuardarNuevo()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    var LastRegister = 0;

                    var flag = false;

                    var registro = new CategoriaJurado();

                    if (this.categoria_id == 0)
                    {
                        db.Database.ExecuteSqlCommand(
                            "DELETE FROM CategoriaJurado WHERE categoria_id = @categoria_id",
                            new SqlParameter("categoria_id", this.categoria_id)
                            );

                        LastRegister = db.CategoriaJurado
                                    .OrderByDescending(x => x.categoriajurado_id)
                                    .First().categoriajurado_id;

                        registro = db.CategoriaJurado
                                    .Where(x => x.categoria_id == this.categoria_id).SingleOrDefault();

                        var cursodocente = this.CategoriaJurado;

                        flag = true;
                        this.CategoriaJurado = null;
                        db.Database.ExecuteSqlCommand(
                        "insert into Categoria values(@categoria_id,@nombre,@descripcion)",
                        new SqlParameter("categoria_id", this.categoria_id),
                        new SqlParameter("nombre", this.nombre),
                        new SqlParameter("descripcion", this.descripcion)
                        );
                        this.CategoriaJurado = cursodocente;

                        foreach (var c in this.CategoriaJurado)
                        {
                            db.Database.ExecuteSqlCommand(
                            "insert into CategoriaJurado values(@categoria_id,@persona_id)",
                            new SqlParameter("categoria_id", this.categoria_id),
                            new SqlParameter("persona_id", c.persona_id)
                            );
                        }
                    }
                    else
                    {
                        registro = db.CategoriaJurado
                                    .Where(x => x.categoria_id == this.categoria_id).SingleOrDefault();
                        db.Entry(this).State = EntityState.Added;
                    }

                    if (flag)
                    {

                    }
                    else
                    {
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }
        }

        public void GuardarModificar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    var LastRegister = 0;

                    var flag = false;

                    var registro = new CategoriaJurado();

                    if (this.categoria_id > 0)
                    {
                        if (this.CategoriaJurado != null)
                        {
                            flag = true;
                        }

                        db.Database.ExecuteSqlCommand(
                            "DELETE FROM CategoriaJurado WHERE categoria_id = @categoria_id",
                            new SqlParameter("categoria_id", this.categoria_id)
                            );

                        LastRegister = Convert.ToInt32(db.Database.SqlQuery<decimal>("Select IDENT_CURRENT ('CategoriaJurado')", new object[0]).FirstOrDefault());

                        registro = db.CategoriaJurado
                                    .Where(x => x.categoria_id == this.categoria_id).SingleOrDefault();

                        var categoriajurado = this.CategoriaJurado;
                        this.CategoriaJurado = null;
                        db.Entry(this).State = EntityState.Modified;
                        this.CategoriaJurado = categoriajurado;
                    }
                    else
                    {
                        registro = db.CategoriaJurado
                                    .Where(x => x.categoria_id == this.categoria_id).SingleOrDefault();
                        db.Entry(this).State = EntityState.Added;
                    }

                    foreach (var c in this.CategoriaJurado)
                    {
                        if (!flag)
                        {
                            if (registro == null)
                            {
                                c.categoriajurado_id = LastRegister + 1;
                                db.Entry(c).State = EntityState.Added;
                                LastRegister++;
                            }
                            else
                            {
                                c.categoriajurado_id = registro.categoriajurado_id;
                                db.Entry(c).State = EntityState.Unchanged;
                            }
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception )
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
                    db.Database.ExecuteSqlCommand(
                            "delete from CategoriaJurado where categoria_id = @categoria_id",
                            new SqlParameter("categoria_id", this.categoria_id)
                            );
                    db.Entry(this).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<CategoriaJurado> listarjuradocategoria2(string concurso,string categoria)
        {
            var persona = new List<CategoriaJurado>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    if (concurso == null || concurso == "" || categoria == null || categoria == "")
                    {
                        persona = db.CategoriaJurado.Include("Persona").ToList();
                    }
                    else
                    {
                        persona = db.CategoriaJurado.Include("Persona").Where(x => x.Categoria.categoria_id.ToString().Contains(categoria) && x.Categoria.Concurso.concurso_id.ToString().Contains(concurso)).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return persona;
        }

        public List<CategoriaJurado> listarjuradocategoria3()
        {
            var persona = new List<CategoriaJurado>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.CategoriaJurado.Include("Persona").ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return persona;
        }

        public List<Proyecto> proyectocategoria(string concurso,string categoria)
        {
            var proyecto = new List<Proyecto>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    if (concurso == null || concurso == "" || categoria == null || categoria == "")
                    {
                        proyecto = db.Proyecto.Include(x => x.Categoria).GroupBy(item => item.categoria_id)
                                    .SelectMany(grouping => grouping.Take(1))
                                    .ToList();
                    }
                    else
                    {
                        proyecto = db.Proyecto.Include(x => x.Categoria).Where(x => x.Categoria.categoria_id.ToString().Contains(categoria) && x.Categoria.Concurso.concurso_id.ToString().Contains(concurso)).GroupBy(item => item.categoria_id)
                                    .SelectMany(grouping => grouping.Take(1))
                                    .ToList();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyecto;
        }

        public int proyectocategoriacant(string concurso,string categoria)
        {
            var proyecto = new List<Proyecto>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    if (concurso == null || concurso == "" || categoria == null || categoria == "")
                    {
                        proyecto = db.Proyecto.Include("Categoria").ToList();
                    }
                    else
                    {
                        proyecto = db.Proyecto.Include("Categoria").Where(x => x.Categoria.categoria_id.ToString().Contains(categoria) && x.Categoria.Concurso.concurso_id.ToString().Contains(concurso)).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyecto.Count;
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
