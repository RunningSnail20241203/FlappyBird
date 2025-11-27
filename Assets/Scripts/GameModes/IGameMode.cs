public interface IGameMode
{
    GameModeType ModeType { get; }
    void Initialize();
    void Start();
    void Pause();
    void Resume();
    void Cleanup();
    void OnUpdate(float deltaTime);
    void SetGameModeData(IGameModeArg arg);
}