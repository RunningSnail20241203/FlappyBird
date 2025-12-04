using Infra.Command;

namespace GameModules.Commands
{
    public class OpenLevelsCommand : ICommand
    {
        public string Name => nameof(OpenLevelsCommand);
    }
}