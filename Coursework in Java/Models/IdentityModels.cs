using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Coursework_in_Java.AppKernel.DatabaseConfigurations.FluentApiConfigs;
using Coursework_in_Java.Models.Inspectors;
using Coursework_in_Java.Models.Tax;
using Coursework_in_Java.Models.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Coursework_in_Java.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string TaxIdentification { get; set; } = Guid.NewGuid().ToString();
        public string InspectorId { get; set; } = Guid.Empty.ToString();

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            userIdentity.AddClaim(new Claim("userTId", this.TaxIdentification));
            userIdentity.AddClaim(new Claim("userIId", this.InspectorId));

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Configurations.Add(new CitizenInformationDetailModelConfig());
            modelBuilder.Configurations.Add(new CitizenInformationModelConfig());
            modelBuilder.Configurations.Add(new DeclarationCheckModelConfig());
            modelBuilder.Configurations.Add(new TaxDeclarationModelConfig());
            modelBuilder.Configurations.Add(new InspectorModelConfig());
            modelBuilder.Configurations.Add(new TaxDeclarationDetailModelConfig());
        }

        public virtual DbSet<IncomeModel> Incomes { get; set; }
        public virtual DbSet<TaxModel> Taxes { get; set; }
        public virtual DbSet<PhotoModel> Photos { get; set; }
        public virtual DbSet<AddressModel> Addresses { get; set; }
        public virtual DbSet<PhoneModel> Phones { get; set; }
        public virtual DbSet<CitizenInformationModel> CitizenInformation { get; set; }
        public virtual DbSet<CitizenInformationDetailModel> CitizenInformationDetails { get; set; }
        public virtual DbSet<TaxDeclarationModel> TaxDeclarations { get; set; }
        public virtual DbSet<TaxDeclarationDetailModel> TaxDeclarationDetails { get; set; }
        public virtual DbSet<InspectorModel> Inspectors { get; set; }
        public virtual DbSet<DeclarationCheckModel> DeclarationChecks { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
    }
}