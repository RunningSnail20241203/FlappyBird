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
        GameManager.UIManager.ShowGameOverPanel();
        GameManager.AudioManager.PlaySound("GameOver");
        
        // 上传分数到排行榜
        GameManager.OnlineManager.UploadScore(GameManager.ScoreManager.CurrentScore);
        
        // 显示最终分数
        GameManager.UIManager.UpdateFinalScore(GameManager.ScoreManager.CurrentScore);
    }

    public override void OnUpdate(float deltaTime)
    {
        // 游戏结束状态逻辑
    }

    public override void OnExit()
    {
        Debug.Log("退出游戏结束状态");
        GameManager.UIManager.HideGameOverPanel();
    }
}