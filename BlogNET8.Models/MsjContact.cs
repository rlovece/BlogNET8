using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogNET8.Models
{
    public class MsjContact
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Su Nombre es requerido")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Su Apellido es requerido")]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Su Email es requerido para enviarle una respuesta")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage ="Respete el formato: email@example.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Su mensaje es requerido")]
        [Display(Name = "Mensaje")]
        public string Message { get; set; }

        public bool Answered { get; set; } = false;
    }
}
