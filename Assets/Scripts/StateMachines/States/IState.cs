/// <summary>
/// 状态接口
/// </summary>
public interface IState
{
    string Name { get; }
    void OnEnter();
    void OnUpdate(float deltaTime);
    void OnFixedUpdate(float fixedDeltaTime);
    void OnExit();
}