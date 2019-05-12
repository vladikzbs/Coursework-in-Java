using Coursework_in_Java.Models.Tax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.Models.Users
{
    public class CitizenInformationModel 
    {
        public int Id { get; set; }

        [Required, Display(Name = "Ім'я")]
        public string Name { get; set; }

        [Required, Display(Name = "Фамілія")]
        public string Surname { get; set; }

        [Required, Display(Name = "Прізвище")]
        public string Patronymic { get; set; } 

        [Required, Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        [NotMapped]
        [Display(Name = "Ім'я")]
        public string FullName { get { return Surname + " " + Name + " " + Patronymic; } }

        public CitizenInformationDetailModel CitizenInformationDetail { get; set; }
        public ICollection<TaxDeclarationModel> TaxDeclarations { get; set; } = new List<TaxDeclarationModel>();
    }
}