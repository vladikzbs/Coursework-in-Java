using Coursework_in_Java.Models.Tax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.Models.Inspectors
{
    public class DeclarationCheckModel 
    {
        public int InspectorId { get; set; }
        public int DeclarationId { get; set; }
        public int LineItem { get; set; }

        [Required, Display(Name = "Перевірена")]
        public bool? Checked { get; set; }

        [Display(Name = "Пройшла перевірку")]
        public bool? Passed { get; set; }

        [Display(Name = "Коментар інспектора")]
        public string Message { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "Дата початку перевірки")]
        public DateTime? DateOfStart { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "Дата кінця перевірки")]
        public DateTime? DateOfEnd { get; set; }

        // [ForeignKey(nameof(InspectorId))]
        [Display(Name = "Інспектор")]
        public InspectorModel Inspector { get; set; }

        //[ForeignKey(nameof(DeclarationId))]
        public TaxDeclarationModel TaxDeclaration { get; set; }


        public static DeclarationCheckModel Default
        {
            get
            {
                return new DeclarationCheckModel();
            }
        }
    }
}