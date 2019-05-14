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
    public class UserInitializerCommand : BaseCommand
    {
        public override void Execute(ApplicationDbContext db)
        {
            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var user = new ApplicationUser
            {
                Email = "user@java.com",
                UserName = "user@java.com",
                InspectorId = Guid.Empty.ToString(),
                TaxIdentification = "user"
            };
            string password = "java123";

            var result = userManager.Create(user, password);

            if (result.Succeeded)
            {
                var roles = roleManager.Roles.ToList();

                foreach (var role in roles)
                {
                    if (HasNeededRole(role.Name))
                    {
                        userManager.AddToRole(user.Id, role.Name);

                    }
                }

            }
        }

        private bool HasNeededRole(string value)
        {
            UserRoles role = (UserRoles)Enum.Parse(typeof(UserRoles), value);

            switch (role)
            {
                case UserRoles.User:
                    return true;

                case UserRoles.Admin:
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