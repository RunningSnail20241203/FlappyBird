public class MainMenuViewModel : ViewModelBase
{
    public void OpenSetting()
    {
        GameStateManager.Instance.AddCommand(new OpenSettingCommand());
    }

    public void OpenThanks()
    {
        GameStateManager.Instance.AddCommand(new OpenThanksCommand());
    }

    public void StartInfiniteGame()
    {
        GameStateManager.Instance.AddCommand(new StartGameCommand
        {
            GameMode = GameModeType.Infinite,
        });
    }

    public void OpenLevels()
    {
        GameStateManager.Instance.AddCommand(new OpenLevelsCommand());
    }

    public void OpenLeaderBoard()
    {
        GameStateManager.Instance.AddCommand(new OpenLeaderBoardCommand());
    }

    public void StartMatch()
    {
        GameStateManager.Instance.AddCommand(new StartMatchCommand());
    }
}