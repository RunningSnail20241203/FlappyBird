using Infra.Command;

namespace GameModules.Commands
{
    public class StartMatchCommand : ICommand
    {
        public string Name => nameof(StartMatchCommand);
    }
}