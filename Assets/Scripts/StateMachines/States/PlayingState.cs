using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏进行状态
/// </summary>
public class PlayingState : GameStateBase
{
    protected override Dictionary<string, Action<ICommandArg>> CommandHandlers => new()
    {
        { nameof(GameOverCommand), GameOverCommandHandler },
        { nameof(PauseGameCommand), PauseGameCommandHandler },
        { nameof(AddScoreCommand), AddScoreCommandHandler },
    };
    
    private GameModeType _currentModeType;

    public override void OnEnter()
    {
        base.OnEnter();
        // 获取要启动的游戏模式（从菜单状态传递过来）
        // _currentModeType = GameModeManager.Instance.PendingGameModeType;
        
        // 启动对应的游戏模式
        // GameModeManager.Instance.SwitchMode(_currentModeType, GameManager.Instance.ModeParameter);

        
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
        UIManager.Instance.HideGamePanel();
        PipeSpawner.Instance.PauseSpawning();
        BirdManager.Instance.Birds.ForEach(x => x.PauseBird());
    }

    private void GameOverCommandHandler(ICommandArg args)
    {
        GameStateManager.Instance.GameOver();
    }

    private void PauseGameCommandHandler(ICommandArg obj)
    {
        GameStateManager.Instance.PauseGame();
    }

    private void AddScoreCommandHandler(ICommandArg obj)
    {
        if (obj is AddScoreArgs args)
        {
            ScoreManager.Instance.AddScore(args.RoleId, args.Score);
        }
        else
        {
            Debug.LogError($"AddScoreCommandHandler: args is not AddScoreArgs :{obj}");
        }
    }
}