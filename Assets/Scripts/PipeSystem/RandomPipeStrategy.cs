/// <summary>
/// 随机管道生成策略
/// </summary>
public class RandomPipeStrategy : IPipeGenerationStrategy
{
    private PipeSpawner _spawner;
    private bool _isGenerating = false;

    public void Initialize()
    {
        _spawner = PipeSpawner.Instance;
    }

    public void StartGenerating()
    {
        _isGenerating = true;
        _spawner.StartSpawning();
    }

    public void StopGenerating()
    {
        _isGenerating = false;
        _spawner.PauseSpawning();
    }

    public void Cleanup()
    {
        _spawner.ReturnAllPipes();
    }
}