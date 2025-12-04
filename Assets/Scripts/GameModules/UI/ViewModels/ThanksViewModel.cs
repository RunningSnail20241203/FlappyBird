using GameModules.Commands;

namespace GameModules.UI.ViewModels
{
    public class ThanksViewModel : ViewModelBase
    {
        public void ReturnMenu()
        {
            GameStateManager.Instance.AddCommand(new OpenMainMenuCommand());
        }
    }
}