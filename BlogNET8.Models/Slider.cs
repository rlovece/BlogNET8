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
		public string Name { get; set; }

		public bool Status { get; set; }

		public string? UrlImagen { get; set; }
	}
}
