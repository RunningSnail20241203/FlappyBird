using UnityEngine;

public class LevelGameMode : GameModeBase
{
    public override GameModeType ModeType => GameModeType.Level;
    private const string CollisionTag = "Obstacle";
    private const string ScoreTriggerTag = "ScoreTrigger";

    public override void Initialize()
    {
        base.Initialize();
        
        // 注册事件
        EventManager.Instance.Subscribe<BirdCollisionEvent>(OnBirdCollision);
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Cleanup()
    {
        base.Cleanup();
        
        // 注册事件
        EventManager.Instance.Unsubscribe<BirdCollisionEvent>(OnBirdCollision);
    }

    public override void Pause()
    {
        base.Pause();
    }

    public override void Resume()
    {
        base.Resume();
    }

    public override void SetGameModeData(IGameModeArg arg)
    {
        base.SetGameModeData(arg);
        
        if (arg is LevelGameData levelData)
        {
            Debug.Log($"设置关卡ID: {levelData.LevelId}");
            // 可以在这里处理关卡ID
        }
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