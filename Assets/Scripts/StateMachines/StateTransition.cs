using System;

/// <summary>
/// 状态过渡条件
/// </summary>
public struct StateTransition
{
    public IState From { get; }
    public IState To { get; }
    public Func<bool> Condition { get; }

    public StateTransition(IState from, IState to, Func<bool> condition)
    {
        From = from;
        To = to;
        Condition = condition;
    }
}