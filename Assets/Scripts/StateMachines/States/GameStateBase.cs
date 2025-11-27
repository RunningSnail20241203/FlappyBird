using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

/// <summary>
/// 游戏状态基类
/// </summary>
public abstract class GameStateBase : StateBase
{
    private readonly List<ICommand> _commands = new();
    protected virtual Dictionary<string, Action<ICommandArg>> CommandHandlers { get; } = new();

    public override void OnFixedUpdate(float fixedDeltaTime)
    {
        base.OnFixedUpdate(fixedDeltaTime);
        ProcessCommands();
    }

    public override void OnExit()
    {
        base.OnExit();
        ClearCommands();
    }

    public void AddCommand(ICommand commandBase)
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

        var copyCommands = new List<ICommand>(_commands);
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
                handlerFunc?.Invoke(command.Args);
            }
            else
            {
                Debug.LogError($"没有找到命令处理函数: {command.Name}");
            }
        }
    }

    private void QuitGame()
    {
        var config = new ConfirmDialogUIData()
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