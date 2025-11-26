
using System;
using System.Collections.Generic;

public class ThanksState : GameStateBase
{
    protected override Dictionary<string, Action<BaseCommandArgs>> CommandHandlers => new()
    {
        { nameof(ReturnMainMenuCommand), ReturnMainMenuCommandHandler },
    };

    public override void OnEnter()
    {
        base.OnEnter();
        
        UIManager.Instance.ShowThanksPanel();
    }

    public override void OnExit()
    {
        base.OnExit();
        
        UIManager.Instance.HideThanksPanel();
    }

    private void ReturnMainMenuCommandHandler(BaseCommandArgs args)
    {
        GameStateManager.Instance.GoToMenu();
    }
}