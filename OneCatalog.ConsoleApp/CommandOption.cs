namespace OneCatalog.ConsoleApp
{
    public class CommandOption
    {
        public CommandOption(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }

        public override string ToString()
        {
            return $"--{Name} <{Description}>";
        }
    }
}