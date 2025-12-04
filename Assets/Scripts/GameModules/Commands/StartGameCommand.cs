using Infra.Command;
using Infra.GameMode;

namespace GameModules.Commands
{
    public class StartGameCommand : ICommand
    {
        public string Name => nameof(StartGameCommand);
        public GameModeType GameMode { get; set; }
        public IGameModeArg Args { get; set; }
    }
}