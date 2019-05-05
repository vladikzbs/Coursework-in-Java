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
    public class RoleInitializerCommand : BaseCommand
    {
        public RoleInitializerCommand()
        {
            Roles = new List<IdentityRole>(GetStandartRoles());
        }

        public IReadOnlyCollection<IdentityRole> Roles { get; }

        public override void Execute(ApplicationDbContext db)
        {
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            foreach (var role in Roles)
            {
                roleManager.Create(role);
            }
        }

        private IEnumerable<IdentityRole> GetStandartRoles()
        {
            return new List<IdentityRole>
            {
                //new IdentityRole {Name = UserRoles.Admin.ToString()},
                //new IdentityRole {Name = UserRoles.Director.ToString()},
                new IdentityRole {Name = UserRoles.Inspector.ToString()},
                new IdentityRole {Name = UserRoles.User.ToString()},
            };
        }
    }
}