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

    [Table("Proyecto")]
    public partial class Proyecto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Proyecto()
        {
            Documento = new HashSet<Documento>();
            Evaluacion = new HashSet<Evaluacion>();
            OrdenProyecto = new HashSet<OrdenProyecto>();
            ProyectoEstudiante = new HashSet<ProyectoEstudiante>();
            ResultadoEvaluacion = new HashSet<ResultadoEvaluacion>();
            ResultadoGanadorCategoria = new HashSet<ResultadoGanadorCategoria>();
        }

        [Key]
        public int proyecto_id { get; set; }

        public int concurso_id { get; set; }

        [Required]
        [StringLength(100)]
        public string curso_cod { get; set; }

        public int categoria_id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre_proyecto { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime fecharegistro { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime horaregistro { get; set; }

        [Required]
        [StringLength(10)]
        public string estado { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual Concurso Concurso { get; set; }

        public virtual Curso Curso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documento> Documento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluacion> Evaluacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenProyecto> OrdenProyecto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProyectoEstudiante> ProyectoEstudiante { get; set; }

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

                    var query = db.Proyecto.Include("Concurso").Include("Curso").Include("Categoria").Where(x => x.proyecto_id > 0);

                    if (grilla.columna == "proyecto_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.proyecto_id)
                                                               : query.OrderBy(x => x.proyecto_id);
                    }

                    if (grilla.columna == "nombre")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.concurso_id)
                                                               : query.OrderBy(x => x.concurso_id);
                    }

                    if (grilla.columna == "curso_cod")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.curso_cod)
                                                               : query.OrderBy(x => x.curso_cod);
                    }

                    if (grilla.columna == "categoria_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.categoria_id)
                                                               : query.OrderBy(x => x.categoria_id);
                    }

                    if (grilla.columna == "nombre_proyecto")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre_proyecto)
                                                               : query.OrderBy(x => x.nombre_proyecto);
                    }

                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "nombrecategoria" && f.valor != "")
                            query = query.Where(x => x.categoria_id.ToString() == f.valor);

                        if (f.columna == "nombreproyecto")
                            query = query.Where(x => x.nombre_proyecto.Contains(f.valor));

                        if (f.columna == "nombre")
                            query = query.Where(x => x.Concurso.concurso_id.ToString().Contains(f.valor));
                    }


                    var planestudio = query.Skip(grilla.pagina)
                                      .Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in planestudio
                                   select new
                                   {
                                       s.proyecto_id,
                                       s.Concurso.nombre,
                                       nombrecurso = s.Curso.nombre,
                                       nombrecategoria = s.Categoria.nombre,
                                       s.nombre_proyecto
                                   }, total);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return grilla.responde();
        }

        public List<Categoria> listarcategoria()
        {
            var categoria = new List<Categoria>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    categoria = db.Categoria.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return categoria;
        }

        public List<Categoria> listarcategoria2(int idconcurso)
        {
            var categoria = new List<Categoria>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    categoria = db.Categoria.Where(x => x.concurso_id == idconcurso).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return categoria;
        }

        public ResultadoEvaluacion penalidad(int id)
        {
            var penal = new ResultadoEvaluacion();

            try
            {
                using (var db = new ModeloDatos())
                {
                    penal = db.ResultadoEvaluacion.Where(x => x.proyecto_id == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return penal;
        }

        public List<Categoria> listarcategoriaa(int id)
        {
            var categoria = new List<Categoria>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    categoria = db.Categoria.Include("Concurso").Where(x => x.concurso_id == id).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return categoria;
        }

        public List<Concurso> listarconcurso()
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

        public List<Persona> listarpersona()
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

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    var LastRegister = 0;

                    var flag = false;

                    var registro = new OrdenProyecto();

                    if (this.categoria_id > 0)
                    {
                        if (this.OrdenProyecto != null)
                        {
                            flag = true;
                        }

                        db.Database.ExecuteSqlCommand(
                            "DELETE FROM OrdenProyecto WHERE proyecto_id = @proyecto_id",
                        new SqlParameter("proyecto_id", this.proyecto_id)
                        );

                        LastRegister = Convert.ToInt32(db.Database.SqlQuery<decimal>("Select IDENT_CURRENT ('OrdenProyecto')", new object[0]).FirstOrDefault());

                        registro = db.OrdenProyecto
                                    .Where(x => x.proyecto_id == this.proyecto_id).SingleOrDefault();

                        var ordenproyecto = this.OrdenProyecto;
                        this.OrdenProyecto = null;
                        db.Entry(this).State = EntityState.Modified;
                        this.OrdenProyecto = ordenproyecto;
                    }
                    else
                    {
                        registro = db.OrdenProyecto
                                    .Where(x => x.proyecto_id == this.proyecto_id).SingleOrDefault();
                        db.Entry(this).State = EntityState.Added;
                    }

                    foreach (var c in this.OrdenProyecto)
                    {
                        if (flag)
                        {
                            if (registro == null)
                            {
                                c.ordenproyecto_id = LastRegister + 1;
                                db.Entry(c).State = EntityState.Added;
                                LastRegister++;
                            }
                            else
                            {
                                db.Entry(c).State = EntityState.Unchanged;
                            }
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<Curso> listarcurso()
        {
            var curso = new List<Curso>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    curso = db.Curso.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return curso;
        }

        public List<OrdenProyecto> listarordenproyecto(int id)
        {
            var orden = new List<OrdenProyecto>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    orden = db.OrdenProyecto.Where(x => x.proyecto_id == id).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return orden;
        }

        public List<ProyectoEstudiante> listarproyectoparticipante(int id)
        {
            var estudiante = new List<ProyectoEstudiante>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    estudiante = db.ProyectoEstudiante.Include("Persona").Where(x => x.proyecto_id == id).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return estudiante;
        }

        public List<Documento> listardocumento(int id)
        {
            var documento = new List<Documento>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    documento = db.Documento.Where(x => x.proyecto_id == id).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return documento;
        }

        public Proyecto Obtener(int id)
        {
            var proyecto = new Proyecto();

            try
            {
                using (var db = new ModeloDatos())
                {
                    proyecto = db.Proyecto.Include("Concurso").Include("Curso").Include("Categoria").
                        Where(x => x.proyecto_id == id).
                        SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyecto;
        }

        public void Eliminar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    db.Database.ExecuteSqlCommand(
                            "delete from Documento where proyecto_id = @proyecto_id",
                            new SqlParameter("proyecto_id", this.proyecto_id)
                            );
                    db.Database.ExecuteSqlCommand(
                            "delete from OrdenProyecto where proyecto_id = @proyecto_id",
                            new SqlParameter("proyecto_id", this.proyecto_id)
                            );
                    db.Database.ExecuteSqlCommand(
                            "delete from ProyectoEstudiante where proyecto_id = @proyecto_id",
                            new SqlParameter("proyecto_id", this.proyecto_id)
                            );

                    db.Database.ExecuteSqlCommand(
                            "delete from Evaluacion where proyecto_id = @proyecto_id",
                            new SqlParameter("proyecto_id", this.proyecto_id)
                            );

                    db.Database.ExecuteSqlCommand(
                            "delete from ResultadoEvaluacion where proyecto_id = @proyecto_id",
                            new SqlParameter("proyecto_id", this.proyecto_id)
                            );

                    db.Database.ExecuteSqlCommand(
                            "delete from ResultadoGanadorCategoria where proyecto_id = @proyecto_id",
                            new SqlParameter("proyecto_id", this.proyecto_id)
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

        public AnexGRIDResponde Consulta01(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();

                    var query = db.Proyecto.Include("Concurso").Include("Curso").Include("Categoria").Where(x => x.proyecto_id > 0 && x.estado.Contains("Activo"));

                    if (grilla.columna == "proyecto_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.proyecto_id)
                                                               : query.OrderBy(x => x.proyecto_id);
                    }

                    if (grilla.columna == "concurso_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.concurso_id)
                                                               : query.OrderBy(x => x.concurso_id);
                    }

                    if (grilla.columna == "curso_cod")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.curso_cod)
                                                               : query.OrderBy(x => x.curso_cod);
                    }

                    if (grilla.columna == "categoria_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.categoria_id)
                                                               : query.OrderBy(x => x.categoria_id);
                    }

                    if (grilla.columna == "nombre_proyecto")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre_proyecto)
                                                               : query.OrderBy(x => x.nombre_proyecto);
                    }

                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "nombrecategoria" && f.valor != "")
                            query = query.Where(x => x.categoria_id.ToString() == f.valor);
                        if (f.columna == "nombre" && f.valor != "")
                            query = query.Where(x => x.Concurso.concurso_id.ToString() == f.valor);
                    }


                    var proyecto = query.Skip(grilla.pagina)
                                      .Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in proyecto
                                   select new
                                   {
                                       s.proyecto_id,
                                       s.Concurso.nombre,
                                       nombrecurso = s.Curso.nombre,
                                       nombrecategoria = s.Categoria.nombre,
                                       s.nombre_proyecto
                                   }, total);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return grilla.responde();
        }

        public List<OrdenProyecto> listarproyecto(string concurso, string categoria)
        {
            var proyecto = new List<OrdenProyecto>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    if (concurso == null || concurso == "" || categoria == null || categoria == "")
                    {
                        proyecto = db.OrdenProyecto.Include("Proyecto").Where(x => x.Proyecto.estado.Equals("Activo")).ToList();
                    }
                    else
                    {
                        proyecto = db.OrdenProyecto.Include("Proyecto").Where(x => x.Proyecto.estado.Equals("Activo") && x.Proyecto.categoria_id.ToString().Contains(categoria) && x.Proyecto.Concurso.concurso_id.ToString().Contains(concurso)).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyecto;
        }

        public List<OrdenProyecto> listarproyecto2(string concurso, string categoria)
        {
            var proyecto = new List<OrdenProyecto>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    if (concurso == null || concurso == "" || categoria == null || categoria == "")
                    {
                        proyecto = db.OrdenProyecto.Include("Proyecto").Where(x => x.Proyecto.estado.Equals("Activo")).Where(x => x.estado.Equals("Activo")).ToList();
                    }
                    else
                    {
                        proyecto = db.OrdenProyecto.Include("Proyecto").Where(x => x.Proyecto.estado.Equals("Activo") && x.Proyecto.categoria_id.ToString().Contains(categoria) && x.Proyecto.Concurso.concurso_id.ToString().Contains(concurso)).Where(x => x.estado.Equals("Activo")).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyecto;
        }

        public List<Proyecto> listarproyectoo(string concurso, string categoria)
        {
            var proyecto = new List<Proyecto>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    if (concurso == null || concurso == "" || categoria == null || categoria == "")
                    {
                        proyecto = db.Proyecto.Include("Curso").Include("Categoria").Include("Concurso").Where(x => x.estado.Contains("Activo")).ToList();
                    }
                    else
                    {
                        proyecto = db.Proyecto.Include("Curso").Include("Categoria").Include("Concurso").Where(x => x.estado.Contains("Activo") && x.categoria_id.ToString().Contains(categoria) && x.Concurso.concurso_id.ToString().Contains(concurso)).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyecto;
        }

        public List<ResultadoEvaluacion> listarproyectoganador(string concurso, string categoria)
        {
            var proyecto = new List<ResultadoEvaluacion>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    if (concurso == null || concurso == "" || categoria == null || categoria == "")
                    {
                        proyecto = db.ResultadoEvaluacion.Include("Proyecto").Include("Categoria").OrderByDescending(x => (x.puntaje - x.penalidad)).ToList();
                    }
                    else
                    {
                        proyecto = db.ResultadoEvaluacion.Include("Proyecto").Include("Categoria").Where(x => x.categoria_id.ToString().Contains(categoria) && x.Proyecto.Concurso.concurso_id.ToString().Contains(concurso)).OrderByDescending(x => (x.puntaje - x.penalidad)).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyecto;
        }

        public void Agregar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    db.Database.ExecuteSqlCommand(
                        "insert into Proyecto values(@concurso_id,@curso_cod,@categoria_id,@nombre_proyecto,@fecharegistro,@horaregistro,@estado)",
                        new SqlParameter("concurso_id", this.concurso_id),
                        new SqlParameter("curso_cod", this.curso_cod),
                        new SqlParameter("categoria_id", this.categoria_id),
                          new SqlParameter("nombre_proyecto", this.nombre_proyecto),
                            new SqlParameter("fecharegistro", this.fecharegistro),
                              new SqlParameter("horaregistro", this.horaregistro),
                                new SqlParameter("estado", "Activo")
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public CursoDocente docente(int id)
        {
            var persona = new CursoDocente();
            var proy = new Proyecto();

            try
            {
                using (var db = new ModeloDatos())
                {
                    proy = db.Proyecto.Include("Concurso").Include("Curso").Include("Categoria").
                        Where(x => x.proyecto_id == id).
                        SingleOrDefault();
                    persona = db.CursoDocente.Include("Curso").Include("Persona").Where(x => x.Curso.curso_cod.Contains(proy.Curso.curso_cod)).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return persona;
        }

        public List<ProyectoEstudiante> integrantes(int id)
        {
            var integrante = new List<ProyectoEstudiante>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    integrante = db.ProyectoEstudiante.Include("Persona").
                        Where(x => x.proyecto_id == id).
                        ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return integrante;
        }

        public List<Evaluacion> evaluacion(int id)
        {
            var evaluacion = new List<Evaluacion>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    evaluacion = db.Evaluacion.Include("Persona").Include("Proyecto").Include("RangoEvaluacion").Include("Criterio").
                        Where(x => x.persona_id == id).
                        ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return evaluacion;
        }

        public List<Evaluacion> evaluacionn(int id)
        {
            var evaluacion = new List<Evaluacion>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    evaluacion = db.Evaluacion.Include("Persona").Include("Proyecto").Include("RangoEvaluacion").Include("Criterio").
                        Where(x => x.proyecto_id == id).
                        ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return evaluacion;
        }

        public List<Proyecto> listarproyectoantiguo(string concurso, string categoria)
        {
            var proyecto = new List<Proyecto>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    if (concurso == null || concurso == "" || categoria == null || categoria == "")
                    {
                        proyecto = db.Proyecto.Include("Concurso").Include("Curso").Include("Categoria").Where(x => x.estado.Equals("Activo")).ToList();
                    }
                    else
                    {
                        proyecto = db.Proyecto.Include("Concurso").Include("Curso").Include("Categoria").Where(x => x.estado.Equals("Activo") && x.categoria_id.ToString().Contains(categoria) && x.Concurso.concurso_id.ToString().Contains(concurso)).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyecto;
        }

        public void AgregarPP()
        {
            System.DateTime dt;
            dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            string fecha;
            fecha = dt.ToString("yyyyMMdd");

            System.DateTime dtt;
            dtt = Convert.ToDateTime(DateTime.Now.ToShortTimeString());

            string hora;
            hora = dtt.ToString("HH:mm:ss");

            try
            {
                using (var db = new ModeloDatos())
                {
                    db.Database.ExecuteSqlCommand(
                        "insert into Proyecto values(@concurso_id,@curso_cod,@categoria_id,@nombre_proyecto,cast(@fecharegistro as smalldatetime),cast(@horaregistro as smalldatetime),@estado)",
                        new SqlParameter("concurso_id", this.concurso_id),
                        new SqlParameter("curso_cod", this.curso_cod),
                        new SqlParameter("categoria_id", this.categoria_id),
                          new SqlParameter("nombre_proyecto", this.nombre_proyecto),
                            new SqlParameter("fecharegistro", fecha),
                              new SqlParameter("horaregistro", hora),
                                new SqlParameter("estado", "Activo")
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }



        public List<Concurso> listarconcursoo()
        {
            var concurso = new List<Concurso>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    concurso = db.Concurso.Where(x => x.ESTADO.Equals("Activo")).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return concurso;
        }

        public List<Curso> listarcursoo()
        {
            var concurso = new List<Curso>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    concurso = db.Curso.Where(x => x.estado.Equals("Activo")).ToList();
                }

            }
            catch (Exception e)
            {
                throw;
            }

            return concurso;
        }

        public void EliminarRegistro(int id)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    db.Database.ExecuteSqlCommand(
                            "delete from Documento where proyecto_id = @proyecto_id",
                            new SqlParameter("proyecto_id", id)
                            );
                    db.Database.ExecuteSqlCommand(
                            "delete from ProyectoEstudiante where proyecto_id = @proyecto_id",
                            new SqlParameter("proyecto_id", id)
                            );

                    db.Database.ExecuteSqlCommand(
                            "delete from Proyecto where proyecto_id = @proyecto_id",
                            new SqlParameter("proyecto_id", id)
                            );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }



        //consulta03 front
        public AnexGRIDResponde Consulta03(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();

                    var query = db.Proyecto.Include("Concurso").Include("Curso").Include("Categoria").Where(x => x.proyecto_id > 0 && x.estado.Contains("Activo") && x.Concurso.ESTADO.Equals("Inactivo"));

                    if (grilla.columna == "proyecto_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.proyecto_id)
                                                               : query.OrderBy(x => x.proyecto_id);
                    }

                    if (grilla.columna == "concurso_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.concurso_id)
                                                               : query.OrderBy(x => x.concurso_id);
                    }

                    if (grilla.columna == "curso_cod")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.curso_cod)
                                                               : query.OrderBy(x => x.curso_cod);
                    }

                    if (grilla.columna == "categoria_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.categoria_id)
                                                               : query.OrderBy(x => x.categoria_id);
                    }

                    if (grilla.columna == "nombre_proyecto")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre_proyecto)
                                                               : query.OrderBy(x => x.nombre_proyecto);
                    }

                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "nombreconcurso" && f.valor != "")
                            query = query.Where(x => x.concurso_id.ToString() == f.valor);
                    }

                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "nombrecategoria" && f.valor != "")
                            query = query.Where(x => x.categoria_id.ToString() == f.valor);
                    }


                    var proyecto = query.Skip(grilla.pagina)
                                      .Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in proyecto
                                   select new
                                   {
                                       s.proyecto_id,
                                       s.Concurso.nombre,
                                       nombrecurso = s.Curso.nombre,
                                       nombrecategoria = s.Categoria.nombre,
                                       s.nombre_proyecto
                                   }, total);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return grilla.responde();
        }

        public List<Concurso> listarconcursoc3()
        {
            var concurso = new List<Concurso>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    concurso = db.Concurso.Where(x => x.ESTADO.Equals("Inactivo") || x.ESTADO.Equals("INACTIVO")).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return concurso;
        }

        public List<ProyectoEstudiante> cantidadparticipante(int id)
        {
            var estudiante = new List<ProyectoEstudiante>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    estudiante = db.ProyectoEstudiante.Where(x => x.proyecto_id == id).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return estudiante;
        }

        public List<Concurso> listarconcursofe()
        {
            var concurso = new List<Concurso>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    concurso = db.Concurso.Where(x => x.ESTADO.Equals("Activo") || x.ESTADO.Equals("ACTIVO")).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return concurso;
        }

        public List<Proyecto> listarproyectooo(string concurso, string categoria)
        {
            var proyecto = new List<Proyecto>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    proyecto = db.Proyecto.Include("Concurso").Include("Categoria").Where(x => x.estado.Equals("Activo") && x.concurso_id.ToString().Equals(concurso) && x.categoria_id.ToString().Equals(categoria)).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyecto;
        }

        public List<CategoriaJurado> categoriajurado(string concurso, string categoria)
        {
            var categoriaa = new List<CategoriaJurado>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    categoriaa = db.CategoriaJurado.Include("Categoria").Include(x => x.Categoria.Concurso).Include("Persona").Where(x => x.categoria_id.ToString().Equals(categoria) && x.Categoria.concurso_id.ToString().Equals(concurso)).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return categoriaa;
        }

        public Concurso obtenerfecha(int concurso)
        {

            var concursoo = new Concurso();

            try
            {
                using (var db = new ModeloDatos())
                {
                    concursoo = db.Concurso.Where(x => x.concurso_id == concurso).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return concursoo;

        }
    }
}
