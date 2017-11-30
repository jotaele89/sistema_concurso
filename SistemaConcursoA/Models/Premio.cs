namespace SistemaConcursoA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.Entity;
    using System.Linq;
    using System.Data;
  

    [Table("Premio")]
    public partial class Premio
    {
        [Key]
        public int premio_id { get; set; }

        public int categoria_id { get; set; }

        public int concurso_id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Column(TypeName = "text")]
        public string descripcion { get; set; }

        [Required]
        [StringLength(10)]
        public string estado { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual Concurso Concurso { get; set; }

        ////////////////////////////////////////////////

        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();

                    var query = db.Premio.Include("Concurso").Include("Categoria").Where(x => x.premio_id >0);

                    if (grilla.columna == "premio_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.premio_id)
                                                               : query.OrderBy(x => x.premio_id);
                    }

                    if (grilla.columna == "nombre")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.concurso_id)
                                                               : query.OrderBy(x => x.concurso_id);
                    }
                    if (grilla.columna == "nombrepremio")
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
                        if (f.columna == "nombrepremio" && f.valor != "")
                            query = query.Where(x => x.nombre.Contains(f.valor));

                        if (f.columna == "nombre" && f.valor != "")
                            query = query.Where(x => x.Concurso.concurso_id.ToString().Contains(f.valor));

                    }


                    var premio = query.Skip(grilla.pagina)
                                      .Take(grilla.limite)
                                      .ToList();

                    var total = query.Count();

                    grilla.SetData(from s in premio
                                   select new
                                   {
                                       s.premio_id,
                                       s.Concurso.nombre,
                                       nombrecategoria=s.Categoria.nombre,
                                       nombrepremio = s.nombre,
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
        public List<Categoria> listarcategoria() //retornar es un Collection
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

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    if (this.premio_id > 0)
                    {
                        db.Entry(this).State = EntityState.Modified;
                    }
                    else {
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

        public Premio Obtener(int id)
        {
            var premio = new Premio();

            try
            {
                using (var db = new ModeloDatos())
                {
                    premio = db.Premio.Include("Concurso").Include("Categoria").
                        Where(x => x.premio_id == id).
                        SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return premio;
        }

       

     

    
    }
}