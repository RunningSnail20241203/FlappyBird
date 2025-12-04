using System.Collections;
using Infra.Command;

namespace Infra.GameMode
{
    public interface IGameMode
    {
        GameModeType ModeType { get; }
        void Initialize();
        void Start();
        void Pause();
        void Resume();
        void Restart();
        void End();
        void Cleanup();
        void OnUpdate(float deltaTime);
        void OnFixedUpdate(float fixedDeltaTime);
        IEnumerator SetGameModeData(IGameModeArg arg);
        void ProcessCommand(ICommand command);
    }
}