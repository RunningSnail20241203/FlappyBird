public class UIManager : MonoSingleton<UIManager>
{
    private WaitStartView _waitStartView;

    protected override void Initialize()
    {
        base.Initialize();
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
}