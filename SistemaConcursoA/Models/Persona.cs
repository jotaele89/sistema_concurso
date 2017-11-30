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

    [Table("Persona")]
    public partial class Persona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Persona()
        {
            CategoriaJurado = new HashSet<CategoriaJurado>();
            CursoDocente = new HashSet<CursoDocente>();
            Evaluacion = new HashSet<Evaluacion>();
            ProyectoEstudiante = new HashSet<ProyectoEstudiante>();
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        public int persona_id { get; set; }

        public int tipopersona_id { get; set; }

        [Required]
        [StringLength(8)]
        public string dni { get; set; }

        [StringLength(10)]
        public string codigo { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string apellido { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(15)]
        public string celular { get; set; }

        [StringLength(10)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoriaJurado> CategoriaJurado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CursoDocente> CursoDocente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluacion> Evaluacion { get; set; }

        public virtual TipoPersona TipoPersona { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProyectoEstudiante> ProyectoEstudiante { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }


        public AnexGRIDResponde ListarGrillaJurado(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();

                    var query = db.Usuario.Include("Persona").Include("TipoUsuario1").Where(x => x.TipoUsuario1.nombre.Contains("Jurado"));

                    if (grilla.columna == "id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.persona_id)
                                                               : query.OrderBy(x => x.persona_id);
                    }

                    if (grilla.columna == "nombre")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.Persona.nombre)
                                                               : query.OrderBy(x => x.Persona.nombre);
                    }

                    if (grilla.columna == "apellido")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.Persona.apellido)
                                                               : query.OrderBy(x => x.Persona.apellido);
                    }

                    if (grilla.columna == "usuario")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre)
                                                               : query.OrderBy(x => x.nombre);
                    }

                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "nombre" && f.valor != "")
                            query = query.Where(x => x.Persona.nombre.Contains(f.valor));

                        if (f.columna == "apellido" && f.valor != "")
                            query = query.Where(x => x.Persona.apellido.Contains(f.valor));
                    }


                    var planestudio = query.Skip(grilla.pagina)
                                      .Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in planestudio
                                   select new
                                   {
                                       s.persona_id,
                                       s.Persona.nombre,
                                       s.Persona.apellido,
                                       usuario = s.nombre
                                   }, total);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return grilla.responde();
        }

        public List<CategoriaJurado> listarcategoriajurado(int id)
        {
            var jurado = new List<CategoriaJurado>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    jurado = db.CategoriaJurado.Include("Categoria").Include("Persona").Where(x => x.persona_id == id).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return jurado;
        }

        public List<Usuario> listarusuariojurado(int id)
        {
            var jurado = new List<Usuario>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    jurado = db.Usuario.Where(x => x.persona_id == id).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return jurado;
        }

        public List<Persona> listarpersona(int id)
        {
            var person = new List<Persona>();
            var persona = new List<Persona>();
            var usuario = new List<Usuario>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    usuario = db.Usuario.Include("TipoUsuario1").Where(x => x.TipoUsuario1.nombre.Contains("Jurado")).ToList();
                    person = db.Persona.Include("TipoPersona").Where(x => x.TipoPersona.nombre.Contains("Docente") || x.TipoPersona.nombre.Contains("Externo")).ToList();
                    foreach (var u in usuario)
                    {
                        foreach (var p in person)
                        {
                            if (u.persona_id != p.persona_id)
                            {
                                persona.Add(p);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return persona;
        }

        public Usuario listarusuario(int id)
        {
            var jurado = new Usuario();

            try
            {
                using (var db = new ModeloDatos())
                {
                    jurado = db.Usuario.Where(x => x.persona_id == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return jurado;
        }

        public int obtenertipousuario()
        {
            var jurado = new TipoUsuario();
            int id = 0;
            try
            {
                using (var db = new ModeloDatos())
                {
                    jurado = db.TipoUsuario.Where(x => x.nombre.Contains("Jurado")).SingleOrDefault();
                    id = jurado.tipousuario_id;
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return id;
        }

        public Persona buscarpersona(string id)
        {
            var jurado = new Persona();

            try
            {
                using (var db = new ModeloDatos())
                {
                    jurado = db.Persona.Include("TipoPersona").Where(x => x.codigo.Contains(id) || (x.nombre + " " + x.apellido).Contains(id) || x.dni.Contains(id)).Where(x => x.TipoPersona.nombre.Contains("Docente") || x.TipoPersona.nombre.Contains("Externo")).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return jurado;
        }

        public Persona ObtenerJurado(int id)
        {
            var jurado = new Persona();

            try
            {
                using (var db = new ModeloDatos())
                {
                    jurado = db.Persona.
                        Where(x => x.persona_id == id).
                        SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return jurado;
        }

        public void GuardarN()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    var LastRegister = 0;

                    if (this.persona_id > 0)
                    {
                        LastRegister = Convert.ToInt32(db.Database.SqlQuery<decimal>("Select IDENT_CURRENT ('CategoriaJurado')", new object[0]).FirstOrDefault());

                        db.Database.ExecuteSqlCommand(
                            "UPDATE Persona " +
"SET nombre = @nombre, apellido = @apellido, estado = @estado WHERE persona_id = @persona_id",
                        new SqlParameter("nombre", this.nombre),
                        new SqlParameter("apellido", this.apellido),
                        new SqlParameter("estado", this.estado),
                        new SqlParameter("persona_id", this.persona_id)
                        );

                        LastRegister = Convert.ToInt32(db.Database.SqlQuery<decimal>("Select IDENT_CURRENT ('Usuario')", new object[0]).FirstOrDefault());

                        foreach (var u in Usuario)
                        {
                            if (u.usuario_id == 0)
                            {
                                db.Database.ExecuteSqlCommand(
                            "insert Usuario(persona_id,tipousuario,nombre,clave,estado) values(" +
"@persona_id, @tipousuario, @nombre, @clave, @estado)",
                        new SqlParameter("persona_id", this.persona_id),
                        new SqlParameter("tipousuario", u.tipousuario),
                        new SqlParameter("nombre", u.nombre),
                        new SqlParameter("clave", HashHelper.MD5(u.clave)),
                        new SqlParameter("estado", u.estado)
                        );
                            }
                            else
                            {
                                if (u.clave != "")
                                {
                                    db.Database.ExecuteSqlCommand(
                            "UPDATE Usuario " +
"SET tipousuario = @tipousuario, nombre = @nombre, clave = @clave, estado = @estado WHERE persona_id = @persona_id",
                        new SqlParameter("tipousuario", u.tipousuario),
                        new SqlParameter("nombre", u.nombre),
                        new SqlParameter("clave", HashHelper.MD5(u.clave)),
                        new SqlParameter("estado", u.estado),
                        new SqlParameter("persona_id", this.persona_id)
                        );
                                }
                                else
                                {
                                    db.Database.ExecuteSqlCommand(
                            "UPDATE Usuario " +
"SET tipousuario = @tipousuario, nombre = @nombre, estado = @estado WHERE persona_id = @persona_id",
                        new SqlParameter("tipousuario", u.tipousuario),
                        new SqlParameter("nombre", u.nombre),
                        new SqlParameter("estado", u.estado),
                        new SqlParameter("persona_id", this.persona_id)
                        );
                                }
                            }
                        }
                    }
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
                    db.Database.ExecuteSqlCommand(
                            "delete from CategoriaJurado where persona_id = @persona_id",
                            new SqlParameter("persona_id", this.persona_id)
                            );

                    db.Database.ExecuteSqlCommand(
                            "delete from Usuario where persona_id = @persona_id",
                            new SqlParameter("persona_id", this.persona_id)
                            );

                    db.Database.ExecuteSqlCommand(
                            "delete from Persona where persona_id = @persona_id",
                            new SqlParameter("persona_id", this.persona_id)
                            );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();

                    var query = db.Persona.Include("TipoPersona").Where(x => x.persona_id > 0);

                    if (grilla.columna == "persona_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.persona_id)
                                                               : query.OrderBy(x => x.persona_id);
                    }

                    if (grilla.columna == "tipopersona_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.tipopersona_id)
                                                               : query.OrderBy(x => x.tipopersona_id);
                    }

                    if (grilla.columna == "dni")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.dni)
                                                               : query.OrderBy(x => x.nombre);
                    }

                    if (grilla.columna == "codigo")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.codigo)
                                                               : query.OrderBy(x => x.codigo);
                    }

                    if (grilla.columna == "nombre")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre)
                                                               : query.OrderBy(x => x.nombre);
                    }

                    if (grilla.columna == "apellido")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.apellido)
                                                               : query.OrderBy(x => x.apellido);
                    }

                    if (grilla.columna == "email")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.email)
                                                               : query.OrderBy(x => x.email);
                    }

                    if (grilla.columna == "celular")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.celular)
                                                               : query.OrderBy(x => x.celular);
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
                            query = query.Where(x => x.tipopersona_id.ToString() == f.valor);
                    }


                    var persona = query.Skip(grilla.pagina)
                                      .Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in persona
                                   select new
                                   {
                                       s.persona_id,
                                       s.TipoPersona.nombre,
                                       s.dni,
                                       s.codigo,
                                       nombretipo = s.nombre,
                                       s.apellido,
                                       s.email,
                                       s.celular,
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

        public List<TipoPersona> listartipo() //retornar es un Collection
        {
            var tipo = new List<TipoPersona>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    tipo = db.TipoPersona.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return tipo;
        }

        //public List<Curso> listarcursoplan(int id)
        //{
        //    var curso = new List<Curso>();

        //    try
        //    {
        //        using (var db = new ModeloDatos())
        //        {
        //            curso = db.Curso.Where(x => x.plan_id == id).ToList();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }

        //    return curso;
        //}


        public Persona Obtener(int id) //retornar un objeto
        {
            var persona = new Persona();

            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.Persona.Include("TipoPersona").
                        Where(x => x.persona_id == id).
                        SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return persona;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    //this.semestre_id es como un "boolean"
                    if (this.persona_id > 0)
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

        public void EliminarP()
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

        public List<Persona> Listar() //retornar es un Collection
        {
            var persona = new List<Persona>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.Persona.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return persona;
        }
        public List<Persona> Buscar(String criterio)
        {
            var persona = new List<Persona>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.Persona.Where(x => x.codigo.Contains(criterio)).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return persona;

        }

        public string[,] ConsultaTotalPersona()
        {
            string[,] personas;
            try
            {
                using (var db = new ModeloDatos())
                {
                    var con = (from p in db.TipoPersona
                               join c in db.Persona on p.tipopersona_id equals c.tipopersona_id into r_join
                               from c in r_join.DefaultIfEmpty()
                               group new { p, c } by new
                               {
                                   p.tipopersona_id,
                                   p.nombre
                               } into g
                               select new
                               {
                                   id_curso = (int?)g.Key.tipopersona_id,
                                   g.Key.nombre,
                                   Cantidad = g.Count(p => p.c.tipopersona_id != 0)
                               }).ToList();
                    personas = new string[con.Count(), 2];
                    int count = 0;
                    foreach (var p in con)
                    {
                        personas[count, 0] = Convert.ToString(p.nombre);
                        personas[count, 1] = Convert.ToString(p.Cantidad);
                        count++;
                    }
                }
                return personas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}