using GameModules.Pipe;
using UnityEngine;

/// <summary>
/// 随机障碍物生成策略
/// </summary>
public class RandomGenerationStrategy : IObstacleGenerationStrategy
{
    private RandomGenerationConfig _config;
    private bool _isGenerating;
    private float _nextGenerationTime;
    private float _pauseTime;

    public void Initialize()
    {
        _config = ConfigManager.Instance.RandomGenerationConfig;
    }

    public void StartGenerating()
    {
        _isGenerating = true;
        _nextGenerationTime = Time.time;
    }

    public void PauseGeneration()
    {
        _isGenerating = false;
        _pauseTime = Time.time;
        ObstaclesManager.Instance.PauseAllObstacles();
    }

    public void ResumeGeneration()
    {
        _isGenerating = true;
        var elapsedTime = Time.time - _pauseTime;
        _nextGenerationTime += elapsedTime;
        ObstaclesManager.Instance.ResumeAllObstacles();
    }

    public void Cleanup()
    {
        ObstaclesManager.Instance.ReturnAllObstacles();
    }

    public void FixedUpdate(float fixedDeltaTime)
    {
    }

    public void Update(float deltaTime)
    {
        if (!_isGenerating)
        {
            return;
        }

        if (Time.time >= _nextGenerationTime)
        {
            _nextGenerationTime = Time.time + _config.GenerateInterval;

            var y1 = Random.Range(_config.YRange1.x, _config.YRange1.y);
            var y2 = Random.Range(_config.YRange2.x, _config.YRange2.y);
            ObstaclesManager.Instance.Spawn(new Vector3(_config.XSpawn, y1, 0), y2, _config.MoveSpeed);
        }
    }
}