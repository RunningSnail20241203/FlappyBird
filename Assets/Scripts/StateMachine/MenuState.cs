using UnityEngine;

/// <summary>
/// 菜单状态
/// </summary>
public class MenuState : GameState
{
    public override string Name => "Menu";

    public override void OnEnter()
    {
        Debug.Log("进入菜单状态");
        GameManager.UIManager.ShowMenuPanel();
        GameManager.AudioManager.PlayBackgroundMusic("MenuMusic");
        
        // 重置游戏数据
        GameManager.ScoreManager.ResetScore();
    }

    public override void OnUpdate(float deltaTime)
    {
        // 菜单状态更新逻辑
    }

    public override void OnExit()
    {
        Debug.Log("退出菜单状态");
        GameManager.UIManager.HideMenuPanel();
    }
}