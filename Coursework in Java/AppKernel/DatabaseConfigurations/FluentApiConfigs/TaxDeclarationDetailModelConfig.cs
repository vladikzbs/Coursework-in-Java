using Coursework_in_Java.Models.Tax;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.AppKernel.DatabaseConfigurations.FluentApiConfigs
{
    public class TaxDeclarationDetailModelConfig : EntityTypeConfiguration<TaxDeclarationDetailModel>
    {
        public TaxDeclarationDetailModelConfig()
        {
            HasRequired(x => x.Tax).WithRequiredPrincipal(x => x.TaxDeclarationDetail);
            HasRequired(x => x.Income).WithRequiredPrincipal(x => x.TaxDeclarationDetail);
        }
    }
}