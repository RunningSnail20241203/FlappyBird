using System;
using System.Collections.Generic;

public class SettingState : GameStateBase
{
    protected override Dictionary<string, Action<ICommand>> CommandHandlers => new()
    {
        { nameof(OpenMainMenuCommand), ReturnMainMenuCommandHandler },
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

    private void ReturnMainMenuCommandHandler(ICommand obj)
    {
        GameStateManager.Instance.GoToMenu();
    }
}