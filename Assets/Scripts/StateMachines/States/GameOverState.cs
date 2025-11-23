using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏结束状态
/// </summary>
public class GameOverState : GameStateBase
{
    public override string Name => "GameOver";

    protected override Dictionary<string, Action<BaseCommandArgs>> CommandHandlers => new()
    {
        { nameof(ReStartGameCommand), ReStartGameCommandHandler },
        { nameof(ReturnMainMenuCommand), ReturnMainMenuCommandHandler },
    };

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("进入游戏结束状态");
        UIManager.Instance.ShowGameOverPanel();
        AudioManager.Instance.PlaySound("GameOver");

        // 上传分数到排行榜
        OnlineManager.Instance.UploadScore(ScoreManager.Instance.CurrentScore);
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("退出游戏结束状态");
        // 重置小鸟的位置和重力因子
        BirdManager.Instance.Birds.ForEach(x => x.ResetBird());
        // 清空屏幕上的管道
        PipeSpawner.Instance.ReturnAllPipes();

        UIManager.Instance.HideGameOverPanel();
    }

    private void ReturnMainMenuCommandHandler(BaseCommandArgs obj)
    {
        GameStateManager.Instance.GoToMenu();
    }

    private void ReStartGameCommandHandler(BaseCommandArgs obj)
    {
        GameStateManager.Instance.StartPlay();
    }
}