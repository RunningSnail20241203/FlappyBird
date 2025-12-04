using System.Collections;
using GameModules.Commands;
using GameModules.State;
using Infra;
using Infra.Command;
using Infra.GameMode;
using Infra.StateMachine;
using UnityEngine;

namespace GameModules
{
    /// <summary>
    /// 游戏状态管理器
    /// </summary>
    public class GameStateManager : MonoSingleton<GameStateManager>
    {
        public StateMachine<GameStateBase> StateMachine { get; private set; }
        public GameStateBase CurrentState => StateMachine.CurrentState;

        /// <summary>
        /// 切换到菜单状态
        /// </summary>
        public void GoToMenu()
        {
            StateMachine.ChangeState<MenuState>();
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        public void StartPlay(GameModeType gameMode, IGameModeArg args = null)
        {
            StartCoroutine(StartPlayInternal(gameMode, args));
        }

        /// <summary>
        /// 恢复游戏
        /// </summary>
        public void ResumeGame()
        {
            StateMachine.ChangeState<PlayingState>();
        }

        public void GotoSettings()
        {
            StateMachine.ChangeState<SettingState>();
        }

        public void GotoThanks()
        {
            StateMachine.ChangeState<ThanksState>();
        }

        public void AddCommand(ICommand command)
        {
            StateMachine.CurrentState.AddCommand(command);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            InitializeStateMachine();
            Debug.Log("GameStateManager 初始化完成");
        }

        private void InitializeStateMachine()
        {
            StateMachine = new StateMachine<GameStateBase>();

            // 创建状态实例
            var menuState = new MenuState();
            var playingState = new PlayingState();
            var settingState = new SettingState();
            var thanksState = new ThanksState();

            // 添加状态
            StateMachine.AddState(menuState);
            StateMachine.AddState(playingState);
            StateMachine.AddState(settingState);
            StateMachine.AddState(thanksState);

            // 监听状态变化
            StateMachine.OnStateChanged += OnStateChanged;
        }

        protected override void OnFixedUpdate()
        {
            StateMachine?.OnFixedUpdate(Time.fixedDeltaTime);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                AddCommand(new QuitGameCommand());
            }
        }

        private void OnStateChanged(GameStateBase oldState, GameStateBase newState)
        {
            // 状态变化时的额外处理
        }


        protected override void OnDestroy()
        {
            if (!_isApplicationQuitting) StateMachine?.Clear();
            base.OnDestroy();
        }

        private IEnumerator StartPlayInternal(GameModeType gameMode, IGameModeArg args = null)
        {
            GameModeManager.Instance.SetGameMode(gameMode);
            yield return GameModeManager.Instance.SetCurrentModeData(args);
            StateMachine.ChangeState<PlayingState>();
        }
    }
}