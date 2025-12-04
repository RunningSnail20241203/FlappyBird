using Infra.Command;

namespace GameModules.Commands
{
    public class OpenSettingCommand : ICommand
    {
        public string Name => nameof(OpenSettingCommand);
    }
}