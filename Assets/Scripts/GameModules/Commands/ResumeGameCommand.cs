using Infra.Command;

namespace GameModules.Commands
{
    public class ResumeGameCommand :  ICommand
    {
        public string Name => nameof(ResumeGameCommand);
    }
}