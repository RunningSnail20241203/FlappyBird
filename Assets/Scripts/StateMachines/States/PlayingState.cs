using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏进行状态
/// </summary>
public class PlayingState : GameStateBase
{
    protected override Dictionary<string, Action<BaseCommandArgs>> CommandHandlers => new()
    {
        { nameof(GameOverCommand), GameOverCommandHandler },
        { nameof(PauseGameCommand), PauseGameCommandHandler },
        { nameof(AddScoreCommand), AddScoreCommandHandler },
    };

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("进入游戏状态");
        UIManager.Instance.ShowGamePanel();
        AudioManager.Instance.PlayBackgroundMusic("GameMusic");
        BirdManager.Instance.Birds.ForEach(x => x.StartBird());
        if (GameStateManager.Instance.StateMachine.PreviousState is PausedState)
        {
            PipeSpawner.Instance.ResumeSpawning();
        }
        else
        {
            PipeSpawner.Instance.StartSpawning();
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("退出游戏状态");
        UIManager.Instance.HideGamePanel();
        PipeSpawner.Instance.PauseSpawning();
        BirdManager.Instance.Birds.ForEach(x => x.PauseBird());
    }

    private void GameOverCommandHandler(BaseCommandArgs args)
    {
        GameStateManager.Instance.GameOver();
    }

    private void PauseGameCommandHandler(BaseCommandArgs obj)
    {
        GameStateManager.Instance.PauseGame();
    }

    private void AddScoreCommandHandler(BaseCommandArgs obj)
    {
        if (obj is AddScoreArgs args)
        {
            ScoreManager.Instance.AddScore(args.Target, args.Score);
        }
        else
        {
            Debug.LogError($"AddScoreCommandHandler: args is not AddScoreArgs :{obj}");
        }
    }
}