using System.Collections;
using GameModules.Obstacle;
using GameModules.Obstacle.GenerateStrategy;
using Infra.Command;

namespace Infra.GameMode
{
    /// <summary>
    /// 模式策略基类
    /// </summary>
    public abstract class GameModeBase : IGameMode
    {
        public abstract GameModeType ModeType { get; }

        protected IObstacleGenerationStrategy GenerationStrategy;

        protected const string CollisionTag = "Obstacle";
        protected const string ScoreTriggerTag = "ScoreTrigger";

        public virtual void Initialize()
        {
        }

        public virtual void Start()
        {
        }

        public virtual void Pause()
        {
        }

        public virtual void Resume()
        {
        }

        public virtual void Restart()
        {
        }

        public virtual void End()
        {
        }

        public virtual void Cleanup()
        {
        }

        public virtual void OnUpdate(float deltaTime)
        {
        }

        public virtual void OnFixedUpdate(float fixedDeltaTime)
        {
        }

        public virtual IEnumerator SetGameModeData(IGameModeArg arg)
        {
            yield break;
        }

        public virtual void ProcessCommand(ICommand command)
        {
        }
    }
}