#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuViewModel : ViewModelBase
{
    public void QuitGame()
    {
        var config = new LoadConfirmDialogConfig()
        {
            Title = "退出游戏",
            Message = "确定要退出游戏吗？",
            OnConfirm = () =>
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            },
            OnCancel = () => { }
        };
        ConfirmDialogManager.Instance.ShowConfirmDialog(config);
    }

    public void StartGame()
    {
        GameStateManager.Instance.AddCommand(new StartGameCommand());
    }
}