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
        public string TIPDOC { get; set; }

        [Required]
        [Display(Name = "Origen, debito / crédito  ")]
        public string ORIGEN { get; set; }

        [Required]
        [Display(Name = "Descripción / nombre")]
        public string DESCRIPCION { get; set; }

        [Required]
        [Display(Name = "Cuenta Contable Debito")]
        public string CtaDebito { get; set; }

        [Required]
        [Display(Name = "Cuenta Contable Crédito")]
        public string CtaCredito { get; set; }

    }
}