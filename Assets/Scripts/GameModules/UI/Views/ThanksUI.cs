using GameModules.UI.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace GameModules.UI.Views
{
    public class ThanksUI : UIBase
    {
        [SerializeField] private Button bgButton;

        public override void Initialize()
        {
            base.Initialize();
            bgButton.onClick.AddListener(OnBgButtonClicked);
        }

        private void OnBgButtonClicked()
        {
            var viewModel = GetViewModel<ThanksViewModel>();
            viewModel.ReturnMenu();
        }
    }
}