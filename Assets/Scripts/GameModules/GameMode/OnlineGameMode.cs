using Infra.GameMode;

namespace GameModules.GameMode
{
    public class OnlineGameMode : GameModeBase
    {
        public override GameModeType ModeType => GameModeType.Online;
    }
}