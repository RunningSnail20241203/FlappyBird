public class PauseViewModel : ViewModelBase
{
    public void StartGame()
    {
        GameStateManager.Instance.AddCommand(new PauseGameCommand());
    }
}