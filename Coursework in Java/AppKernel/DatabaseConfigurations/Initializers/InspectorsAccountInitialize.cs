using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coursework_in_Java.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Coursework_in_Java.AppKernel.DatabaseConfigurations.Initializers
{
    public class InspectorsAccountInitialize : IInitializeStrategy
    {
        public Usage UsageStatus { get; set; } = Usage.Yes;

        public void Initialize(ApplicationDbContext context)
        {
            //using (var transaction = context.Database.BeginTransaction())
            //{
            //    try
            //    {
            var inspectors = context.Inspectors.ToList();

            // For template
            string email = "inspector{0}@mail.com";
            string password = "ins123";

            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            List<ApplicationUser> users = new List<ApplicationUser>();
            for (int i = 0; i < 10; i++)
            {
                users.Add(new ApplicationUser
                {
                    Email = string.Format(email, i),
                    UserName = string.Format(email, i),
                    InspectorId = inspectors[i].SpecialNumber
                });
            }

            List<IdentityResult> identityResults = new List<IdentityResult>();
            foreach (var user in users)
            {
                identityResults.Add(userManager.Create(user, password));
            }

            IdentityRole inspectorRole = roleManager.FindByName("Inspector");

            if (inspectorRole == null)
                throw new Exception("Не найдена роль инспектора");

            context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                if (identityResults[i].Succeeded)
                {
                    userManager.AddToRole(users[i].Id, inspectorRole.Name);
                }
            }

            context.SaveChanges();

            //    transaction.Commit();
            //}
            //catch (Exception ex)
            //{
            //    transaction.Rollback();
            //}
        }
    }
}
