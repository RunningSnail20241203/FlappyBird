using UnityEngine;

public class SettingState : GameState
{
    public override string Name => nameof(SettingState);
    
    public override void OnEnter()
    {
        Debug.Log("进入设置状态");
        UIManager.Instance.ShowSettingPanel();
    }

    public override void OnUpdate(float deltaTime)
    {
        // 设置状态逻辑
    }

    public override void OnExit()
    {
        Debug.Log("退出设置状态");
    }
}