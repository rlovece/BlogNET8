using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogNET8.Models
{
    public class Category
    {
        /*Los decoradores arriba de cada atributo son Data Notation*/
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="La categoría es requerida")]
        [Display (Name = "Nombre de Categoría")]
        public string Name { get; set; }

		[Display(Name = "Orden")]
        [Range(1, 10, ErrorMessage ="Debe estar entre 1 y 10")]
		public int? Order {  get; set; }

    }
}
