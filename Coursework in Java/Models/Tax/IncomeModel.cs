using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.Models.Tax
{
    public class IncomeModel
    {
        public int Id { get; set; }

        [Required, Display(Name = "Дохід зарплати")]
        public decimal SalaryIncome { get; set; }

        [Required, Display(Name = "Дохід із обміну/продажу майна")]
        public decimal ExchangeSoldPropertyIncome { get; set; }

        [Required, Display(Name = "Дохід інвестицій")]
        public decimal InvestmentsIncome { get; set; }

        [Required, Display(Name = "Дохід від спадку/дарунків")]
        public decimal HeritageDonatedIncome { get; set; }

        [Required, Display(Name = "Інші доходи")]
        public decimal OtherIncome { get; set; }

        [NotMapped]
        [Display(Name = "Загальний дохід")]
        public decimal Total
        {
            get
            {
                return SalaryIncome
                    + ExchangeSoldPropertyIncome
                    + InvestmentsIncome
                    + HeritageDonatedIncome
                    + OtherIncome;
            }
        }

        public TaxDeclarationDetailModel TaxDeclarationDetail { get; set; }
    }
}