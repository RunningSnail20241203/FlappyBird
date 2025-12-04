namespace Infra.Event
{
    /// <summary>
    /// 事件接口
    /// </summary>
    public interface IEvent
    {
        string Name { get; }
        IEventArg EventArgs { get; set; }
    }

    public interface IEventArg
    {
    }
}