using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P0006.Models.ViewModels
{
	public class EditUserViewModels
	{
        public int Id { get; set; }

		[Required]
		[Display(Name = "Nombre Completo")]
        public string Nombre { get; set; }

		[Required]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

		[Required]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirmar Contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden. Por favor revisar!")]
        public string ConfirmaPassword { get; set; }

        [Required]
        [Display(Name = "Edad")]
        public int? Edad { get; set; }
    }
}