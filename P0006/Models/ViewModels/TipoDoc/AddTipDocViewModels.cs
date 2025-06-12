using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P0006.Models.ViewModels.TipoDoc
{
	public class AddTipDocViewModels
	{
		[Required]
		[Display(Name = "Tipo Documento")]
        [StringLength(3, ErrorMessage = "El código del tipo de documento debe tener 3 caracteres.")]
        public string TIPDOC { get; set; }

        [Required]
        [Display(Name = "Origen, debito / crédito  ")]
        [StringLength(1, ErrorMessage = "El origen debe ser un carácter.")]
        public string ORIGEN { get; set; }

        [Required]
        [Display(Name = "Descripción / nombre")]
        [StringLength(50, ErrorMessage = "La descripción no puede exceder los 50 caracteres.")]
        public string DESCRIPCION { get; set; }

        [Required]
        [Display(Name = "Cuenta Contable Debito")]
        [StringLength(20, ErrorMessage = "La cuenta contable debe tener un máximo de 20 caracteres.")]
        public string CtaDebito { get; set; }

        [Required]
        [Display(Name = "Cuenta Contable Crédito")]
        [StringLength(20, ErrorMessage = "La cuenta contable debe tener un máximo de 20 caracteres.")]
        public string CtaCredito { get; set; }

    }
}