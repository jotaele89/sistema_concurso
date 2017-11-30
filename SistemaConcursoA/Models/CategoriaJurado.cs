namespace SistemaConcursoA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoriaJurado")]
    public partial class CategoriaJurado
    {
        [Key]
        public int categoriajurado_id { get; set; }

        public int categoria_id { get; set; }

        public int persona_id { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual Persona Persona { get; set; }
    }
}
