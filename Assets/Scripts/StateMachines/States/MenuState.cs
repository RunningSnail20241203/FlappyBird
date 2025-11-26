using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 菜单状态
/// </summary>
public class MenuState : GameStateBase
{
    protected override Dictionary<string, Action<BaseCommandArgs>> CommandHandlers => new()
    {
        { nameof(StartGameCommand), StartGameCommandHandler },
        { nameof(OpenSettingCommand), OpenSettingCommandHandler },
        { nameof(OpenThanksCommand), OpenThanksCommandHandler }
    };


    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.ShowMenuPanel();
        AudioManager.Instance.PlayBackgroundMusic("MenuMusic");
    }

    public override void OnExit()
    {
        base.OnExit();
        UIManager.Instance.HideMenuPanel();
    }

    private void StartGameCommandHandler(BaseCommandArgs args)
    {
        GameStateManager.Instance.StartPlay();
    }

    private void OpenSettingCommandHandler(BaseCommandArgs obj)
    {
        GameStateManager.Instance.GotoSettings();
    }

    private void OpenThanksCommandHandler(BaseCommandArgs obj)
    {
        GameStateManager.Instance.GotoThanks();
    }
}