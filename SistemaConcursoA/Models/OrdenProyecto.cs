 namespace SistemaConcursoA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("OrdenProyecto")]
    public partial class OrdenProyecto
    {
        [Key]
        public int ordenproyecto_id { get; set; }

        public int proyecto_id { get; set; }

        public int orden { get; set; }

        public TimeSpan horaexposicion { get; set; }

        [Required]
        [StringLength(10)]
        public string estado { get; set; }

        public virtual Proyecto Proyecto { get; set; }

        //todofrontend

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();

                    var query = db.OrdenProyecto.Include("Proyecto").Where(x => x.ordenproyecto_id > 0).Where(x => x.estado.Equals("Activo"));

                    if (grilla.columna == "ordenproyecto_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.ordenproyecto_id)
                                                               : query.OrderBy(x => x.ordenproyecto_id);
                    }
                    if (grilla.columna == "proyecto_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.proyecto_id)
                                                               : query.OrderBy(x => x.proyecto_id);
                    }

                    if (grilla.columna == "ordenproyecto")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.orden)
                                                               : query.OrderBy(x => x.orden);
                    }

                    if (grilla.columna == "horaexposicion")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.horaexposicion)
                                                               : query.OrderBy(x => x.horaexposicion);
                    }



                    var ordenproyecto = query.Skip(grilla.pagina)
                                      .Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in ordenproyecto
                                   select new
                                   {
                                       s.ordenproyecto_id,
                                       s.proyecto_id,
                                       s.Proyecto.nombre_proyecto,
                                       s.orden,

                                   }, total);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return grilla.responde();
        }

        public List<Proyecto> listarproyecto()
        {
            var proyecto = new List<Proyecto>();

            try
            {
                using (var db = new ModeloDatos())
                {
                    proyecto = db.Proyecto.Where(x => x.estado.Contains("Activo")).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return proyecto;
        }
    }
}
