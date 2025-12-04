using Infra.Command;

namespace GameModules.Commands
{
    public class OpenLeaderBoardCommand : ICommand
    {
        public string Name => nameof(OpenLeaderBoardCommand);
    }
}