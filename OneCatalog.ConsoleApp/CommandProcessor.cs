using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using OneCatalog.Application;
using OneCatalog.ConsoleApp.Commands;

namespace OneCatalog.ConsoleApp
{
    public class CommandProcessor
    {
        private readonly IConfiguration _configuration;
        private readonly List<ICommand> _commands = new List<ICommand>();

        private readonly List<CommandOption> _options = new List<CommandOption>()
        {
            new CommandOption(Constants.Configuration.DatasetLocation, "Dataset location")
        };

        public CommandProcessor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Add(ICommand command)
        {
            _commands.Add(command);
        }

        public void Process()
        {
            var command = _commands.Find(c => CanExecuteCommand(c));

            if (!CommandOptionsAreValid(_options) || command is null)
                ShowHelp();
            else
                command.Execute();
        }

        private void ShowHelp()
        {
            var helpStringbuilder = new StringBuilder();
            helpStringbuilder.AppendLine("Welcome to OneCatalog!");
            helpStringbuilder.AppendLine(
                $"OneCatalog is a mega merge and supply chain management tool. Currently it supports the following commands:{Environment.NewLine}");

            _commands.ForEach(command =>
            {
                helpStringbuilder.AppendLine($"Command     : {command.Name}");
                helpStringbuilder.AppendLine($"Description : {command.Description}");
                helpStringbuilder.AppendLine(
                    $"Usage       : dotnet run {string.Join(" ", _options)} --command {command.Name} {string.Join(" ", command.Options)}{Environment.NewLine}");
            });

            Console.WriteLine(helpStringbuilder.ToString());
        }

        private bool CanExecuteCommand(ICommand command)
        {
            return string.Equals(_configuration["command"], command.Name, StringComparison.OrdinalIgnoreCase) &&
                   CommandOptionsAreValid(command.Options);
        }

        private bool CommandOptionsAreValid(IEnumerable<CommandOption> options)
        {
            return options.All(option => !string.IsNullOrWhiteSpace(_configuration[option.Name]));
        }
    }
}