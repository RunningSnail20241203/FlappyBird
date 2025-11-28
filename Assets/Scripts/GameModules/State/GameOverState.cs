using System;
using System.Collections.Generic;

/// <summary>
/// 游戏结束状态
/// </summary>
public class GameOverState : GameStateBase
{
    protected override Dictionary<string, Action<ICommand>> CommandHandlers => new()
    {
        { nameof(StartGameCommand), StartGameCommandHandler },
        { nameof(OpenMainMenuCommand), ReturnMainMenuCommandHandler },
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
        // // 清空屏幕上的管道
        // PipeSpawner.Instance.ReturnAllPipes();
        // 关闭结算界面
        UIManager.Instance.HideGameOverPanel();
        // 清空积分
        ScoreManager.Instance.ClearAllScores();
    }

    private void ReturnMainMenuCommandHandler(ICommand obj)
    {
        GameStateManager.Instance.GoToMenu();
    }

    private void StartGameCommandHandler(ICommand obj)
    {
        if (obj is StartGameCommand command)
        {
            GameStateManager.Instance.StartPlay(command.GameMode, command.Args);
        }
    }
}