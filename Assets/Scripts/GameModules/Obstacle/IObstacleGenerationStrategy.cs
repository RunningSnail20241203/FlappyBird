/// <summary>
/// 管道生成策略接口
/// </summary>
public interface IObstacleGenerationStrategy
{
    void Initialize();
    void StartGenerating();
    void PauseGeneration();
    void ResumeGeneration();
    void Cleanup();
    void FixedUpdate(float fixedDeltaTime);
    void Update(float deltaTime);
}