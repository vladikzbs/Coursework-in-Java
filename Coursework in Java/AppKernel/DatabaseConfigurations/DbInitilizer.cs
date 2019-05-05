using Coursework_in_Java.AppKernel.Roles;
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
    //public class DbInitilizer : DropCreateDatabaseAlways<ApplicationDbContext>

    public class DbInitilizer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        private IReadOnlyCollection<IInitializeStrategy> initializers;

        public DbInitilizer()
        {
            this.initializers = GetInitializers();
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //new RoleInitializer().InitilizeInBaseStandartRoles(context);
            //new UserInitializer().InitializeSiteDirection(context);
            //new InspectorsInitialize().Initialize(context);
            //new CitizenInitialize().InitializeCitizens(context);

            foreach (var initializer in this.initializers)
            {
                initializer.Initialize(context);
            }

            base.Seed(context);
        }

        private IReadOnlyCollection<IInitializeStrategy> GetInitializers()
        {
            List<IInitializeStrategy> initializers = new List<IInitializeStrategy>();
            //Type baseType = typeof(IInitializeStrategy);

            //var types = Assembly.GetExecutingAssembly()
            //    .GetTypes();
            //    //.Where(x => x != baseType && x.IsAssignableFrom(baseType))
            //    //.ToList();

            //var types2 = types.Where(x=> x.)

            //foreach (var type in types)
            //{
            //    IInitializeStrategy strategy = Activator.CreateInstance(type) as IInitializeStrategy;

            //    if (strategy.UsageStatus == Usage.Yes)
            //        initializers.Add(strategy);
            //}

            UserInitializer init5 = new UserInitializer();
            CitizenInitialize init1 = new CitizenInitialize();
            InspectorsInitialize init3 = new InspectorsInitialize();
            InspectorsAccountInitialize init2 = new InspectorsAccountInitialize();
            RoleInitializer init4 = new RoleInitializer();
            initializers.AddRange(new List<IInitializeStrategy> { init4, init5, init1, init3, init2 });

            return initializers;
        }
    }
}