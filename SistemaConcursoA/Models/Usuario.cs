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
    using System.IO;
    using System.Web;
    using System.Data.Entity.Validation;

    [Table("Usuario")]
    public partial class Usuario
    {
        [Key]
        public int usuario_id { get; set; }

        public int persona_id { get; set; }

        public int tipousuario { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string clave { get; set; }

        [StringLength(250)]
        public string avatar { get; set; }

        [Required]
        [StringLength(10)]
        public string estado { get; set; }

        public virtual Persona Persona { get; set; }

        public virtual TipoUsuario TipoUsuario1 { get; set; }

        //crear metodo listar
        public List<Usuario> Listar() //retornar es un Collection
        {
            var usuario = new List<Usuario>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    usuario = db.Usuario.Include("Persona").Include("TipoUsuario1").ToList(); //relacionando la tabla usuario con tabla persona
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return usuario;
        }

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {

                    grilla.Inicializar();
                    var query = db.Usuario.Include("Persona").Include("TipoUsuario1").Where(x => x.persona_id > 0).Where(x => x.tipousuario > 0);

                    //ordenar las columnas a mostrar

                    if (grilla.columna == "usuario_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.usuario_id)
                                                                : query.OrderBy(x => x.usuario_id);
                    }
                    if (grilla.columna == "persona_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.persona_id)
                                                                : query.OrderBy(x => x.persona_id);
                    }
                    if (grilla.columna == "tipousuario")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.tipousuario)
                                                                : query.OrderBy(x => x.tipousuario);
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


                    var usuario = query.Skip(grilla.pagina)
                                        .Take(grilla.limite)
                                        .ToList();
                    var total = query.Count();
                    grilla.SetData(
                        from s in usuario
                        select new
                        {
                            s.usuario_id,
                            nombrepersona = s.Persona.nombre,
                            nombretipousuario = s.TipoUsuario1.nombre,
                            s.nombre,
                            s.estado

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



        public Usuario Obtener(int id) //retornar un objeto
        {
            var usuario = new Usuario();

            try
            {
                using (var db = new ModeloDatos())
                {
                    usuario = db.Usuario.Include("Persona").Include("TipoUsuario1") //relacionando la tabla usuario con tabla persona
                    .Where(x => x.usuario_id == id)
                    .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return usuario;
        }

        public List<Usuario> Buscar(string criterio)
        {
            var usuario = new List<Usuario>();

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
                    usuario = db.Usuario
                        .Where(x => x.nombre.Contains(criterio) || x.estado.Contains(criterio))
                        //|| x.estado = estadosem
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return usuario;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    db.Configuration.ValidateOnSaveEnabled = false;

                    //this.semestre_id es como un "boolean"
                    if (this.usuario_id > 0)
                    {
                        db.Entry(this).State = EntityState.Modified;

                        if (this.clave != null)
                        {
                            this.clave = HashHelper.MD5(this.clave);
                        }
                        else
                        {
                            db.Entry(this).Property(x => x.clave).IsModified = false;
                        }
                    }
                    else
                    {
                        this.clave = HashHelper.MD5(this.clave);
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

        public List<Persona> listarpersona()
        {
            var persona = new List<Persona>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.Persona.Include("TipoPersona").Where(x => x.TipoPersona.nombre.Equals("Docente") || x.TipoPersona.nombre.Equals("Externo")).Where(x => x.estado.Equals("Activo")).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return persona;
        }

        public List<TipoUsuario> listartipousuario()
        {
            var persona = new List<TipoUsuario>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.TipoUsuario.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return persona;
        }

        //validar login
        public ResponseModel ValidarLogin(string Usuario, string Password)
        {
            var rm = new ResponseModel();

            try
            {
                using (var db = new ModeloDatos())
                {
                    Password = HashHelper.MD5(Password);

                    var usuario = db.Usuario.Where(x => x.nombre == Usuario)
                        .Where(x => x.clave == Password).Where(x => x.estado.Equals("Activo"))
                        .SingleOrDefault();

                    if (usuario != null)
                    {
                        SessionHelper.AddUserToSession(usuario.usuario_id.ToString());
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.SetResponse(false, "¡Usuario o Password incorrecto! Ó ¡Usuario Inactivo!");
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return rm;
        }
        public ResponseModel GuardarPerfil(HttpPostedFileBase Foto)
        {
            var rm = new ResponseModel();
            try
            {
                using (var db = new ModeloDatos())
                {
                    db.Configuration.ValidateOnSaveEnabled = false;

                    var Usu = db.Entry(this);

                    Usu.State = EntityState.Modified;

                    if (Foto != null)
                    {
                        string extension = Path.GetExtension(Foto.FileName).ToLower();
                        int size = 1024 * 1024 * 7; //7 megas
                        var filtroextension = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        var extensiones = Path.GetExtension(Foto.FileName);

                        if (filtroextension.Contains(extensiones) && (Foto.ContentLength < size))
                        {
                            string archivo = Path.GetFileName(Foto.FileName);
                            Foto.SaveAs(HttpContext.Current.Server.MapPath("~/Server/Images/" + archivo));
                            this.avatar = archivo;
                        }

                    }
                    else Usu.Property(x => x.avatar).IsModified = false;
                    if (this.usuario_id == 0) Usu.Property(x => x.usuario_id).IsModified = false;
                    if (this.persona_id == 0) Usu.Property(x => x.persona_id).IsModified = false;
                    if (this.tipousuario == 0) Usu.Property(x => x.tipousuario).IsModified = false;
                    if (this.clave == null) Usu.Property(x => x.clave).IsModified = false;
                    if (this.estado == null) Usu.Property(x => x.estado).IsModified = false;
                    db.SaveChanges();
                    rm.SetResponse(true);
                }
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return rm;
        }
    }
}
