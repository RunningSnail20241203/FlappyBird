/// <summary>
/// 小鸟状态基类
/// </summary>
public abstract class BirdState : BaseState<BirdController>
{
    protected BirdState(BirdController context) : base(context)
    {
    }
}