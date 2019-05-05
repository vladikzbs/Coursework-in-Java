using Coursework_in_Java.AppKernel.Roles;
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
    public class UserInitializer : IInitializeStrategy
    {
        public Usage UsageStatus { get; set; } = Usage.Yes;

        public void Initialize(ApplicationDbContext context)
        {
            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var admin = new ApplicationUser
            {
                Email = "admin@java.com",
                UserName = "admin@java.com",
                InspectorId = Guid.Empty.ToString()
            };
            string password = "admin123";

            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                if (roleManager.RoleExists(UserRoles.Admin.ToString()))
                {
                    var roles = roleManager.Roles.ToList();

                    foreach (var role in roles)
                    {
                        if (HasNeededRole(role.Name))
                        {
                            userManager.AddToRole(admin.Id, role.Name);

                        }
                    }
                }
            }
        }

        private bool HasNeededRole(string value)
        {
            UserRoles role = (UserRoles)Enum.Parse(typeof(UserRoles), value);

            switch (role)
            {
                case UserRoles.Admin:
                case UserRoles.User:
                    return true;

                case UserRoles.Director:
                case UserRoles.Inspector:
                case UserRoles.Undefined:
                case UserRoles.Guest:
                default:
                    return false;
            }
        }
    }
}