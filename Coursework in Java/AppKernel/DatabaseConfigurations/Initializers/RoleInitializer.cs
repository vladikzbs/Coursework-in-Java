using Coursework_in_Java.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.AppKernel.DatabaseConfigurations.Initializers
{
    public enum UserRoles
    {
        Undefined,

        Admin,
        Director,
        Inspector,
        User,
        Guest
    }
    public class RoleInitializer : IInitializeStrategy
    {
        public RoleInitializer()
        {
            Roles = new List<IdentityRole>(GetStandartRoles());
        }

        public IReadOnlyCollection<IdentityRole> Roles { get; }
        public Usage UsageStatus { get; set; } = Usage.Yes;

        public void Initialize(ApplicationDbContext context)
        {
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            foreach (var role in Roles)
            {
                roleManager.Create(role);
            }
        }

        private IEnumerable<IdentityRole> GetStandartRoles()
        {
            return new List<IdentityRole>
            {
                new IdentityRole {Name = UserRoles.Admin.ToString()},
                new IdentityRole {Name = UserRoles.Director.ToString()},
                new IdentityRole {Name = UserRoles.Inspector.ToString()},
                new IdentityRole {Name = UserRoles.User.ToString()},
                new IdentityRole {Name = UserRoles.Guest.ToString()},
            };
        }
    }
}