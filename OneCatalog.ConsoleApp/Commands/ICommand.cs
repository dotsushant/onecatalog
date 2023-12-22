using System.Collections.Generic;

namespace OneCatalog.ConsoleApp.Commands
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        IEnumerable<CommandOption> Options { get; }
        void Execute();
    }
}