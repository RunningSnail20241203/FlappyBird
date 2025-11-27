using System;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 联机服务
/// </summary>
public class OnlineService : MonoSingleton<OnlineService>
{
    public string LocalPlayerId { get; private set; }
    public bool IsInMatch { get; private set; }
    
    public event Action<string> OnPlayerEliminated;
    public event Action<MatchResult> OnMatchEnded;

    /// <summary>
    /// 开始匹配
    /// </summary>
    public async Task<MatchmakingResult> StartMatchmaking()
    {
        try
        {
            // 使用Photon或自定义匹配服务
            var result = await MatchmakingManager.Instance.FindMatch();
            if (result.Success)
            {
                IsInMatch = true;
                await JoinMatch(result.MatchId);
            }
            return result;
        }
        catch (Exception e)
        {
            Debug.LogError($"匹配失败: {e.Message}");
            return MatchmakingResult.Failed();
        }
    }

    /// <summary>
    /// 获取比赛随机种子
    /// </summary>
    public string GetMatchSeed(string matchId)
    {
        // 从服务器获取同步的随机种子
        return matchId; // 简化实现
    }

    private async Task JoinMatch(string matchId)
    {
        // 加入比赛房间逻辑
        await NetworkManager.Instance.JoinRoom(matchId);
    }
}