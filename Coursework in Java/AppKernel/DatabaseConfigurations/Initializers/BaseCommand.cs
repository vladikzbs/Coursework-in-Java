using Coursework_in_Java.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_in_Java.AppKernel.DatabaseConfigurations.Initializers
{
    public abstract class BaseCommand
    {
        public abstract void Execute(ApplicationDbContext context);
    }
}
