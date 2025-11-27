/// <summary>
/// 管道生成策略接口
/// </summary>
public interface IPipeGenerationStrategy
{
    void Initialize();
    void StartGenerating();
    void StopGenerating();
    void Cleanup();
}