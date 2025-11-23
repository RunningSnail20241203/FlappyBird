public class GameViewModel : ViewModelBase
{
    public Observable<int> Score { get; } = new();

    public void PauseGame()
    {
        GameStateManager.Instance.PauseGame();
    }
}