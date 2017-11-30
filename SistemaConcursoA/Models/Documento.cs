namespace SistemaConcursoA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Web;
    using System.Data.Entity;
    using System.Data;
    using System.IO;
    using System.Data.SqlClient;

    [Table("Documento")]
    public partial class Documento
    {
        [Key]
        public int documento_id { get; set; }

        public int proyecto_id { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre { get; set; }

        [Column(TypeName = "text")]
        public string descripcion { get; set; }

        [StringLength(3)]
        public string extension { get; set; }

        [StringLength(15)]
        public string tamanio { get; set; }

        [Required]
        [StringLength(10)]
        public string estado { get; set; }

        public virtual Proyecto Proyecto { get; set; }


        //guardardocumento
        //Todo frontend
        //guardardocumento
        public ResponseModel GuardarDocumento(HttpPostedFileBase rar)
        {
            var rm = new ResponseModel();
            string nombrearchivo = "";
            try
            {
                using (var db = new ModeloDatos())
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    var Doc = db.Entry(this);
                    Doc.State = System.Data.Entity.EntityState.Modified;
                    if (rar != null)
                    {
                        string extension = Path.GetExtension(rar.FileName).ToLower();
                        var filtroextension = new[] { ".rar" };
                        var extensiones = Path.GetExtension(rar.FileName);

                        if (filtroextension.Contains(extensiones))
                        {
                            string archivo = Path.GetFileName(rar.FileName);
                            nombrearchivo = Path.GetFileName(rar.FileName);
                            rar.SaveAs(HttpContext.Current.Server.MapPath("~/Server/Docs/" + archivo));
                            this.nombre = archivo;

                            this.tamanio = rar.ContentLength.ToString();
                            this.nombre = nombrearchivo;

                            string value = extension;
                            char delimiter = '.';
                            string[] substrings = value.Split(delimiter);
                            foreach (var substring in substrings)
                            {
                                this.extension = substring;
                            }

                            this.estado = "Activo";

                        }

                    }

                    ///  else rar.Property(X => X.nombre).IsModified = false;
                    //if (this.estado == null) rar.Property(x => x.estado).IsModified = false;
                    db.Entry(this).State = EntityState.Added; //si el valor es  cero va a agregar
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
        public List<Proyecto> listarproyecto()
        {
            var proyecto = new List<Proyecto>();

            try
            {
                using (var db = new ModeloDatos())
                {

                    proyecto = db.Proyecto.OrderByDescending(t => t.proyecto_id).ToList();
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

        public Documento Obtener(int id)
        {
            var documento = new Documento();

            try
            {
                using (var db = new ModeloDatos())
                {
                    documento = db.Documento.Include("Proyecto").
                        Where(x => x.documento_id == id).
                        SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return documento;
        }


    }
}
