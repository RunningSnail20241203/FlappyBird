using Infra.GameMode;

namespace GameModules.GameMode.GameModeData
{
    public struct ChallengeGameData : IGameModeArg
    {
        public int ChallengeId;
        public int TargetScore;
        public int Seed;
    }
}