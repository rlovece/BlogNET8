using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogNET8.Models
{
	public class Article
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "El título es requerido")]
		[Display(Name = "Título")]
		public string Title { get; set; }

		[Required(ErrorMessage = "La descripción es requerida")]
		[Display(Name = "Descripción")]
		public string Description { get; set; }

		public DateTime CreatedDate { get; set; } = DateTime.Now;

		public DateTime UpdatedDate { get; set; }

		[DataType(DataType.ImageUrl)]
		[Display(Name = "URL Imagen")]
		public string? UrlImagen { get; set; }

		//[Required(ErrorMessage = "La Categoría es requerida")]
		public int CategoryId { get; set; }

		[ForeignKey("CategoryId")]
		public Category? Category { get; set; }
	}
}
