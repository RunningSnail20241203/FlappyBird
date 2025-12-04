using GameModules.UI.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace GameModules.UI.Views
{
    public class PauseUI : UIBase
    {
        [SerializeField] private Button startButton;

        public override void Initialize()
        {
            base.Initialize();

            startButton.onClick.AddListener(OnClickStartButton);
        }

        private void OnClickStartButton()
        {
            var pauseViewModel = GetViewModel<PauseViewModel>();
            pauseViewModel.StartGame();
        }
    }
}