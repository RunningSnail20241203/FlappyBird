#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuViewModel : ViewModelBase
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
            }
        };
        ConfirmDialogManager.Instance.ShowConfirmDialog(config);
    }

    public void StartGame()
    {
        UIManager.Instance.ShowGamePanel();
        UIManager.Instance.HideMenuPanel();
    }
}