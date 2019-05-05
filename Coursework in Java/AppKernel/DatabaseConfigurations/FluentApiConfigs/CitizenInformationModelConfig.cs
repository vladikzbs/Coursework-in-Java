using Coursework_in_Java.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.AppKernel.DatabaseConfigurations.FluentApiConfigs
{
    public class CitizenInformationModelConfig : EntityTypeConfiguration<CitizenInformationModel>
    {
        public CitizenInformationModelConfig()
        {
            HasRequired(x => x.CitizenInformationDetail).WithRequiredPrincipal(x => x.CitizenInformation);
            HasMany(x => x.TaxDeclarations).WithRequired(x => x.CitizenInformation);
        }
    }
}