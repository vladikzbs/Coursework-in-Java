using Coursework_in_Java.Models.Tax;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.AppKernel.DatabaseConfigurations.FluentApiConfigs
{
    public class TaxDeclarationModelConfig : EntityTypeConfiguration<TaxDeclarationModel>
    {
        public TaxDeclarationModelConfig()
        {
            HasRequired(x => x.TaxDeclarationDetail).WithRequiredPrincipal(x => x.TaxDeclaration);
            HasRequired(x => x.DeclarationCheck).WithOptional(x => x.TaxDeclaration);
            Property(x => x.DateOfFilling).HasColumnType("datetime");
        }
    }
}