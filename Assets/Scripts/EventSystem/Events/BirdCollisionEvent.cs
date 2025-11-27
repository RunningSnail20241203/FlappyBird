public struct BirdCollisionEvent : IEvent
{
    public string Name => GetType().Name;
    public IEventArg EventArgs { get; set; }
}

public struct BirdCollisionEventArg : IEventArg
{
    public string ColliderTag { get; set; }
}