using Coursework_in_Java.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.AppKernel.DatabaseConfigurations.FluentApiConfigs
{
    public class CitizenInformationDetailModelConfig : EntityTypeConfiguration<CitizenInformationDetailModel>
    {
        public CitizenInformationDetailModelConfig()
        {
            HasRequired(x => x.Phone).WithRequiredPrincipal(x => x.CitizenInformationDetail);
            HasRequired(x => x.Address).WithRequiredPrincipal(x => x.CitizenInformationDetail);
        }
    }
}