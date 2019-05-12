using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.Models.Users
{
    public class AddressModel 
    {
        public int Id { get; set; }

        //[Required, Display(Name = "Область")]
        //public string Region { get; set; }

        //[Required, Display(Name = "Район")]
        //public string District { get; set; }

        [Required, Display(Name = "Місто")]
        public string City { get; set; }

        [Required, Display(Name = "Вулиця")]
        public string Street { get; set; }

        [Required, Display(Name = "Номер будинку")]
        public string HouseNumber { get; set; }

        //[Display(Name = "Корпус")]
        //public string Corps { get; set; }

        [Required, Display(Name = "Номер квартири")]
        public string AppartmentNumber { get; set; }

        public CitizenInformationDetailModel CitizenInformationDetail { get; set; }
    }
}