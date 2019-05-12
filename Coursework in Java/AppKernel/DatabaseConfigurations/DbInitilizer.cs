using Coursework_in_Java.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Reflection;
using Coursework_in_Java.AppKernel.DatabaseConfigurations.Initializers;

namespace Coursework_in_Java.AppKernel.DatabaseConfigurations
{
    // public class DbInitilizer : DropCreateDatabaseAlways<ApplicationDbContext>

    public class DbInitilizer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        private CommandInvoker commandInvoker;

        public DbInitilizer()
        {
            this.commandInvoker = new CommandInvoker();
            InitializeCommandInvoker();
        }

        protected override void Seed(ApplicationDbContext db)
        {
            this.commandInvoker.ExecuteCommands(db);

            base.Seed(db);
        }

        private void InitializeCommandInvoker()
        {
            // Order matters!!!
            // Standart initializers data
            this.commandInvoker.StoreCommand(new RoleInitializerCommand());
            this.commandInvoker.StoreCommand(new UserInitializerCommand());
            this.commandInvoker.StoreCommand(new CitizenInitializeCommand());
            this.commandInvoker.StoreCommand(new InspectorsInitializeCommand());
            this.commandInvoker.StoreCommand(new InspectorsAccountInitializeCommand());
        }
    }
}