CREATE DATABASE db_concurso
go
USE db_concurso
go


if (not exists(select 1 from sys.tables where name = 'Semestre'))
    CREATE TABLE dbo.Semestre (
       semestre_id    int identity(1,1) NOT NULL,
       nombre        varchar(100) NOT NULL unique,
       anio 	     char(4) NOT NULL,
       fechainicio   smalldatetime NOT NULL,
       fechafin      smalldatetime NOT NULL,
       PRIMARY KEY (semestre_id)
      )
go

if (not exists(select 1 from sys.tables where name = 'Ciclo'))
    CREATE TABLE dbo.Ciclo (
       ciclo_id      int identity(1,1) NOT NULL,
       nombre        varchar(100) NULL unique,
       descripcion   text,
       PRIMARY KEY (ciclo_id)
)
go

if (not exists(select 1 from sys.tables where name = 'PlanEstudio'))
    CREATE TABLE dbo.PlanEstudio (
       plan_id         int identity(1,1) NOT NULL,
       semestre_id     int,
       nombre          varchar(100) NULL unique,
       estado          varchar(10) NOT NULL,
       PRIMARY KEY (plan_id),
       FOREIGN KEY (semestre_id) REFERENCES SEMESTRE
)
go

if (not exists(select 1 from sys.tables where name = 'Curso'))
    CREATE TABLE dbo.Curso (
       curso_cod        varchar(100) NOT NULL unique,
       plan_id          int NOT NULL,
       ciclo_id         int NOT NULL,
       nombre          varchar(150) NOT NULL unique,
       credito        int NOT NULL,
       horasteoria     int NOT NULL,
       horaspractica   int NOT NULL,
       totalhoras      int NOT NULL,
       prerequisito    varchar(150) NULL,
       estado          varchar(10) NULL,
       PRIMARY KEY (curso_cod),
       FOREIGN KEY (plan_id) REFERENCES PlanEstudio,
       FOREIGN KEY (ciclo_id) REFERENCES Ciclo
)
go

if (not exists(select 1 from sys.tables where name = 'TipoPersona'))
    CREATE TABLE dbo.TipoPersona (
       tipopersona_id       int identity(1,1) NOT NULL,
       nombre               varchar(100) NOT NULL UNIQUE, 
       estado               varchar(10) not null,
       PRIMARY KEY (tipopersona_id)
)
go


if (not exists(select 1 from sys.tables where name = 'Persona'))
    CREATE TABLE dbo.Persona (
       persona_id      int identity(1,1) NOT NULL,
       tipopersona_id  int NOT NULL,
       dni            varchar(8) NOT NULL UNIQUE,
       codigo         varchar(10) NULL UNIQUE,
       nombre         varchar(100) NOT NULL,
       apellido       varchar(100) NOT NULL,
       email          varchar(100) NULL UNIQUE,
       celular	      varchar(15) NULL,
       estado         varchar(10) NULL,
       PRIMARY KEY (persona_id), 
       FOREIGN KEY (tipopersona_id) REFERENCES TipoPersona
)
go

if (not exists(select 1 from sys.tables where name = 'CursoDocente'))
    CREATE TABLE dbo.CursoDocente (
       cursodocente_id	int identity(1,1) NOT NULL,
       curso_cod        varchar(100) NOT NULL,
       persona_id          int NOT NULL,
       PRIMARY KEY (cursodocente_id),
       FOREIGN KEY (curso_cod) REFERENCES Curso,
       FOREIGN KEY (persona_id) REFERENCES Persona
)
go



if (not exists(select 1 from sys.tables where name = 'Concurso'))
    CREATE TABLE dbo.Concurso (
       concurso_id        int identity(1,1) NOT NULL,
       semestre_id         int NOT NULL,
       plan_id             int NOT NULL,
       nombre             varchar(150) NOT NULL UNIQUE,
       descripcion        text NULL,
       fechaconcurso      date NOT NULL,
	   horainicio 	  time NOT NULL,
	   fechaconcursoinicioregistro      date NOT NULL,
	   fechaconcursofinregistro      date NOT NULL,
       horainicioregistro 	  time NOT NULL,
	   horafinregistro 	  time NOT NULL,
       tiempoexposicion   varchar(10) NULL,
       ESTADO             varchar(10) NOT NULL,
       PRIMARY KEY (concurso_id),
       FOREIGN KEY (semestre_id) REFERENCES Semestre,
       FOREIGN KEY (plan_id) REFERENCES PlanEstudio
)
go

