/// <summary>
/// 游戏状态基类
/// </summary>
public abstract class GameState : BaseState
{
    public abstract override string Name { get; }
    
    protected static GameManager GameManager => GameManager.Instance;
}