public class GameOverViewModel : ViewModelBase
{
    public Observable<int> Score { get; } = new();

    public void RestartGame()
    {
        GameStateManager.Instance.AddCommand(new ReStartGameCommand());
    }

    public void ReturnMainMenu()
    {
        GameStateManager.Instance.AddCommand(new ReturnMainMenuCommand());
    }

    protected override void InitializeProperties()
    {
        base.InitializeProperties();
        ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
        OnScoreChanged(BirdManager.Instance.MyBird.name,
            ScoreManager.Instance.QueryScore(BirdManager.Instance.MyBird.name));
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        ScoreManager.Instance.OnScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(string key, int score)
    {
        Score.SetValue(score);
    }
}