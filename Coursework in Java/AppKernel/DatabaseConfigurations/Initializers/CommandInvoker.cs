using Coursework_in_Java.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.AppKernel.DatabaseConfigurations.Initializers
{
    public class CommandInvoker
    {
        private List<BaseCommand> commands;

        public CommandInvoker()
        {
            this.commands = new List<BaseCommand>();
        }

        public void StoreCommand(BaseCommand command)
        {
            this.commands.Add(command);
        }

        public void ExecuteCommands(ApplicationDbContext db)
        {
            foreach (var command in this.commands)
            {
                command.Execute(db);
            }
        }
    }
}