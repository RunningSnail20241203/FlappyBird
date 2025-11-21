using UnityEngine;

/// <summary>
/// 游戏状态管理器
/// </summary>
public class GameStateManager : MonoSingleton<GameStateManager>
{
    public StateMachine<GameState> StateMachine { get; set; }
    
    protected override void OnAwake()
    {
        InitializeStateMachine();
        Debug.Log("GameStateManager 初始化完成");
    }

    private void InitializeStateMachine()
    {
        StateMachine = new StateMachine<GameState>();
        
        // 创建状态实例
        var menuState = new MenuState();
        var playingState = new PlayingState();
        var pausedState = new PausedState();
        var gameOverState = new GameOverState();
        var settingState = new SettingState();
        
        // 添加状态
        StateMachine.AddState(menuState);
        StateMachine.AddState(playingState);
        StateMachine.AddState(pausedState);
        StateMachine.AddState(gameOverState);
        StateMachine.AddState(settingState);
        
        
        // 添加状态过渡
        StateMachine.AddTransition<PlayingState, PausedState>(() => Input.GetKeyDown(KeyCode.Escape));
        StateMachine.AddTransition<PausedState, PlayingState>(() => Input.GetKeyDown(KeyCode.Escape));
 
        // 监听状态变化
        StateMachine.OnStateChanged += OnStateChanged;
    }

    private void Update()
    {
        StateMachine?.OnUpdate(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        StateMachine?.OnFixedUpdate(Time.fixedDeltaTime);
    }

    private void OnStateChanged(GameState oldState, GameState newState)
    {
        // 状态变化时的额外处理
        Debug.Log($"游戏状态变化: {oldState?.Name} -> {newState.Name}");
    }

    private bool CheckLevelComplete()
    {
        // 检查关卡完成条件
        return false; // 根据实际游戏逻辑实现
    }

    /// <summary>
    /// 切换到菜单状态
    /// </summary>
    public void GoToMenu()
    {
        StateMachine.ChangeState<MenuState>();
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    public void StartGame()
    {
        StateMachine.ChangeState<PlayingState>();
    }

    /// <summary>
    /// 暂停游戏
    /// </summary>
    public void PauseGame()
    {
        StateMachine.ChangeState<PausedState>();
    }

    /// <summary>
    /// 恢复游戏
    /// </summary>
    public void ResumeGame()
    {
        StateMachine.ChangeState<PlayingState>();
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    public void GameOver()
    {
        StateMachine.ChangeState<GameOverState>();
    }

    public void AddCommand(CommandBase command)
    {
        StateMachine.CurrentState.AddCommand(command);
    }

    protected override void OnDestroy()
    {
        StateMachine?.Clear();
        base.OnDestroy();
    }
}