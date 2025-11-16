using UnityEngine;

/// <summary>
/// 小鸟死亡状态
/// </summary>
public class BirdDeadState : BirdState
{
    public BirdDeadState(BirdController context) : base(context)
    {
    }

    public override string Name => "Dead";

    public override void OnEnter()
    {
        Debug.Log("小鸟进入死亡状态");
        Context.PlayDeathEffect();
        Context.EnablePhysics(false);
    }

    public override void OnUpdate(float deltaTime)
    {
        // 死亡状态逻辑
    }

    public override void OnExit()
    {
        Debug.Log("小鸟退出死亡状态");
    }
}