public struct UIEvent
{
    public UIEventType UIEventType;
}

public enum UIEventType
{
    StartGame = 0,
    ShowOptionView = 1,
    QuitGame = 2,
    PauseGame = 3,
    ResumeGame = 4,
    RestartGame = 5,
    ReturnToMenu = 6,
}