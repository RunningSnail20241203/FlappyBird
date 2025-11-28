/// <summary>
/// 状态机接口
/// </summary>
/// <typeparam name="T">状态类型</typeparam>
public interface IStateMachine<T> where T : class, IState
{
    T CurrentState { get; }
    T PreviousState { get; }
    void ChangeState(T newState);
    void ChangeState<TState>() where TState : class, T;
    bool IsInState<TState>() where TState : class, T;
    void OnUpdate(float deltaTime);
    void OnFixedUpdate(float fixedDeltaTime);
}