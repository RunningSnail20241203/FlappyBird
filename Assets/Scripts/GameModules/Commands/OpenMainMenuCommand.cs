using Infra.Command;

namespace GameModules.Commands
{
    public class OpenMainMenuCommand : ICommand
    {
        public string Name => nameof(OpenMainMenuCommand);
    }
}