using Coursework_in_Java.Models.Inspectors;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.AppKernel.DatabaseConfigurations.FluentApiConfigs
{
    public class InspectorModelConfig : EntityTypeConfiguration<InspectorModel>
    {
        public InspectorModelConfig()
        {
            HasRequired(x => x.Photo).WithRequiredPrincipal(x => x.Inspector);
        }
    }
}