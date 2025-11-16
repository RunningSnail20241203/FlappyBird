using UnityEngine;

/// <summary>
/// 抽象基础状态
/// </summary>
public abstract class BaseState : IState
{
    public abstract string Name { get; }
    
    public virtual void OnEnter() { }
    public virtual void OnUpdate(float deltaTime) { }
    public virtual void OnFixedUpdate(float fixedDeltaTime) { }
    public virtual void OnExit() { }
}

/// <summary>
/// 带上下文的状态基类
/// </summary>
/// <typeparam name="T">上下文类型</typeparam>
public abstract class BaseState<T> : BaseState
{
    protected T Context { get; private set; }
    
    public virtual void SetContext(T context)
    {
        Context = context;
    }
    
    public BaseState(T context)
    {
        Context = context;
    }
}