using System;

public interface IEventManager
{
    void Subscribe<T>(Action<T> handler) where T : IEvent;
    void Unsubscribe<T>( Action<T> handler) where T : IEvent;
    void Publish<T>(T evt) where T : IEvent;
}