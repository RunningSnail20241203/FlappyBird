using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoSingleton<GameModeManager>
{
    private readonly Dictionary<GameModeType, IGameMode> _strategies = new();
    public GameModeType CurrentModeType => CurrentMode.ModeType;

    public IGameMode CurrentMode { get; private set; }

    protected override void OnInitialize()
    {
        base.OnInitialize();
        RegisterAllStrategies();
    }

    /// <summary>
    /// 注册所有策略
    /// </summary>
    private void RegisterAllStrategies()
    {
        RegisterStrategy(GameModeType.Infinite, new InfiniteGameMode());
        RegisterStrategy(GameModeType.Level, new LevelGameMode());
        RegisterStrategy(GameModeType.Challenge, new ChallengeGameMode());
        RegisterStrategy(GameModeType.Online, new OnlineGameMode());
    }

    /// <summary>
    /// 切换游戏模式
    /// </summary>
    public void SwitchMode(GameModeType newModeType, IGameModeArg modeData = null)
    {
        if (!_strategies.TryGetValue(newModeType, out var newStrategy))
        {
            Debug.LogError($"未注册的模式: {newModeType}");
            return;
        }
            
        var previousModeType = CurrentModeType;
        
        // 清理当前模式
        CurrentMode?.Cleanup();

        // 设置新模式
        CurrentMode = newStrategy;

        // 传递模式特定数据
        CurrentMode.SetGameModeData(modeData);

        // 开始新模式
        CurrentMode.Start();

        // 发布模式切换事件
        EventManager.Instance.Publish(new GameModeChangedEvent()
        {
            EventArgs = new GameModeChangedEventArgs()
            {
                PreviousMode = previousModeType,
                NewMode = newModeType
            }
        });

        Debug.Log($"游戏模式已切换至: {newModeType}");
    }

    /// <summary>
    /// 暂停当前模式
    /// </summary>
    public void PauseCurrentMode()
    {
        CurrentMode?.Pause();
    }

    /// <summary>
    /// 恢复当前模式
    /// </summary>
    public void ResumeCurrentMode()
    {
        CurrentMode?.Resume();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        CurrentMode?.OnUpdate(Time.deltaTime);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        CurrentMode?.Cleanup();

        foreach (var strategy in _strategies.Values)
        {
            strategy.Cleanup();
        }

        _strategies.Clear();
    }

    /// <summary>
    /// 注册策略
    /// </summary>
    private void RegisterStrategy(GameModeType modeType, IGameMode strategy)
    {
        if (_strategies.ContainsKey(modeType))
        {
            Debug.LogWarning($"模式 {modeType} 已注册，将被覆盖");
        }

        _strategies[modeType] = strategy;
        strategy.Initialize();
    }
}