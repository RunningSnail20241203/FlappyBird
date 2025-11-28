using UnityEngine;

public class InfiniteGameMode : GameModeBase
{
    public override GameModeType ModeType => GameModeType.Infinite;
    
    private RandomGenerationStrategy _generationStrategy;
    private const string CollisionTag = "Obstacle";
    private const string ScoreTriggerTag = "ScoreTrigger";

    public override void Initialize()
    {
        // 使用随机管道生成策略
        _generationStrategy = new RandomGenerationStrategy();
        _generationStrategy.Initialize();
        
        // 注册事件
        EventManager.Instance.Subscribe<BirdCollisionEvent>(OnBirdCollision);
    }

    public override void Start()
    {
        base.Start();
        
        _generationStrategy.StartGenerating();
        
        UIManager.Instance.ShowGamePanel();
        AudioManager.Instance.PlayBackgroundMusic("GameMusic");
        BirdManager.Instance.StartBirds();
    }

    public override void Pause()
    {
        base.Pause();
        BirdManager.Instance.PauseBirds();
        _generationStrategy.PauseGeneration();
    }

    public override void Resume()
    {
        base.Resume();
        BirdManager.Instance.ResumeBirds();
        _generationStrategy.ResumeGeneration();
    }

    public override void Cleanup()
    {
        base.Cleanup();
        UIManager.Instance.HideGamePanel();
        BirdManager.Instance.ResetBirds();
        _generationStrategy.Cleanup();
        EventManager.Instance.Unsubscribe<BirdCollisionEvent>(OnBirdCollision);
    }

    public override void OnFixedUpdate(float fixedDeltaTime)
    {
        base.OnFixedUpdate(fixedDeltaTime);
        _generationStrategy.FixedUpdate(fixedDeltaTime);
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        _generationStrategy.Update(deltaTime);
    }

    private void OnBirdCollision(BirdCollisionEvent e)
    {
        if (e.EventArgs is not BirdCollisionEventArg arg) return;
        Debug.Log($"BirdController: Collision with {arg.ColliderTag}");
        switch (arg.ColliderTag)
        {
            case CollisionTag:
                // 游戏结束逻辑
                GameStateManager.Instance.AddCommand(new GameOverCommand());
                break;
            case ScoreTriggerTag:
                // 游戏结束逻辑
                GameStateManager.Instance.AddCommand(new AddScoreCommand()
                {
                    RoleId = BirdManager.Instance.MyBird.name,
                    Score = 1,
                });
                break;
        }
    }
}