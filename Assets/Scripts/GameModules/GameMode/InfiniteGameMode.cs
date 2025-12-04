using GameModules.Audio;
using GameModules.Bird;
using GameModules.Commands;
using GameModules.Events;
using GameModules.Obstacle;
using GameModules.Obstacle.GenerateStrategy;
using Infra;
using Infra.Command;
using Infra.Event;
using Infra.GameMode;
using UnityEngine;

namespace GameModules.GameMode
{
    public class InfiniteGameMode : GameModeBase
    {
        public override GameModeType ModeType => GameModeType.Infinite;
        

        public override void Initialize()
        {
            base.Initialize();
            // 使用随机管道生成策略
            GenerationStrategy = new RandomGenerationStrategy();
            GenerationStrategy.Initialize(null);
        
            // 注册事件
            EventManager.Instance.Subscribe<BirdCollisionEvent>(OnBirdCollision);
        }

        public override void Start()
        {
            base.Start();
        
            GenerationStrategy.StartGenerating();
            UIManager.Instance.ShowGamePanel();
            AudioManager.Instance.PlayBackgroundMusic("GameMusic");
            BirdManager.Instance.StartBirds();
        }

        public override void Restart()
        {
            base.Restart();
            Cleanup();
            Start();
            // 注册事件
            EventManager.Instance.Subscribe<BirdCollisionEvent>(OnBirdCollision);
        }

        public override void End()
        {
            base.End();
            UIManager.Instance.ShowGameOverPanel();
            AudioManager.Instance.PlayBackgroundMusic("EndMusic");
            BirdManager.Instance.PauseBirds();
            GenerationStrategy.PauseGeneration();
        }

        public override void Pause()
        {
            base.Pause();
            UIManager.Instance.ShowPausePanel();
            BirdManager.Instance.PauseBirds();
            GenerationStrategy.PauseGeneration();
        }

        public override void Resume()
        {
            base.Resume();
            UIManager.Instance.HidePausePanel();
            BirdManager.Instance.ResumeBirds();
            GenerationStrategy.ResumeGeneration();
        }

        public override void Cleanup()
        {
            base.Cleanup();
            UIManager.Instance.HideGamePanel();
            UIManager.Instance.HideGameOverPanel();
            BirdManager.Instance.ResetBirds();
            GenerationStrategy.Cleanup();
            EventManager.Instance.Unsubscribe<BirdCollisionEvent>(OnBirdCollision);
        }

        public override void OnFixedUpdate(float fixedDeltaTime)
        {
            base.OnFixedUpdate(fixedDeltaTime);
            GenerationStrategy.FixedUpdate(fixedDeltaTime);
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            GenerationStrategy.Update(deltaTime);
        }
    
        public override void ProcessCommand(ICommand command)
        {
            base.ProcessCommand(command);
            switch (command)
            {
                case GameOverCommand:
                    End();
                    break;
                case StartGameCommand:
                    Restart();
                    break;
                case PauseGameCommand:
                    Pause();
                    break;
                case ResumeGameCommand:
                    Resume();
                    break;
            }
        }

        private void OnBirdCollision(BirdCollisionEvent e)
        {
            if (e.EventArgs is not BirdCollisionEventArg arg) return;
            Debug.Log($"BirdController: Collision with {arg.ColliderTag}");
            switch (arg.ColliderTag)
            {
                case CollisionTag:
                    // 游戏结束逻辑
                    GameStateManager.Instance.AddCommand(new GameOverCommand());
                    break;
                case ScoreTriggerTag:
                    // 
                    ScoreManager.Instance.AddScore(BirdManager.Instance.MyBird.name, 1);
                    break;
            }
        }
    }
}