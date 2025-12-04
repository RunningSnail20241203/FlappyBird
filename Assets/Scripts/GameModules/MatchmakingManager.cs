using System.Threading.Tasks;
using Infra;

namespace GameModules
{
    public class MatchmakingManager : MonoSingleton<MatchmakingManager>
    {
        public async Task<MatchmakingResult> FindMatch()
        {
            await Task.Delay(1000);
            return new MatchmakingResult();
        }
    }

    public class MatchmakingResult
    {
        public bool Success{ get; private set;}
        public string MatchId{ get; private set;}

        private static readonly MatchmakingResult FailResult = new MatchmakingResult(){Success = false}; 
        public static MatchmakingResult Failed()
        {
            return FailResult;
        }
    }

    public class MatchResult
    {
    
    }
}