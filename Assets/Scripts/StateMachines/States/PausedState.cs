using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 暂停状态
/// </summary>
public class PausedState : GameStateBase
{
    protected override Dictionary<string, Action<BaseCommandArgs>> CommandHandlers => new()
    {
        {nameof(PauseGameCommand), PauseGameCommandHandler},
    };
    
    public override void OnEnter()
    {
        Debug.Log("进入暂停状态");
        Time.timeScale = 0f;
        UIManager.Instance.ShowPausePanel();
    }

    public override void OnExit()
    {
        Debug.Log("退出暂停状态");
        Time.timeScale = 1f;
        
        UIManager.Instance.HidePausePanel();
    }
    
    private void PauseGameCommandHandler(BaseCommandArgs obj)
    {
        GameStateManager.Instance.ResumeGame();
    }
}