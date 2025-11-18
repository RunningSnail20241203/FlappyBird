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
        UIManager.Instance.ShowGamePanel();
        AudioManager.Instance.PlayBackgroundMusic("GameMusic");
        
        // 开始生成管道
        PipeSpawner.Instance.StartSpawning();
        
        // BirdController.Instance.StartBird();
    }

    public override void OnUpdate(float deltaTime)
    {
        // 游戏逻辑更新
        ScoreManager.Instance.OnUpdate(deltaTime);
        //
        // // 检查游戏结束条件
        // if (BirdController.Instance.IsDead)
        // {
        //     StateMachine.Instance.ChangeState<GameOverState>();
        // }
    }

    public override void OnExit()
    {
        Debug.Log("退出游戏状态");
        PipeSpawner.Instance.StopSpawning();
        UIManager.Instance.HideGamePanel();
    }
}