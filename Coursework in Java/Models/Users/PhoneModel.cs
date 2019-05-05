using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.Models.Users
{
    public class PhoneModel
    {
        public int Id { get; set; }

        [Required, Display(Name = "Мобільний телефон"), Phone]
        public string MobilePhone1 { get; set; }

        //[Display(Name = "Додатковий телефон"), Phone]
        //public string MobilePhone2 { get; set; }

        //[Display(Name = "Домашній телефон"), Phone]
        //public string HomePhone { get; set; }

        //[Display(Name = "Робочий телефон"), Phone]
        //public string WorkPhone { get; set; }

        public CitizenInformationDetailModel CitizenInformationDetail { get; set; }
    }
}