using UnityEngine;

/// <summary>
/// 暂停状态
/// </summary>
public class PausedState : GameState
{
    public override string Name => "Paused";

    public override void OnEnter()
    {
        Debug.Log("进入暂停状态");
        Time.timeScale = 0f;
        UIManager.Instance.ShowPausePanel();
    }

    public override void OnUpdate(float deltaTime)
    {
        // 暂停状态逻辑
    }

    public override void OnExit()
    {
        Debug.Log("退出暂停状态");
        Time.timeScale = 1f;
        UIManager.Instance.HidePausePanel();
    }
}