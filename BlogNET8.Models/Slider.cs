using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogNET8.Models
{
	public class Slider
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage ="El nombre es requerido")]
		[Display(Name="Nombre Slider")]
		public string Name { get; set; }

		[Display(Name = "Estado Slider")]
		public bool Status { get; set; }

		[DataType(DataType.ImageUrl)]
		[Display(Name = "URL Imagen")]
		public string? UrlImagen { get; set; }
	}
}
