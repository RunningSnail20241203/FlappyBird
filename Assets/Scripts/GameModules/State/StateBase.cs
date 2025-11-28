using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 抽象基础状态
/// </summary>
public abstract class StateBase : IState
{
    public virtual string Name => GetType().Name;


    public virtual void OnEnter()
    {
    }

    public virtual void OnUpdate(float deltaTime)
    {
    }

    public virtual void OnFixedUpdate(float fixedDeltaTime)
    {
    }

    public virtual void OnExit()
    {
    }
}

/// <summary>
/// 带上下文的状态基类
/// </summary>
/// <typeparam name="T">上下文类型</typeparam>
public abstract class StateBase<T> : StateBase
{
    protected T Context { get; private set; }

    public virtual void SetContext(T context)
    {
        Context = context;
    }

    protected StateBase(T context)
    {
        Context = context;
    }
}