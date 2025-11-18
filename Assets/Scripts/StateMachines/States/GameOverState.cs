using UnityEngine;

/// <summary>
/// 游戏结束状态
/// </summary>
public class GameOverState : GameState
{
    public override string Name => "GameOver";

    public override void OnEnter()
    {
        Debug.Log("进入游戏结束状态");
        UIManager.Instance.ShowGameOverPanel();
        AudioManager.Instance.PlaySound("GameOver");
        
        // 上传分数到排行榜
        OnlineManager.Instance.UploadScore(ScoreManager.Instance.CurrentScore);
        
        // 显示最终分数
        UIManager.Instance.UpdateFinalScore(ScoreManager.Instance.CurrentScore);
    }

    public override void OnUpdate(float deltaTime)
    {
        // 游戏结束状态逻辑
    }

    public override void OnExit()
    {
        Debug.Log("退出游戏结束状态");
        UIManager.Instance.HideGameOverPanel();
    }
}