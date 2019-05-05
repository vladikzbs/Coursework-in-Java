using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.Models.Users
{
    public class CitizenInformationDetailModel
    {
        public int Id { get; set; }

        [Display(Name = "Податковий номер")]
        public string TaxCardNumber { get; set; }

        [Required, Display(Name = "Поштовий індекс")]
        public string PostIndex { get; set; }

        public PhoneModel Phone { get; set; }
        public AddressModel Address { get; set; }
        public CitizenInformationModel CitizenInformation { get; set; }
    }
}