if (not exists(select 1 from sys.tables where name = 'Categoria'))
    CREATE TABLE dbo.Categoria (
       categoria_id     int identity(1,1) NOT NULL,
	   concurso_id      int NOT NULL,
       nombre           varchar(100) NULL,
       descripcion	text,
       PRIMARY KEY (categoria_id),
	   FOREIGN KEY (concurso_id) REFERENCES Concurso
)
go

if (not exists(select 1 from sys.tables where name = 'CategoriaJurado'))
    CREATE TABLE dbo.CategoriaJurado (
       categoriajurado_id	int identity(1,1) NOT NULL,
       categoria_id     int NOT NULL,
       persona_id       int NOT NULL,
       PRIMARY KEY (categoriajurado_id),
       FOREIGN KEY (categoria_id) REFERENCES Categoria,
       FOREIGN KEY (persona_id) REFERENCES Persona
)
go

if (not exists(select 1 from sys.tables where name = 'Criterio'))
    CREATE TABLE dbo.Criterio (
       criterio_id      int identity(1,1) NOT NULL,
       concurso_id      int NOT NULL,
       nombre           varchar(150) NULL UNIQUE,
       puntaje          int NOT NULL,
       estado           varchar(10),
       PRIMARY KEY (criterio_id),
       FOREIGN KEY (concurso_id) REFERENCES Concurso
)
go

if (not exists(select 1 from sys.tables where name = 'RangoEvaluacion'))
    CREATE TABLE dbo.RangoEvaluacion (
       rango_id           int identity(1,1) NOT NULL,
	   concurso_id      int NOT NULL,
       nombre             varchar(150) NULL UNIQUE,
       puntaje            int NOT NULL,
       PRIMARY KEY (rango_id),
	   FOREIGN KEY (concurso_id) REFERENCES Concurso
)
go

if (not exists(select 1 from sys.tables where name = 'Proyecto'))
    CREATE TABLE dbo.Proyecto (
       proyecto_id        int identity(1,1) NOT NULL,
       concurso_id        int NOT NULL,
	   curso_cod	      varchar(100),
       categoria_id       int NOT NULL,
	   nombre_proyecto    varchar(100) NOT NULL,
       fecharegistro      smalldatetime NOT NULL,
       horaregistro       smalldatetime NOT NULL,       
       estado             varchar(10) NOT NULL,       
       PRIMARY KEY (proyecto_id), 
       FOREIGN KEY (concurso_id)  REFERENCES Concurso,
       FOREIGN KEY (categoria_id) REFERENCES Categoria
)
go

if (not exists(select 1 from sys.tables where name = 'Evaluacion'))
    CREATE TABLE dbo.Evaluacion (
       evaluacion_id     int identity(1,1) NOT NULL,
       proyecto_id       int NOT NULL,
	   persona_id	int not null,
       rango_id          int NOT NULL,
       criterio_id       int NOT NULL, 
       PRIMARY KEY (evaluacion_id), 
       FOREIGN KEY (proyecto_id)  REFERENCES Proyecto,
       FOREIGN KEY (rango_id) REFERENCES RangoEvaluacion,
       FOREIGN KEY (criterio_id) REFERENCES Criterio,
	   FOREIGN KEY (persona_id) REFERENCES Persona
)
go

if (not exists(select 1 from sys.tables where name = 'ResultadoEvaluacion'))
    CREATE TABLE dbo.ResultadoEvaluacion (
       proyecto_id     int NOT NULL,
	   categoria_id       int NOT NULL,
       puntaje       int NOT NULL,
       penalidad          int NOT NULL, 
       FOREIGN KEY (proyecto_id)  REFERENCES Proyecto,
	   FOREIGN KEY (categoria_id)  REFERENCES Categoria
)
go

