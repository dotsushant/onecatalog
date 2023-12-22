using System.Collections.Generic;

namespace OneCatalog.ConsoleApp.Commands
{
    public abstract class Command : ICommand
    {
        public Command(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }

        public string Description { get; }

        public abstract IEnumerable<CommandOption> Options { get; }

        public abstract void Execute();
    }
}