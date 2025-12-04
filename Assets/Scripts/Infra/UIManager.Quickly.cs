using GameModules.UI.Views;

namespace Infra
{
    public partial class UIManager
    {
        public void ShowMenuPanel()
        {
            ShowUI(new LoadUIConfig { UIName = UIScreen.MainMenu });
        }

        public void HideMenuPanel()
        {
            HideUI(UIScreen.MainMenu);
        }

        public void ShowGamePanel()
        {
            ShowUI(new LoadUIConfig { UIName = UIScreen.Game });
        }

        public void HideGamePanel()
        {
            HideUI(UIScreen.Game);
        }

        public void ShowPausePanel()
        {
            ShowUI(new LoadUIConfig { UIName = UIScreen.Pause });
        }

        public void HidePausePanel()
        {
            HideUI(UIScreen.Pause);
        }

        public void ShowGameOverPanel()
        {
            ShowUI(new LoadUIConfig { UIName = UIScreen.GameOver });
        }

        public void HideGameOverPanel()
        {
            HideUI(UIScreen.GameOver);
        }

        public void ShowSettingPanel()
        {
            ShowUI(new LoadUIConfig { UIName = UIScreen.Settings });
        }

        public void HideSettingPanel()
        {
            HideUI(UIScreen.Settings);
        }
    
        public void ShowThanksPanel()
        {
            ShowUI(new LoadUIConfig { UIName = UIScreen.Thanks });
        }

        public void HideThanksPanel()
        {
            HideUI(UIScreen.Thanks);
        }

        public void ShowLevelsPanel()
        {
            ShowUI(new LoadUIConfig { UIName = UIScreen.Levels});
        }

        public void HideLevelsPanel()
        {
            HideUI(UIScreen.Levels);
        }

    }
}