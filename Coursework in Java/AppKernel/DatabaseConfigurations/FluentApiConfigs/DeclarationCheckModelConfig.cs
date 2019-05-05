using Coursework_in_Java.Models.Inspectors;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.AppKernel.DatabaseConfigurations.FluentApiConfigs
{
    public class DeclarationCheckModelConfig : EntityTypeConfiguration<DeclarationCheckModel>
    {
        public DeclarationCheckModelConfig()
        {
            HasKey(x => x.LineItem);
            Property(x => x.DateOfEnd).HasColumnType("datetime");
            Property(x => x.DateOfStart).HasColumnType("datetime");

        }
    }
}