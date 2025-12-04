// 事件的基类

namespace Infra.Event
{
    public class EventBase : IEvent
    {
        public string Name => GetType().Name;
        public object Sender { get; set; }
        public IEventArg EventArgs { get; set; }
    }
}