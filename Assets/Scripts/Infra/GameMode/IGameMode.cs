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
    void SetGameModeData(IGameModeArg arg);
    void ProcessCommand(ICommand command);
}