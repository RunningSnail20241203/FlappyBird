using Infra.Command;

namespace GameModules.Commands
{
    public class AddScoreCommand : ICommand
    {
        public string Name => nameof(AddScoreCommand);
        public string RoleId { get; set; }
        public int Score { get; set; }
    }
}