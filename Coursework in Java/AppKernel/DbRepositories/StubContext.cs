using Coursework_in_Java.Models;
using Coursework_in_Java.Models.Inspectors;
using Coursework_in_Java.Models.Tax;
using Coursework_in_Java.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.AppKernel.DbRepositories
{
    public class StubContext
    {
        public ICollection<IncomeModel> Incomes                                     { get; set; } = new List<IncomeModel>();
        public ICollection<TaxModel> Taxes                                          { get; set; } = new List<TaxModel>();
        public ICollection<PhotoModel> Photos                                       { get; set; } = new List<PhotoModel>();
        public ICollection<AddressModel> Addresses                                  { get; set; } = new List<AddressModel>();
        public ICollection<PhoneModel> Phones                                       { get; set; } = new List<PhoneModel>();
        public ICollection<CitizenInformationModel> CitizenInformation              { get; set; } = new List<CitizenInformationModel>();
        public ICollection<CitizenInformationDetailModel> CitizenInformationDetails { get; set; } = new List<CitizenInformationDetailModel>();
        public ICollection<TaxDeclarationModel> TaxDeclarations                     { get; set; } = new List<TaxDeclarationModel>();
        public ICollection<TaxDeclarationDetailModel> TaxDeclarationDetails         { get; set; } = new List<TaxDeclarationDetailModel>();
        public ICollection<InspectorModel> Inspectors                               { get; set; } = new List<InspectorModel>();
        public ICollection<DeclarationCheckModel> DeclarationChecks                 { get; set; } = new List<DeclarationCheckModel>();
        public ICollection<Posts> Posts                                             { get; set; } = new List<Posts>();
    }
}