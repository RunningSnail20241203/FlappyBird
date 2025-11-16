using UnityEngine;

/// <summary>
/// 小鸟飞行状态
/// </summary>
public class BirdFlyingState : BirdState
{
    public BirdFlyingState(BirdController context) : base(context)
    {
    }

    public override string Name => "Flying";

    public override void OnEnter()
    {
        Debug.Log("小鸟进入飞行状态");
        Context.EnablePhysics(true);
    }

    public override void OnUpdate(float deltaTime)
    {
        // 飞行状态逻辑
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Context.Jump();
        }
    }

    public override void OnExit()
    {
        Debug.Log("小鸟退出飞行状态");
    }
}