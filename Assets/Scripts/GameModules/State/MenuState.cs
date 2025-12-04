using System;
using System.Collections.Generic;
using GameModules.Audio;
using GameModules.Bird;
using GameModules.Commands;
using Infra;
using Infra.Command;
using Infra.GameMode;

namespace GameModules.State
{
    /// <summary>
    /// 菜单状态
    /// </summary>
    public class MenuState : GameStateBase
    {
        protected override Dictionary<string, Action<ICommand>> CommandHandlers => new()
        {
            { nameof(StartGameCommand), StartGameCommandHandler },
            { nameof(OpenSettingCommand), OpenSettingCommandHandler },
            { nameof(OpenThanksCommand), OpenThanksCommandHandler },
            { nameof(OpenLevelsCommand), OpenLevelsCommandHandler }
        };

        public override void OnEnter()
        {
            base.OnEnter();
            UIManager.Instance.ShowMenuPanel();
            AudioManager.Instance.PlayBackgroundMusic("MenuMusic");
            BirdManager.Instance.HideAllBirds();
        }

        public override void OnExit()
        {
            base.OnExit();
            UIManager.Instance.HideMenuPanel();
        }

        private void StartGameCommandHandler(ICommand args)
        {
            if (args is not StartGameCommand command) return;

            if (command.GameMode == GameModeType.Level)
            {
                UIManager.Instance.HideLevelsPanel();
            }

            GameStateManager.Instance.StartPlay(command.GameMode, command.Args);
        }

        private void OpenSettingCommandHandler(ICommand obj)
        {
            GameStateManager.Instance.GotoSettings();
        }

        private void OpenThanksCommandHandler(ICommand obj)
        {
            GameStateManager.Instance.GotoThanks();
        }

        private void OpenLevelsCommandHandler(ICommand obj)
        {
            UIManager.Instance.ShowLevelsPanel();
        }
    }
}