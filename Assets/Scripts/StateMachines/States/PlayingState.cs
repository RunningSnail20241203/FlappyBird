using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏进行状态
/// </summary>
public class PlayingState : GameStateBase
{
    public override string Name => "Playing";

    protected override Dictionary<string, Action<BaseCommandArgs>> CommandHandlers => new()
    {
        { nameof(GameOverCommand), GameOverCommandHandler },
    };

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("进入游戏状态");
        UIManager.Instance.ShowGamePanel();
        AudioManager.Instance.PlayBackgroundMusic("GameMusic");

        // 开始生成管道
        PipeSpawner.Instance.StartSpawning();
        
        BirdManager.Instance.Birds.ForEach(x => x.StartBird());
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        // 游戏逻辑更新
        ScoreManager.Instance.OnUpdate(deltaTime);
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("退出游戏状态");
        UIManager.Instance.HideGamePanel();
        PipeSpawner.Instance.StopSpawning();
        PipeSpawner.Instance.StopMovingPipe();
        BirdManager.Instance.Birds.ForEach(x => x.PauseBird());
    }

    private void GameOverCommandHandler(BaseCommandArgs args)
    {
        GameStateManager.Instance.GameOver();
    }
}