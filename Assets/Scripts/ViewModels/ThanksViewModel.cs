public class ThanksViewModel : ViewModelBase
{
    public void ReturnMenu()
    {
        GameStateManager.Instance.AddCommand(new ReturnMainMenuCommand());
    }
}