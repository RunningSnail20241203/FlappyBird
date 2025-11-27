using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : MonoSingleton<EventManager>, IEventManager
{
    private readonly Dictionary<string, List<Delegate>> _events = new();

    public void Subscribe<T>(Action<T> handler) where T : IEvent
    {
        var evtName = typeof(T).Name;

        if (_events.TryGetValue(evtName, out var list))
        {
            list.Add(handler);
        }
        else
        {
            _events.Add(evtName, new List<Delegate> { handler });
        }

        Debug.Log($"EventManager: Subscribed to event {evtName}");
    }

    public void Unsubscribe<T>(Action<T> handler) where T : IEvent
    {
        var evtName = typeof(T).Name;

        if (!_events.TryGetValue(evtName, out var list)) return;

        list.Remove(handler);

        // 清理空列表
        if (list.Count == 0)
        {
            _events.Remove(evtName);
        }

        Debug.Log($"EventManager: Unsubscribed from event {evtName}");
    }

    public void Publish<T>(T evt) where T : IEvent
    {
        var evtName = evt.Name;

        if (!_events.TryGetValue(evtName, out var handlers)) return;
        // 创建副本避免在枚举时修改集合
        handlers = handlers.ToList();

        // 使用类型安全的调用
        foreach (var handler in handlers)
        {
            if (handler is Action<T> typedHandler)
            {
                typedHandler(evt);
            }
            // 可选：记录类型不匹配的警告
            else
            {
                Debug.LogWarning($"EventManager: Handler type mismatch for event {evtName}");
            }
        }
    }
}