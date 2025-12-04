using Infra.Event;

namespace GameModules.Events
{
    public class BirdCollisionEvent : IEvent
    {
        public string Name => GetType().Name;
        public IEventArg EventArgs { get; set; }
    }

    public class BirdCollisionEventArg : IEventArg
    {
        public string ColliderTag { get; set; }
    }
}