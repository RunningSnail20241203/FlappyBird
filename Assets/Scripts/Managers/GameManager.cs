public class GameManager : MonoSingleton<GameManager>
{
    public static BirdController BirdController => BirdController;
    public UIManager UIManager => UIManager;
    public ScoreManager ScoreManager => ScoreManager;
    public AudioManager AudioManager => AudioManager;
    public PipeSpawner PipeSpawner => PipeSpawner;
    public OnlineManager OnlineManager => OnlineManager;
    public CurrencyManager CurrencyManager => CurrencyManager;
    public StateMachine<GameState> StateMachine => GameStateManager.Instance.StateMachine;
}