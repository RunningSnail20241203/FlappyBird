using System.Collections;
using UnityEngine;

/// <summary>
/// 关卡管道生成策略
/// </summary>
public class LevelPipeStrategy : MonoBehaviour, IPipeGenerationStrategy
{
    private PipeSpawner _spawner;
    private LevelData _levelData;
    private int _currentPipeIndex = 0;

    public void Initialize()
    {
        _spawner = PipeSpawner.Instance;
    }

    public void SetLevelData(LevelData levelData)
    {
        _levelData = levelData;
        _currentPipeIndex = 0;
    }

    public void StartGenerating()
    {
        // 使用关卡预设数据生成管道
        StartCoroutine(GenerateLevelPipes());
    }

    private IEnumerator GenerateLevelPipes()
    {
        foreach (var pipeData in _levelData.Pipes)
        {
            yield return new WaitForSeconds(pipeData.SpawnDelay);
            SpawnPipeAtPosition(pipeData.Position, pipeData.IsUpper);
            _currentPipeIndex++;
        }
    }

    private void SpawnPipeAtPosition(Vector2 position, bool isUpper)
    {
        // 特定位置生成管道逻辑
    }

    public void StopGenerating()
    {
        StopAllCoroutines();
        _spawner.PauseSpawning();
    }

    public void Cleanup()
    {
        StopGenerating();
        _spawner.ReturnAllPipes();
    }

    public class LevelData
    {
        public PipeData[] Pipes;
    }

    public class PipeData
    {
        public Vector2 Position;
        public bool IsUpper;
        public float SpawnDelay;
    }
}