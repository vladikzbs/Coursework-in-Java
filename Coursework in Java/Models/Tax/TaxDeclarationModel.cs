using Coursework_in_Java.AppKernel.MainInterfaces;
using Coursework_in_Java.Models.Inspectors;
using Coursework_in_Java.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.Models.Tax
{
    public enum DeclarationType
    {
        Звітна,
        Нова,
        Уточнююча,
    }

    public class TaxDeclarationModel : ITaxDeclaration
    {
        public int Id { get; set; }

        [Required, Display(Name = "Унікальний ідентифікатор декларації")]
        public string UniqueDeclarationId { get; set; }

        [Required, Display(Name = "Тип декларації")]
        public DeclarationType DeclarationType { get; set; }

        [Column(TypeName = "datetime2"), DataType(DataType.Date)]
        [Required, Display(Name = "Податковий період")]
        public DateTime ReportingPeriod { get; set; }

        [Required, Display(Name = "Квартал")]
        public int Quarter { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required, Display(Name = "Дата подачі")]
        public DateTime? DateOfFilling { get; set; }

       [ Display(Name = "Громадянин")]
        public CitizenInformationModel CitizenInformation { get; set; }
        public TaxDeclarationDetailModel TaxDeclarationDetail { get; set; }
        public DeclarationCheckModel DeclarationCheck { get; set; }

    }
}