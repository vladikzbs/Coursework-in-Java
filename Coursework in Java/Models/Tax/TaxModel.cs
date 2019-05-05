using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.Models.Tax
{
    public class TaxModel
    {
        public int Id { get; set; }

        [Required, Display(Name = "Податки за кордоном")]
        public decimal AbroadTaxes { get; set; }

        [Required, Display(Name = "Податки громадянина")]
        public decimal CitizenTax { get; set; }

        [Required, Display(Name = "Військовий збір")]
        public decimal MilitaryTax { get; set; }

        [NotMapped]
        [Display(Name = "Загальна сума податку")]
        public decimal TotalTax { get { return AbroadTaxes + CitizenTax + MilitaryTax; } }

        public TaxDeclarationDetailModel TaxDeclarationDetail { get; set; }
    }
}