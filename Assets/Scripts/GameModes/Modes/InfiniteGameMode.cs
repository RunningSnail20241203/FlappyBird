public class InfiniteGameMode : GameModeBase
{
    public override GameModeType ModeType => GameModeType.Infinite;
    
    private RandomPipeStrategy _pipeStrategy;
    // private int _currentScore = 0;

    public override void Initialize()
    {
        // 使用随机管道生成策略
        _pipeStrategy = new RandomPipeStrategy();
        _pipeStrategy.Initialize();
        
        // 注册事件
        EventManager.Instance.Subscribe<BirdCollisionEvent>(OnBirdCollision);
    }

    public override void Start()
    {
        base.Start();
        _pipeStrategy.StartGenerating();
        // _currentScore = 0;
    }

    public override void Cleanup()
    {
        _pipeStrategy.StopGenerating();
        EventManager.Instance.Unsubscribe<BirdCollisionEvent>(OnBirdCollision);
    }

    private void OnBirdCollision(BirdCollisionEvent e)
    {
        // 游戏结束逻辑
        GameStateManager.Instance.AddCommand(new GameOverCommand());
    }
}