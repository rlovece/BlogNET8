using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace BlogNET8.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required(ErrorMessage = "El Nombre es requerido")]
		[Display(Name ="Nombre")]
		public String Name { get; set; }

		[Required(ErrorMessage = "El Apellido es requerido")]
		[Display(Name = "Apellido")]
		public String LastName { get; set; }

		[Required(ErrorMessage = "El Domicilio es requerido")]
		[Display(Name = "Domicilio")]
		public String Address { get; set; }

		[Required(ErrorMessage = "La Ciudad es requerido")]
		[Display(Name = "Ciudad")]
		public String City { get; set; }
	}
}
