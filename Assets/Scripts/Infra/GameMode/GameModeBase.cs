/// <summary>
/// 模式策略基类
/// </summary>
public abstract class GameModeBase : IGameMode
{
    public abstract GameModeType ModeType { get; }

    public virtual void Initialize()
    {
    }

    public virtual void Start()
    {
    }

    public virtual void Pause()
    {
    }

    public virtual void Resume()
    {
    }

    public virtual void Cleanup()
    {
    }

    public virtual void OnUpdate(float deltaTime)
    {
    }

    public virtual void OnFixedUpdate(float fixedDeltaTime)
    {
        
    }

    public virtual void SetGameModeData(IGameModeArg arg)
    {
    }
}