using Infra.Command;

namespace GameModules.Commands
{
    public class QuitGameCommand : ICommand
    {
        public string Name => nameof(QuitGameCommand);
    }
}