using System;
using System.Collections.Generic;
using GameModules.Bird;
using GameModules.Commands;
using Infra.Command;
using Infra.GameMode;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameModules.State
{
    /// <summary>
    /// 游戏进行状态
    /// </summary>
    public class PlayingState : GameStateBase
    {
        protected override Dictionary<string, Action<ICommand>> CommandHandlers => new()
        {
            { nameof(GameOverCommand), GameOverCommandHandler },
            { nameof(PauseGameCommand), PauseGameCommandHandler },
            { nameof(ResumeGameCommand), ResumeGameCommandHandler },
            { nameof(StartGameCommand), StartGameCommandHandler },
            { nameof(OpenMainMenuCommand), ReturnMainMenuCommandHandler },
        };

        private IGameMode _currentGameMode;

        public override void OnEnter()
        {
            base.OnEnter();

            _currentGameMode = GameModeManager.Instance.CurrentMode;
            Assert.IsNotNull(_currentGameMode, "当前游戏模式不能为空");

            BirdManager.Instance.ShowAllBirds();
            _currentGameMode.Start();
        }

        public override void OnExit()
        {
            base.OnExit();
            _currentGameMode.Cleanup();
            // 清空积分
            ScoreManager.Instance.ClearAllScores();
        }

        private void GameOverCommandHandler(ICommand obj)
        {
            _currentGameMode.ProcessCommand(obj);
        }

        private void StartGameCommandHandler(ICommand obj)
        {
            _currentGameMode.ProcessCommand(obj);
        }

        private void PauseGameCommandHandler(ICommand obj)
        {
            _currentGameMode.ProcessCommand(obj);
        }

        private void ResumeGameCommandHandler(ICommand obj)
        {
            _currentGameMode.ProcessCommand(obj);
        }

        private void ReturnMainMenuCommandHandler(ICommand obj)
        {
            GameStateManager.Instance.GoToMenu();
        }
    }
}