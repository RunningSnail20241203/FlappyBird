using GameModules.Commands;

namespace GameModules.UI.ViewModels
{
    public class PauseViewModel : ViewModelBase
    {
        public void StartGame()
        {
            GameStateManager.Instance.AddCommand(new PauseGameCommand());
        }
    }
}