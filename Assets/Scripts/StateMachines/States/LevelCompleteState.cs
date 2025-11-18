using UnityEngine;

/// <summary>
/// 关卡完成状态
/// </summary>
public class LevelCompleteState : GameState
{
    public override string Name => "LevelComplete";

    public override void OnEnter()
    {
        Debug.Log("进入关卡完成状态");
        UIManager.Instance.ShowLevelCompletePanel();
        AudioManager.Instance.PlaySound("LevelComplete");
        
        // 发放奖励
        CurrencyManager.Instance.AddCoins(CalculateReward());
    }

    public override void OnUpdate(float deltaTime)
    {
        // 关卡完成状态逻辑
    }

    public override void OnExit()
    {
        Debug.Log("退出关卡完成状态");
        UIManager.Instance.HideLevelCompletePanel();
    }

    private int CalculateReward()
    {
        // 根据表现计算奖励
        return ScoreManager.Instance.CurrentScore * 10;
    }
}