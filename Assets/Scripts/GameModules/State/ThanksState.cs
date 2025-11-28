
using System;
using System.Collections.Generic;

public class ThanksState : GameStateBase
{
    protected override Dictionary<string, Action<ICommand>> CommandHandlers => new()
    {
        { nameof(OpenMainMenuCommand), ReturnMainMenuCommandHandler },
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

    private void ReturnMainMenuCommandHandler(ICommand args)
    {
        GameStateManager.Instance.GoToMenu();
    }
}