if (not exists(select 1 from sys.tables where name = 'ResultadoGanadorCategoria'))
    CREATE TABLE dbo.ResultadoGanadorCategoria (
	   categoria_id       int NOT NULL,
	   proyecto_id	int NOT NULL,
	   concurso_id	int NOT NULL,
       puntaje       int NOT NULL,
	   estado	varchar(10) NOT NULL,
       FOREIGN KEY (categoria_id)  REFERENCES Categoria,
	   FOREIGN KEY (proyecto_id)  REFERENCES Proyecto,
	   FOREIGN KEY (concurso_id)  REFERENCES Concurso
)
go

if (not exists(select 1 from sys.tables where name = 'TipoUsuario'))
    CREATE TABLE dbo.TipoUsuario (
       tipousuario_id       int identity(1,1) NOT NULL,
       nombre               varchar(100) NOT NULL UNIQUE, 
       descripcion          text NULL,
       PRIMARY KEY (tipousuario_id)
)
go


if (not exists(select 1 from sys.tables where name = 'Usuario'))
    CREATE TABLE dbo.Usuario (
       usuario_id      int identity(1,1) NOT NULL,
       persona_id      int NOT NULL,  
       tipousuario     int NOT NULL,      
       nombre          varchar(100) NOT NULL UNIQUE,
       clave           varchar(100) NOT NULL,
       avatar          varchar(250) NULL,
       estado          varchar(10) NOT NULL,       
       PRIMARY KEY (usuario_id),
       FOREIGN KEY (persona_id)  REFERENCES Persona,
	   FOREIGN KEY (tipousuario)  REFERENCES TipoUsuario
)
go

if (not exists(select 1 from sys.tables where name = 'ProyectoEstudiante'))
    CREATE TABLE dbo.ProyectoEstudiante (
       proyectoestudiante_id        int identity(1,1) NOT NULL,
       proyecto_id        int NOT NULL,
       persona_id      int NOT NULL,       
       PRIMARY KEY (proyectoestudiante_id), 
       FOREIGN KEY (proyecto_id)  REFERENCES Proyecto,
       FOREIGN KEY (persona_id) REFERENCES Persona
)
go

if (not exists(select 1 from sys.tables where name = 'Documento'))
    CREATE TABLE dbo.Documento (
       documento_id    int identity(1,1) NOT NULL,
       proyecto_id     int NOT NULL,
       nombre          varchar(250) NOT NULL,
       descripcion     text NULL,
       extension       char(3) NULL,
       tamanio         varchar(15) NULL,
       estado          varchar(10) NOT NULL,
       PRIMARY KEY (documento_id),
       FOREIGN KEY (proyecto_id) REFERENCES Proyecto,
)
go

if (not exists(select 1 from sys.tables where name = 'OrdenProyecto'))
    CREATE TABLE dbo.OrdenProyecto (
       ordenproyecto_id        int identity(1,1) NOT NULL,
       proyecto_id             int NOT NULL,
       orden                   int NOT NULL,
       horaexposicion          time NOT NULL,
	   estado	varchar(10) NOT NULL,
       PRIMARY KEY (ordenproyecto_id),
       FOREIGN KEY (proyecto_id) REFERENCES Proyecto
)
go

if (not exists(select 1 from sys.tables where name = 'Premio'))
    CREATE TABLE dbo.Premio (
       premio_id       int identity(1,1) NOT NULL,
       categoria_id    int NOT NULL,
       concurso_id     int NOT NULL,
       nombre          varchar(100) NOT NULL, 
       descripcion     text NULL, 
       estado          varchar(10) NOT NULL,
       PRIMARY KEY (premio_id),
       FOREIGN KEY (categoria_id) REFERENCES Categoria,
       FOREIGN KEY (concurso_id) REFERENCES Concurso
)
go