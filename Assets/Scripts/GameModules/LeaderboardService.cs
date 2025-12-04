using System.Threading.Tasks;
using GameModules.Rank;
using Infra;
using UnityEngine;

namespace GameModules
{
    /// <summary>
    /// 排行榜服务
    /// </summary>
    public class LeaderboardService : MonoSingleton<LeaderboardService>
    {
        private ILeaderboardProvider _localProvider;
        private ILeaderboardProvider _onlineProvider;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _localProvider = new LocalLeaderboardProvider();
            _onlineProvider = new OnlineLeaderboardProvider();
        }

        /// <summary>
        /// 提交分数
        /// </summary>
        public async void SubmitScore(int score)
        {
            // 本地存储
            _localProvider.SubmitScore(score);
        
            // 网络提交
            if (NetworkManager.Instance.IsConnected)
            {
                var result = await _onlineProvider.SubmitScoreAsync(score);
                if (result.Success)
                {
                    Debug.Log($"分数提交成功，当前排名: {result.Rank}");
                }
            }
        }

        /// <summary>
        /// 获取排行榜数据
        /// </summary>
        public async Task<LeaderboardData> GetLeaderboardData(LeaderboardType type, int count = 100)
        {
            if (NetworkManager.Instance.IsConnected)
            {
                return await _onlineProvider.GetLeaderboardAsync(type, count);
            }
            else
            {
                return _localProvider.GetLeaderboard(type, count);
            }
        }
    }
}