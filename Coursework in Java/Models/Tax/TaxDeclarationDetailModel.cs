using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.Models.Tax
{
    public enum PayerCaterogy
    {
        Громядянин,
        Незалежний,
        Підприємець,
    }

    public class TaxDeclarationDetailModel
    {
        public int Id { get; set; }

        [Required, Display(Name = "Назва контролюючого органу")]
        public string NameOfControllingBody { get; set; }

        [Required, Display(Name = "Статус резидента")]
        public bool IsResident { get; set; }

        [Required, Display(Name = "Особисто заповнює декларацію")]
        public bool IsHimselfFillsDeclaration { get; set; }

        [Required, Display(Name = "Категорія платника")]
        public PayerCaterogy PayerCaterogy { get; set; }

        public TaxDeclarationModel TaxDeclaration { get; set; }
        public IncomeModel Income { get; set; }
        public TaxModel Tax { get; set; }
    }
}