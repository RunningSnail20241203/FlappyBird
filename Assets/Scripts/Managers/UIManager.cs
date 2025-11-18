using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    private WaitStartView _waitStartView;/**/
    private Dictionary<UIEventType, Action> _uiEventHandlers;

    protected override void Initialize()
    {
        base.Initialize();
        InitUIEventHandlers();
    }

    public void ShowMenuPanel()
    {
    }

    public void HideMenuPanel()
    {
    }

    public void ShowGamePanel()
    {
    }

    public void HideGamePanel()
    {
    }

    public void ShowPausePanel()
    {
    }

    public void HidePausePanel()
    {
    }

    public void ShowGameOverPanel()
    {
    }

    public void HideGameOverPanel()
    {
    }

    public void UpdateFinalScore(int score)
    {
    }

    public void ShowLevelCompletePanel()
    {
    }

    public void HideLevelCompletePanel()
    {
    }

    public void ShowWaitStartPanel()
    {
        _waitStartView.Show();
    }

    public void HideWaitStartPanel()
    {
        _waitStartView.Hide();
    }

    public void ShowCountDown(float seconds)
    {
        if (_waitStartView.IsShowing)
        {
            _waitStartView.ShowCountDown(seconds);
        }
    }

    public void ProcessUIEvent(UIEvent uiEvent)
    {
        if (_uiEventHandlers.TryGetValue(uiEvent.UIEventType, out var handler))
        {
            handler();
        }
        else
        {
            Debug.LogError($"[UIManager] 未处理的UI事件类型: {uiEvent.UIEventType}");
        }
    }

    private void InitUIEventHandlers()
    {
        _uiEventHandlers = new Dictionary<UIEventType, Action>()
        {
            {UIEventType.StartGame, ProcessUIEvent_StartGame}
        };
    }

    private void ProcessUIEvent_StartGame()
    {
        GameStateManager.Instance.AddCommand(new StartGameCommand());
    }
}