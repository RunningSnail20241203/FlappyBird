using GameModules.UI.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace GameModules.UI.Views
{
    public class SettingUI : UIBase
    {
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Button returnMenuButton;

        public override void Initialize()
        {
            base.Initialize();

            musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
            soundToggle.onValueChanged.AddListener(OnSoundToggleChanged);
            returnMenuButton.onClick.AddListener(OnReturnMenuButtonClicked);
        }

        protected override void OnShow()
        {
            base.OnShow();

            var settingModel = GetViewModel<SettingViewModel>();
            UpdateMusicToggle(settingModel.IsMusicEnabled.Value);
            UpdateSoundToggle(settingModel.IsSoundEnabled.Value);

            settingModel.IsMusicEnabled.OnValueChanged += UpdateMusicToggle;
            settingModel.IsSoundEnabled.OnValueChanged += UpdateSoundToggle;
        }

        protected override void OnHide()
        {
            // 这里有点问题，因为依赖了OnHide的执行顺序，目前只能先放到OnHide前面，后面优化一下
            // 因为在OnHide里面释放了所有的ViewModel，下面的代码会重新创建一个新的SettingViewModel,那就没有意义了，而且有内存没法释放
            var settingModel = GetViewModel<SettingViewModel>();
            settingModel.IsMusicEnabled.OnValueChanged -= UpdateMusicToggle;
            settingModel.IsSoundEnabled.OnValueChanged -= UpdateSoundToggle;

            base.OnHide();
        }

        private void OnReturnMenuButtonClicked()
        {
            var settingModel = ViewModelContainer.Instance.GetViewModel<SettingViewModel>();
            settingModel.ReturnMenu();
        }

        private void OnSoundToggleChanged(bool arg0)
        {
            var settingModel = ViewModelContainer.Instance.GetViewModel<SettingViewModel>();
            settingModel.ToggleSound(arg0);
        }

        private void OnMusicToggleChanged(bool arg0)
        {
            var settingModel = ViewModelContainer.Instance.GetViewModel<SettingViewModel>();
            settingModel.ToggleMusic(arg0);
        }

        private void UpdateMusicToggle(bool arg0)
        {
            musicToggle.SetIsOnWithoutNotify(arg0);
        }

        private void UpdateSoundToggle(bool obj)
        {
            soundToggle.SetIsOnWithoutNotify(obj);
        }
    }
}