public class MainMenuViewModel : ViewModelBase
{
    public void StartGame()
    {
        GameStateManager.Instance.AddCommand(new StartGameCommand());
    }

    public void OpenSetting()
    {
        GameStateManager.Instance.AddCommand(new OpenSettingCommand());
    }
    
    public void OpenThanks()
    {
        GameStateManager.Instance.AddCommand(new OpenThanksCommand());
    }
}