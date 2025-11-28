using System;
using System.Collections.Generic;
using GameModules.Commands;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// 游戏进行状态
/// </summary>
public class PlayingState : GameStateBase
{
    protected override Dictionary<string, Action<ICommand>> CommandHandlers => new()
    {
        { nameof(GameOverCommand), GameOverCommandHandler },
        { nameof(PauseGameCommand), PauseGameCommandHandler },
        { nameof(ResumeGameCommand), ResumeGameCommandHandler },
        { nameof(AddScoreCommand), AddScoreCommandHandler },
    };

    private IGameMode _currentGameMode;

    public override void OnEnter()
    {
        base.OnEnter();

        _currentGameMode = GameModeManager.Instance.CurrentMode;
        Assert.IsNotNull(_currentGameMode, "当前游戏模式不能为空");

        _currentGameMode.Start();
    }

    public override void OnExit()
    {
        base.OnExit();
        _currentGameMode.Cleanup();
    }

    private void GameOverCommandHandler(ICommand args)
    {
        GameStateManager.Instance.GameOver();
    }

    private void PauseGameCommandHandler(ICommand obj)
    {
        _currentGameMode.Pause();
        UIManager.Instance.ShowPausePanel();
    }

    private void ResumeGameCommandHandler(ICommand obj)
    {
        _currentGameMode.Resume();
        UIManager.Instance.HidePausePanel();
    }

    private void AddScoreCommandHandler(ICommand obj)
    {
        if (obj is AddScoreCommand args)
        {
            ScoreManager.Instance.AddScore(args.RoleId, args.Score);
        }
        else
        {
            Debug.LogError($"AddScoreCommandHandler: args is not AddScoreArgs :{obj}");
        }
    }
}