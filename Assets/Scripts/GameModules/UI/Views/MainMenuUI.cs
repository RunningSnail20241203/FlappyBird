using GameModules.UI.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace GameModules.UI.Views
{
    public class MainMenuUI : UIBase
    {
        [SerializeField] private Button infiniteButton;
        [SerializeField] private Button levelButton;
        [SerializeField] private Button matchButton;
        [SerializeField] private Button leaderBoardButton;
        [SerializeField] private Button optionButton;
        [SerializeField] private Button thanksButton;

        public override void Initialize()
        {
            base.Initialize();

            infiniteButton.onClick.AddListener(OnClickInfiniteButton);
            optionButton.onClick.AddListener(OnClickOptionButton);
            thanksButton.onClick.AddListener(OnClickThanksButton);
            levelButton.onClick.AddListener(OnClickLevelButton);
            matchButton.onClick.AddListener(OnClickMatchButton);
            leaderBoardButton.onClick.AddListener(OnClickLeaderBoardButton);
        }

        private void OnClickLeaderBoardButton()
        {
            var mainMenuViewModel = GetViewModel<MainMenuViewModel>();
            mainMenuViewModel.OpenLeaderBoard();
        }

        private void OnClickMatchButton()
        {
            var mainMenuViewModel = GetViewModel<MainMenuViewModel>();
            mainMenuViewModel.StartMatch();
        }

        private void OnClickLevelButton()
        {
            var mainMenuViewModel = GetViewModel<MainMenuViewModel>();
            mainMenuViewModel.OpenLevels();
        }

        private void OnClickThanksButton()
        {
            var mainMenuViewModel = GetViewModel<MainMenuViewModel>();
            mainMenuViewModel.OpenThanks();
        }

        private void OnClickOptionButton()
        {
            var mainMenuViewModel = GetViewModel<MainMenuViewModel>();
            mainMenuViewModel.OpenSetting();
        }

        private void OnClickInfiniteButton()
        {
            var mainMenuViewModel = GetViewModel<MainMenuViewModel>();
            mainMenuViewModel.StartInfiniteGame();
        }
    }
}