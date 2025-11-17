using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏状态基类
/// </summary>
public abstract class GameState : BaseState
{
    public abstract override string Name { get; }
    protected List<BaseCommand> commands;
    protected virtual Dictionary<string, Action<BaseCommandArgs>> commandHandlers { get; }

    protected static GameManager GameManager => GameManager.Instance;

    public void AddCommand(BaseCommand command)
    {
        commands.Add(command);
    }

    public void ClearCommands()
    {
        commands.Clear();
    }

    private void ProcessCommands()
    {
        if (commands.Count <= 0) return;

        var copyCommands = new List<BaseCommand>(commands);
        commands.Clear();
        foreach (var command in copyCommands)
        {
            if (commandHandlers.TryGetValue(command.Name, out var handlerFunc))
            {
                handlerFunc.Invoke(command.Args);
            }
            else
            {
                Debug.LogError($"没有找到命令处理函数: {command.Name}");
            }
        }
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        ProcessCommands();
    }

    public override void OnExit()
    {
        base.OnExit();
        ClearCommands();
    }
}