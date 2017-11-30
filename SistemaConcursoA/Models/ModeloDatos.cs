namespace SistemaConcursoA.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModeloDatos : DbContext
    {
        public ModeloDatos()
            : base("name=ModeloDatos")
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<CategoriaJurado> CategoriaJurado { get; set; }
        public virtual DbSet<Ciclo> Ciclo { get; set; }
        public virtual DbSet<Concurso> Concurso { get; set; }
        public virtual DbSet<Criterio> Criterio { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<CursoDocente> CursoDocente { get; set; }
        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<Evaluacion> Evaluacion { get; set; }
        public virtual DbSet<OrdenProyecto> OrdenProyecto { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<PlanEstudio> PlanEstudio { get; set; }
        public virtual DbSet<Premio> Premio { get; set; }
        public virtual DbSet<Proyecto> Proyecto { get; set; }
        public virtual DbSet<ProyectoEstudiante> ProyectoEstudiante { get; set; }
        public virtual DbSet<RangoEvaluacion> RangoEvaluacion { get; set; }
        public virtual DbSet<Semestre> Semestre { get; set; }
        public virtual DbSet<TipoPersona> TipoPersona { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<ResultadoEvaluacion> ResultadoEvaluacion { get; set; }
        public virtual DbSet<ResultadoGanadorCategoria> ResultadoGanadorCategoria { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.CategoriaJurado)
                .WithRequired(e => e.Categoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.Premio)
                .WithRequired(e => e.Categoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.Proyecto)
                .WithRequired(e => e.Categoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.ResultadoEvaluacion)
                .WithRequired(e => e.Categoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.ResultadoGanadorCategoria)
                .WithRequired(e => e.Categoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ciclo>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Ciclo>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Ciclo>()
                .HasMany(e => e.Curso)
                .WithRequired(e => e.Ciclo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Concurso>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Concurso>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Concurso>()
                .Property(e => e.tiempoexposicion)
                .IsUnicode(false);

            modelBuilder.Entity<Concurso>()
                .Property(e => e.ESTADO)
                .IsUnicode(false);

            modelBuilder.Entity<Concurso>()
                .HasMany(e => e.Categoria)
                .WithRequired(e => e.Concurso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Concurso>()
                .HasMany(e => e.Criterio)
                .WithRequired(e => e.Concurso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Concurso>()
                .HasMany(e => e.Premio)
                .WithRequired(e => e.Concurso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Concurso>()
                .HasMany(e => e.Proyecto)
                .WithRequired(e => e.Concurso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Concurso>()
                .HasMany(e => e.RangoEvaluacion)
                .WithRequired(e => e.Concurso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Concurso>()
                .HasMany(e => e.ResultadoGanadorCategoria)
                .WithRequired(e => e.Concurso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Criterio>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Criterio>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<Criterio>()
                .HasMany(e => e.Evaluacion)
                .WithRequired(e => e.Criterio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Curso>()
                .Property(e => e.curso_cod)
                .IsUnicode(false);

            modelBuilder.Entity<Curso>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Curso>()
                .Property(e => e.prerequisito)
                .IsUnicode(false);

            modelBuilder.Entity<Curso>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<Curso>()
                .HasMany(e => e.CursoDocente)
                .WithRequired(e => e.Curso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Curso>()
                .HasMany(e => e.Proyecto)
                .WithRequired(e => e.Curso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CursoDocente>()
                .Property(e => e.curso_cod)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.extension)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.tamanio)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<OrdenProyecto>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.dni)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.celular)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.CategoriaJurado)
                .WithRequired(e => e.Persona)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.CursoDocente)
                .WithRequired(e => e.Persona)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Evaluacion)
                .WithRequired(e => e.Persona)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.ProyectoEstudiante)
                .WithRequired(e => e.Persona)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.Persona)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanEstudio>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<PlanEstudio>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<PlanEstudio>()
                .HasMany(e => e.Concurso)
                .WithRequired(e => e.PlanEstudio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanEstudio>()
                .HasMany(e => e.Curso)
                .WithRequired(e => e.PlanEstudio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Premio>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Premio>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Premio>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<Proyecto>()
                .Property(e => e.curso_cod)
                .IsUnicode(false);

            modelBuilder.Entity<Proyecto>()
                .Property(e => e.nombre_proyecto)
                .IsUnicode(false);

            modelBuilder.Entity<Proyecto>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<Proyecto>()
                .HasMany(e => e.Documento)
                .WithRequired(e => e.Proyecto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Proyecto>()
                .HasMany(e => e.Evaluacion)
                .WithRequired(e => e.Proyecto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Proyecto>()
                .HasMany(e => e.OrdenProyecto)
                .WithRequired(e => e.Proyecto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Proyecto>()
                .HasMany(e => e.ProyectoEstudiante)
                .WithRequired(e => e.Proyecto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Proyecto>()
                .HasMany(e => e.ResultadoEvaluacion)
                .WithRequired(e => e.Proyecto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Proyecto>()
                .HasMany(e => e.ResultadoGanadorCategoria)
                .WithRequired(e => e.Proyecto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RangoEvaluacion>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<RangoEvaluacion>()
                .HasMany(e => e.Evaluacion)
                .WithRequired(e => e.RangoEvaluacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semestre>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Semestre>()
                .Property(e => e.anio)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Semestre>()
                .HasMany(e => e.Concurso)
                .WithRequired(e => e.Semestre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoPersona>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TipoPersona>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<TipoPersona>()
                .HasMany(e => e.Persona)
                .WithRequired(e => e.TipoPersona)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoUsuario>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TipoUsuario>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<TipoUsuario>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.TipoUsuario1)
                .HasForeignKey(e => e.tipousuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.clave)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.avatar)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<ResultadoGanadorCategoria>()
                .Property(e => e.estado)
                .IsUnicode(false);
        }
    }
}
