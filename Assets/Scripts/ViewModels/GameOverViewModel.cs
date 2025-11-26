public class GameOverViewModel : ViewModelBase
{
    public Observable<int> Score => GetObservable<int>() as Observable<int>;

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
        OnScoreChanged(string.Empty, ScoreManager.Instance.QueryScore(BirdManager.Instance.MyBird.name));
        ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
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