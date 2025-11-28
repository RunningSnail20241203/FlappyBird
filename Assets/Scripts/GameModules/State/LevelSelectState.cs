using System;
using System.Collections.Generic;

namespace GameModules.State
{
    public class LevelSelectState : GameStateBase
    {
        protected override Dictionary<string, Action<ICommand>> CommandHandlers => new()
        {
            { nameof(StartGameCommand), StartGameCommandHandler },
            { nameof(OpenMainMenuCommand), OpenMainMenuCommandHandler },
        };


        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
        }

        public override void OnFixedUpdate(float fixedDeltaTime)
        {
            base.OnFixedUpdate(fixedDeltaTime);
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void StartGameCommandHandler(ICommand args)
        {
            if (args is StartGameCommand command)
            {
                GameStateManager.Instance.StartPlay(command.GameMode, command.Args);
            }
        }

        private void OpenMainMenuCommandHandler(ICommand obj)
        {
            GameStateManager.Instance.GoToMenu();
        }
    }
}