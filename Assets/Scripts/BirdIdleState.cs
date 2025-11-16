using UnityEngine;

/// <summary>
/// 小鸟空闲状态
/// </summary>
public class BirdIdleState : BirdState
{
    public BirdIdleState(BirdController context) : base(context)
    {
    }

    public override string Name => "Idle";

    public override void OnEnter()
    {
        Debug.Log("小鸟进入空闲状态");
        Context.EnablePhysics(false);
    }

    public override void OnUpdate(float deltaTime)
    {
        // 空闲状态逻辑
    }

    public override void OnExit()
    {
        Debug.Log("小鸟退出空闲状态");
    }
}