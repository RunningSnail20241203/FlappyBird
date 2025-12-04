using Infra.Command;

namespace GameModules.Commands
{
    public class GameOverCommand : ICommand
    {
        public string Name => nameof(GameOverCommand);

        public string WinnerName;
    }
}