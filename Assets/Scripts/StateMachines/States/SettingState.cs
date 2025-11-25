using System;
using System.Collections.Generic;
using UnityEngine;

public class SettingState : GameStateBase
{
    protected override Dictionary<string, Action<BaseCommandArgs>> CommandHandlers => new()
    {
    };

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("进入设置状态");
        UIManager.Instance.ShowSettingPanel();
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("退出设置状态");
        UIManager.Instance.HideSettingPanel();
    }
}