/// <summary>
/// 模式策略基类
/// </summary>
public abstract class GameModeBase : IGameMode
{
    public abstract GameModeType ModeType { get; }

    // protected GameModeContext _context;
    protected bool _isActive = false;

    public virtual void Initialize()
    {
    }

    public virtual void Start()
    {
        _isActive = true;
    }

    public virtual void Pause()
    {
        _isActive = false;
    }

    public virtual void Resume()
    {
        _isActive = true;
    }

    public virtual void Cleanup()
    {
        _isActive = false;
    }

    public virtual void OnUpdate(float deltaTime)
    {
    }

    public virtual void SetGameModeData(IGameModeArg arg)
    {
    }
}