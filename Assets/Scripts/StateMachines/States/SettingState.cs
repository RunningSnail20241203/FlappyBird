using System;
using System.Collections.Generic;

public class SettingState : GameStateBase
{
    protected override Dictionary<string, Action<ICommandArg>> CommandHandlers => new()
    {
        { nameof(ReturnMainMenuCommand), ReturnMainMenuCommandHandler },
    };

    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.ShowSettingPanel();
    }

    public override void OnExit()
    {
        base.OnExit();
        UIManager.Instance.HideSettingPanel();
    }

    private void ReturnMainMenuCommandHandler(ICommandArg obj)
    {
        GameStateManager.Instance.GoToMenu();
    }
}