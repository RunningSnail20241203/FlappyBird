using System.Threading.Tasks;

namespace GameModules.Rank
{
    public class LocalLeaderboardProvider : ILeaderboardProvider
    {
        public void SubmitScore(int score)
        {
            throw new System.NotImplementedException();
        }

        public Task<SubmitScoreResult> SubmitScoreAsync(int score)
        {
            throw new System.NotImplementedException();
        }

        public LeaderboardData GetLeaderboard(LeaderboardType type, int count)
        {
            throw new System.NotImplementedException();
        }

        public Task<LeaderboardData> GetLeaderboardAsync(LeaderboardType type, int count)
        {
            throw new System.NotImplementedException();
        }
    }
}