using UnityEngine;

/// <summary>
/// 游戏状态管理器
/// </summary>
public class GameStateManager : MonoSingleton<GameStateManager>
{
    public StateMachine<GameStateBase> StateMachine { get; private set; }

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
    public void StartPlay()
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

    public void GotoSettings()
    {
        StateMachine.ChangeState<SettingState>();
    }

    public void GotoThanks()
    {
        StateMachine.ChangeState<ThanksState>();
    }

    public void AddCommand(CommandBase commandBase)
    {
        StateMachine.CurrentState.AddCommand(commandBase);
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();
        InitializeStateMachine();
        Debug.Log("GameStateManager 初始化完成");
    }

    private void InitializeStateMachine()
    {
        StateMachine = new StateMachine<GameStateBase>();

        // 创建状态实例
        var menuState = new MenuState();
        var playingState = new PlayingState();
        var pausedState = new PausedState();
        var gameOverState = new GameOverState();
        var settingState = new SettingState();
        var thanksState = new ThanksState();

        // 添加状态
        StateMachine.AddState(menuState);
        StateMachine.AddState(playingState);
        StateMachine.AddState(pausedState);
        StateMachine.AddState(gameOverState);
        StateMachine.AddState(settingState);
        StateMachine.AddState(thanksState);


        // 添加状态过渡
        StateMachine.AddTransition<PlayingState, PausedState>(() => Input.GetKeyDown(KeyCode.Escape));
        StateMachine.AddTransition<PausedState, PlayingState>(() => Input.GetKeyDown(KeyCode.Escape));

        // 监听状态变化
        StateMachine.OnStateChanged += OnStateChanged;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        StateMachine?.OnUpdate(Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AddCommand(new QuitGameCommand());
        }
    }

    private void FixedUpdate()
    {
        StateMachine?.OnFixedUpdate(Time.fixedDeltaTime);
    }

    private void OnStateChanged(GameStateBase oldState, GameStateBase newState)
    {
        // 状态变化时的额外处理
    }


    protected override void OnDestroy()
    {
        if (!_isApplicationQuitting) StateMachine?.Clear();
        base.OnDestroy();
    }
}