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
    };


    public override void OnEnter()
    {
        Debug.Log("进入菜单状态");
        base.OnEnter();
        UIManager.Instance.ShowMenuPanel();
        AudioManager.Instance.PlayBackgroundMusic("MenuMusic");
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("退出菜单状态");
        UIManager.Instance.HideMenuPanel();
    }

    private void StartGameCommandHandler(BaseCommandArgs args)
    {
        GameStateManager.Instance.StartPlay();
    }


    private void OpenSettingCommandHandler(BaseCommandArgs obj)
    {
        GameStateManager.Instance.OpenSettings();
    }
}