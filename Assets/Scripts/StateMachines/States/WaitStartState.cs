using System;
using System.Collections.Generic;
using Commands;

public class WaitStartState : GameState
{
    public override string Name => "WaitStartState";
    
    private const float WaitTime = 2f;
    private float _changeStateTimer;
    private bool _readyToChangeState;

    protected override Dictionary<string, Action<BaseCommandArgs>> commandHandlers => new()
    {
        { nameof(StartGameCommand), OnStartGame }
    };

    public override void OnEnter()
    {
        base.OnEnter();

        // 重置小鸟位置
        GameManager.BirdController.ResetBird();
        GameManager.UIManager.ShowWaitStartPanel();
    }

    public override void OnExit()
    {
        base.OnExit();
        GameManager.UIManager.HideWaitStartPanel();
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        if (_readyToChangeState)
        {
            _changeStateTimer -= deltaTime;
            if (_changeStateTimer < 0)
            {
                _readyToChangeState = false;
                GameManager.StateMachine.ChangeState<PlayingState>();
            }
        }
    }

    public override void OnFixedUpdate(float fixedDeltaTime)
    {
        base.OnFixedUpdate(fixedDeltaTime);
    }

    private void OnStartGame(BaseCommandArgs args)
    {
        _readyToChangeState = true;
        _changeStateTimer = WaitTime;
    }
}
