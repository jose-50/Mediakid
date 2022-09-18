using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MediaKid.EntidadesDeNegocio
{
   public  class Video

    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Categoria")]
        [Required(ErrorMessage = "Categoria es obligatorio")]
        [Display(Name = "Categoria")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Url es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Descripcion es obligatorio")]
        [StringLength(25, ErrorMessage = "Maximo 30 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Duracion es obligatorio")]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "Password debe estar entre 5 a 32 caracteres", MinimumLength = 5)]
        public string Duracion { get; set; }

        

        

        public Categoria Categoria { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        
    }

}

