public class MainMenuViewModel : ViewModelBase
{
    public void QuitGame()
    {

        GameStateManager.Instance.AddCommand(new QuitGameCommand());
    }

    public void StartGame()
    {
        GameStateManager.Instance.AddCommand(new StartGameCommand());
    }

    public void OpenSetting()
    {
        GameStateManager.Instance.AddCommand(new OpenSettingCommand());
    }
}