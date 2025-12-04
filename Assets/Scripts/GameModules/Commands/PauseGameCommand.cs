using Infra.Command;

namespace GameModules.Commands
{
    public class PauseGameCommand : ICommand
    {
        public string Name => nameof(PauseGameCommand);
    }
}