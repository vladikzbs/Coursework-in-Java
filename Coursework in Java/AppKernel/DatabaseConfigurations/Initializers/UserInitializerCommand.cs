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