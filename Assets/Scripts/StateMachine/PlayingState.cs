using UnityEngine;

/// <summary>
/// 游戏进行状态
/// </summary>
public class PlayingState : GameState
{
    public override string Name => "Playing";

    public override void OnEnter()
    {
        Debug.Log("进入游戏状态");
        GameManager.UIManager.ShowGamePanel();
        GameManager.AudioManager.PlayBackgroundMusic("GameMusic");
        
        // 开始生成管道
        GameManager.PipeSpawner.StartSpawning();
        
        // 重置小鸟位置
        GameManager.BirdController.ResetBird();
    }

    public override void OnUpdate(float deltaTime)
    {
        // 游戏逻辑更新
        GameManager.ScoreManager.OnUpdate(deltaTime);
        
        // 检查游戏结束条件
        if (GameManager.BirdController.IsDead)
        {
            GameManager.StateMachine.ChangeState<GameOverState>();
        }
    }

    public override void OnExit()
    {
        Debug.Log("退出游戏状态");
        GameManager.PipeSpawner.StopSpawning();
        GameManager.UIManager.HideGamePanel();
    }
}