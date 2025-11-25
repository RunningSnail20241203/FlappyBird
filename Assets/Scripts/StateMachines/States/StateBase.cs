using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


/// <summary>
/// 抽象基础状态
/// </summary>
public abstract class StateBase : IState
{
    public virtual string Name  => GetType().Name;
    private readonly List<CommandBase> _commands = new();
    protected virtual Dictionary<string, Action<BaseCommandArgs>> CommandHandlers { get; } = new();
    
    public virtual void OnEnter() { }

    public virtual void OnUpdate(float deltaTime)
    {
        ProcessCommands();
    }
    public virtual void OnFixedUpdate(float fixedDeltaTime) { }

    public virtual void OnExit()
    {
        ClearCommands();
    }
    
    public void AddCommand(CommandBase commandBase)
    {
        Debug.Log($"当前状态：{Name},添加命令: {commandBase.Name}");
        _commands.Add(commandBase);
    }

    private void ClearCommands()
    {
        _commands.Clear();
    }

    private void ProcessCommands()
    {
        if (_commands.Count <= 0) return;

        var copyCommands = new List<CommandBase>(_commands);
        _commands.Clear();
        foreach (var command in copyCommands)
        {
            if (command is QuitGameCommand)
            {
                QuitGame();
                return;
            }
            
            if (CommandHandlers.TryGetValue(command.Name, out var handlerFunc))
            {
                handlerFunc.Invoke(command.Args);
            }
            else
            {
                Debug.LogError($"没有找到命令处理函数: {command.Name}");
            }
        }
    }

    private void QuitGame()
    {
        var config = new LoadConfirmDialogConfig()
        {
            Title = "退出游戏",
            Message = "确定要退出游戏吗？",
            OnConfirm = () =>
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            },
            OnCancel = () => { }
        };
        ConfirmDialogManager.Instance.ShowConfirmDialog(config);
    }
}

/// <summary>
/// 带上下文的状态基类
/// </summary>
/// <typeparam name="T">上下文类型</typeparam>
public abstract class StateBase<T> : StateBase
{
    protected T Context { get; private set; }
    
    public virtual void SetContext(T context)
    {
        Context = context;
    }

    protected StateBase(T context)
    {
        Context = context;
    }
}