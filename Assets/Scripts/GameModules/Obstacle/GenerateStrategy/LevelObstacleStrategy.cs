using System;
using System.Collections;
using GameModules.GameMode.GameModeData;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Logger = Infra.Logger;

namespace GameModules.Obstacle.GenerateStrategy
{
    /// <summary>
    /// 关卡障碍物生成策略
    /// </summary>
    public class LevelObstacleStrategy : IObstacleGenerationStrategy
    {
        public int MaxObstacleCount => _config.levelInfos.Count;
        
        private LevelConfig _config;
        private bool _isGenerating;
        private float _nextGenerationTime;
        private float _pauseTime;
        private int _currentIndex;
        
        
        public void Initialize(IObstacleGenerationData data)
        {
            throw new NotImplementedException();
        }

        public IEnumerator InitializeAsync(IObstacleGenerationData data)
        {
            if (data is not LevelGenerationStrategyData levelData) yield break;
            
            
            var handle = Addressables.LoadAssetAsync<LevelConfig>(GetLevelName(levelData.LevelId));
            yield return handle;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Logger.LogError($"LevelObstacleStrategy|InitializeAsync|Load LevelConfig Failed|{levelData.LevelId}");
                yield break;
            }
            
            _config = handle.Result;
        }

        public void StartGenerating()
        {
            _isGenerating = true;
            _currentIndex = 0;
            _nextGenerationTime = _config.levelInfos[_currentIndex].spawnTime;
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

            if (_currentIndex >= _config.levelInfos.Count)
            {
                _isGenerating = false;
                return;
            }

            if (Time.time >= _nextGenerationTime)
            {
                _currentIndex = Mathf.Min(_config.levelInfos.Count - 1, _currentIndex + 1);

                var info = _config.levelInfos[_currentIndex];
                _nextGenerationTime = info.spawnTime;

                var y1 = info.posInY;
                var y2 = info.height;
                ObstaclesManager.Instance.Spawn(new Vector3(_config.xSpawn, y1, 0), y2, _config.moveSpeed);
            }
        }

        private string GetLevelName(int levelId)
        {
            return string.Format($"LevelConfig{levelId}");
        }
    }
}