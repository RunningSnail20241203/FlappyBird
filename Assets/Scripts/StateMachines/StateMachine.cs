using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 泛型状态机
/// </summary>
/// <typeparam name="T">状态类型</typeparam>
public class StateMachine<T> : IStateMachine<T> where T : class, IState
{
    public T CurrentState { get; private set; }
    public T PreviousState { get; private set; }

    private readonly Dictionary<Type, T> _stateInstances = new();
    private readonly List<StateTransition> _transitions = new();

    public event Action<T, T> OnStateChanged; // 参数: 旧状态, 新状态

    public StateMachine()
    {
    }

    public StateMachine(T initialState)
    {
        ChangeState(initialState);
    }

    /// <summary>
    /// 添加状态
    /// </summary>
    public void AddState(T state)
    {
        var type = state.GetType();
        _stateInstances.TryAdd(type, state);
    }

    /// <summary>
    /// 添加状态过渡
    /// </summary>
    private void AddTransition(T from, T to, Func<bool> condition)
    {
        _transitions.Add(new StateTransition(from, to, condition));
    }

    /// <summary>
    /// 添加状态过渡（泛型版本）
    /// </summary>
    public void AddTransition<TFrom, TTo>(Func<bool> condition)
        where TFrom : class, T
        where TTo : class, T
    {
        var from = GetState<TFrom>();
        var to = GetState<TTo>();

        if (from != null && to != null)
        {
            AddTransition(from, to, condition);
        }
        else
        {
            Debug.LogWarning($"无法添加状态过渡: 状态未找到");
        }
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    public void ChangeState(T newState)
    {
        if (newState == null || newState == CurrentState)
            return;

        PreviousState = CurrentState;

        // 退出当前状态
        CurrentState?.OnExit();

        // 进入新状态
        var oldState = CurrentState;
        CurrentState = newState;
        CurrentState.OnEnter();

        OnStateChanged?.Invoke(oldState, CurrentState);

        Debug.Log($"[StateMachine] 状态切换: {oldState?.Name ?? "None"} -> {CurrentState.Name}");
    }

    /// <summary>
    /// 切换状态（泛型版本）
    /// </summary>
    public void ChangeState<TState>() where TState : class, T
    {
        var state = GetState<TState>();
        if (state != null)
        {
            ChangeState(state);
        }
        else
        {
            Debug.LogError($"状态未找到: {typeof(TState).Name}");
        }
    }

    /// <summary>
    /// 检查是否在指定状态
    /// </summary>
    public bool IsInState<TState>() where TState : class, T
    {
        return CurrentState is TState;
    }

    /// <summary>
    /// 获取状态实例
    /// </summary>
    private TState GetState<TState>() where TState : class, T
    {
        var type = typeof(TState);
        if (_stateInstances.TryGetValue(type, out var state))
        {
            return state as TState;
        }

        return null;
    }

    /// <summary>
    /// 更新状态机
    /// </summary>
    public void OnUpdate(float deltaTime)
    {
        // 更新当前状态
        CurrentState?.OnUpdate(deltaTime);
    }

    /// <summary>
    /// 固定更新状态机
    /// </summary>
    public void OnFixedUpdate(float fixedDeltaTime)
    {
        // 检查状态过渡
        CheckTransitions();
        
        // 更新当前状态
        CurrentState?.OnFixedUpdate(fixedDeltaTime);
    }

    /// <summary>
    /// 检查状态过渡条件
    /// </summary>
    private void CheckTransitions()
    {
        foreach (var transition in _transitions.Where(transition =>
                     transition.From == CurrentState && transition.Condition()))
        {
            ChangeState(transition.To as T);
            break;
        }
    }

    /// <summary>
    /// 清空状态机
    /// </summary>
    public void Clear()
    {
        CurrentState?.OnExit();
        CurrentState = null;
        PreviousState = null;
        _stateInstances.Clear();
        _transitions.Clear();
    }
}