using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 菜单状态
/// </summary>
public class MenuState : GameStateBase
{
    public override string Name => "Menu";

    protected override Dictionary<string, Action<BaseCommandArgs>> CommandHandlers => new()
    {
        {nameof(StartGameCommand), StartGameCommandHandler}
    };

    public override void OnEnter()
    {
        Debug.Log("进入菜单状态");
        base.OnEnter();
        UIManager.Instance.ShowMenuPanel();
        AudioManager.Instance.PlayBackgroundMusic("MenuMusic");
        
        // 重置游戏数据
        ScoreManager.Instance.ResetScore();
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
}