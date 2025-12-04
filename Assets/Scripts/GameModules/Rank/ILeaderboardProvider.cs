using System.Threading.Tasks;

namespace GameModules.Rank
{
    public interface ILeaderboardProvider
    {
        void SubmitScore(int score);
        Task<SubmitScoreResult> SubmitScoreAsync(int score);
        LeaderboardData GetLeaderboard(LeaderboardType type, int count);
        Task<LeaderboardData> GetLeaderboardAsync(LeaderboardType type, int count);
    }

    public class SubmitScoreResult
    {
        public bool Success { get; set; }
        public int Rank { get; set; }
    }
}