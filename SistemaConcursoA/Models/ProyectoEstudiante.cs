namespace SistemaConcursoA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Data.Entity;

    [Table("ProyectoEstudiante")]
    public partial class ProyectoEstudiante
    {
        [Key]
        public int proyectoestudiante_id { get; set; }

        public int proyecto_id { get; set; }

        public int persona_id { get; set; }

        public virtual Persona Persona { get; set; }

        public virtual Proyecto Proyecto { get; set; }


        //todofrontend
        public void Guardarp()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    db.Database.ExecuteSqlCommand(
                        "insert into ProyectoEstudiante values(@proyecto_id,@persona_id)",

                        new SqlParameter("proyecto_id", this.proyecto_id),
                        new SqlParameter("persona_id", this.persona_id)
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void GuardarIntegrante(string codigo, string proy_id)
        {
            Persona pers = new Persona();
            List<ProyectoEstudiante> existente = new List<ProyectoEstudiante>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    pers = db.Persona.Include("TipoPersona").Where(x => x.codigo.Contains(codigo) && (x.TipoPersona.nombre.Contains("Alumno") || x.TipoPersona.nombre.Contains("Estudiante") || x.TipoPersona.nombre.Contains("Participante"))).SingleOrDefault();

                    if (pers != null)
                    {
                        int person = pers.persona_id;
                        existente = db.ProyectoEstudiante.Where(x => x.persona_id == person && x.proyecto_id.ToString().Contains(proy_id)).ToList();

                        if (existente.Count == 0)
                        {
                            db.Database.ExecuteSqlCommand(
                        "insert into ProyectoEstudiante values(@proyecto_id,@persona_id)",

                        new SqlParameter("proyecto_id", Convert.ToInt32(proy_id)),
                        new SqlParameter("persona_id", Convert.ToInt32(pers.persona_id))
                        );
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public ProyectoEstudiante Obtener(int id)
        {
            var proyectoestudiante = new ProyectoEstudiante();

            try
            {
                using (var db = new ModeloDatos())
                {
                    proyectoestudiante = db.ProyectoEstudiante.Include("Proyecto").Include("Persona").Where(x => x.proyectoestudiante_id == id).
                        SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyectoestudiante;
        }

        public Proyecto listarproyecto(int codigo)
        {
            var proyecto = new Proyecto();

            try
            {
                using (var db = new ModeloDatos())
                {

                    proyecto = db.Proyecto.Where(t => t.proyecto_id == codigo).SingleOrDefault();
                    //  proyecto = (from e in Proyecto
                    // select e.proyecto_id).Last();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyecto;
        }

        public List<Persona> listarpersona()
        {
            var persona = new List<Persona>();

            try
            {
                using (var db = new ModeloDatos())
                {

                    persona = db.Persona.Where(t => t.tipopersona_id == 2).ToList();
                    //  proyecto = (from e in Proyecto
                    // select e.proyecto_id).Last();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return persona;
        }

        public List<ProyectoEstudiante> listarintegrantes(int codigo)
        {
            var persona = new List<ProyectoEstudiante>();

            try
            {
                using (var db = new ModeloDatos())
                {

                    persona = db.ProyectoEstudiante.Include("Persona").Where(t => t.proyecto_id == codigo).ToList();
                    //  proyecto = (from e in Proyecto
                    // select e.proyecto_id).Last();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return persona;
        }

        public int listarcodproyecto()
        {
            var proyecto = new Proyecto();
            int codigo = 0;
            try
            {
                using (var db = new ModeloDatos())
                {

                    proyecto = db.Proyecto.OrderByDescending(x => x.proyecto_id).First();
                    codigo = proyecto.proyecto_id;
                    //  proyecto = (from e in Proyecto
                    // select e.proyecto_id).Last();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return codigo;
        }


        public AnexGRIDResponde ListarGrilla(int id, AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();

                    var query = db.ProyectoEstudiante.Include("Persona").Where(x => x.proyectoestudiante_id > 0);

                    if (grilla.columna == "proyectoestudiante_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.proyectoestudiante_id)
                                                               : query.OrderBy(x => x.proyectoestudiante_id);
                    }

                    if (grilla.columna == "proyecto_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.proyecto_id)
                                                               : query.OrderBy(x => x.proyecto_id);
                    }

                    if (grilla.columna == "persona_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.persona_id)
                                                               : query.OrderBy(x => x.persona_id);
                    }


                    var proyectoestudiante = query.Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in proyectoestudiante
                                   select new
                                   {

                                       codigopersona = s.Persona.codigo,
                                       nombrepersona = s.Persona.nombre,
                                       apellidopersona = s.Persona.apellido
                                   }, total);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return grilla.responde();
        }

        public void eliminarintregrante(int proy_id, int persona)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    db.Database.ExecuteSqlCommand(
                            "delete from ProyectoEstudiante where proyecto_id = @proyecto_id AND persona_id=@persona_id",
                            new SqlParameter("proyecto_id", proy_id),
                            new SqlParameter("persona_id", persona)
                            );
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
                            "delete from ProyectoEstudiante where proyectoestudiante_id = @proyectoestudiante_id",
                            new SqlParameter("proyectoestudiante_id", this.proyectoestudiante_id)
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
        public List<ProyectoEstudiante> listarpestudiante()
        {
            var proyectoestudiante = new List<ProyectoEstudiante>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    proyectoestudiante = db.ProyectoEstudiante.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyectoestudiante;
        }
    }
}
