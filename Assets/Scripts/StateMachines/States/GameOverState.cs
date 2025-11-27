using System;
using System.Collections.Generic;

/// <summary>
/// 游戏结束状态
/// </summary>
public class GameOverState : GameStateBase
{

    protected override Dictionary<string, Action<ICommandArg>> CommandHandlers => new()
    {
        { nameof(ReStartGameCommand), ReStartGameCommandHandler },
        { nameof(ReturnMainMenuCommand), ReturnMainMenuCommandHandler },
    };

    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.ShowGameOverPanel();
        // AudioManager.Instance.PlaySound("GameOver");
    }

    public override void OnExit()
    {
        base.OnExit();
        // 重置小鸟的位置和重力因子
        BirdManager.Instance.Birds.ForEach(x => x.ResetBird());
        // 清空屏幕上的管道
        PipeSpawner.Instance.ReturnAllPipes();
        // 关闭结算界面
        UIManager.Instance.HideGameOverPanel();
        // 清空积分
        ScoreManager.Instance.ClearAllScores();
    }

    private void ReturnMainMenuCommandHandler(ICommandArg obj)
    {
        GameStateManager.Instance.GoToMenu();
    }

    private void ReStartGameCommandHandler(ICommandArg obj)
    {
        GameStateManager.Instance.StartPlay();
    }
}