using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 暂停状态
/// </summary>
public class PausedState : GameStateBase
{
    protected override Dictionary<string, Action<ICommandArg>> CommandHandlers => new()
    {
        {nameof(PauseGameCommand), PauseGameCommandHandler},
    };
    
    public override void OnEnter()
    {
        Time.timeScale = 0f;
        UIManager.Instance.ShowPausePanel();
    }

    public override void OnExit()
    {
        Time.timeScale = 1f;
        
        UIManager.Instance.HidePausePanel();
    }
    
    private void PauseGameCommandHandler(ICommandArg obj)
    {
        GameStateManager.Instance.ResumeGame();
    }
}