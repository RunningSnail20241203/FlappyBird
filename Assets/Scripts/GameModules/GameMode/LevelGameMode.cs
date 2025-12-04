using System.Collections;
using GameModules.Bird;
using GameModules.Commands;
using GameModules.Events;
using GameModules.GameMode.GameModeData;
using GameModules.Obstacle.GenerateStrategy;
using Infra.Command;
using Infra.Event;
using Infra.GameMode;
using UnityEngine;

namespace GameModules.GameMode
{
    public class LevelGameMode : GameModeBase
    {
        public override GameModeType ModeType => GameModeType.Level;

        private int _curScore;
        private int _curHp;

        public override void Initialize()
        {
            base.Initialize();

            // 注册事件
            EventManager.Instance.Subscribe<BirdCollisionEvent>(OnBirdCollision);
        }

        public override void Start()
        {
            base.Start();

            _curHp = 3;
            _curScore = 0;
            GenerationStrategy.StartGenerating();
        }

        public override void Cleanup()
        {
            base.Cleanup();

            GenerationStrategy.Cleanup();
            _curHp = 3;
            _curScore = 0;

            // 注册事件
            EventManager.Instance.Unsubscribe<BirdCollisionEvent>(OnBirdCollision);
        }

        public override void Pause()
        {
            base.Pause();

            GenerationStrategy.PauseGeneration();
        }

        public override void Resume()
        {
            base.Resume();

            GenerationStrategy.ResumeGeneration();
        }

        public override IEnumerator SetGameModeData(IGameModeArg arg)
        {
            yield return base.SetGameModeData(arg);

            if (arg is not LevelGameData levelData) yield break;

            Debug.Log($"设置关卡ID: {levelData.LevelId}");
            // 可以在这里处理关卡ID
            GenerationStrategy = new LevelObstacleStrategy();
            GenerationStrategy.InitializeAsync(new LevelGenerationStrategyData
            {
                LevelId = levelData.LevelId,
            });
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
                case DecreaseLifeCommand temp:
                    BirdManager.Instance.ChangeLife(temp.BirdId, -temp.DecreaseCount);
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
                    _curHp -= 1;
                    if (_curHp <= 0)
                    {
                        GameStateManager.Instance.AddCommand(new GameOverCommand());
                    }
                    break;
                case ScoreTriggerTag:
                    ScoreManager.Instance.AddScore(BirdManager.Instance.MyBird.name, 1);
                    _curScore += 1;

                    if (GenerationStrategy is LevelObstacleStrategy levelStrategy &&
                        _curScore >= levelStrategy.MaxObstacleCount)
                    {
                        GameStateManager.Instance.AddCommand(new GameOverCommand());
                    }

                    break;
            }
        }
    }
}