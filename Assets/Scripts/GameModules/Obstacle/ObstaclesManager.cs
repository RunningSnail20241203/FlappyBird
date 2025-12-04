using System.Collections.Generic;
using GameModules.ObjectPool;
using Infra;
using UnityEngine;

namespace GameModules.Obstacle
{
    public class ObstaclesManager : MonoSingleton<ObstaclesManager>, IObstacleSpawner
    {
        #region Private members

        private const string ObstaclePoolName = "ObstaclePool";
        private const string WorldCanvasTag = "WorldCanvas";
        private Transform _parent; // 用于存放生成的障碍物对象
        private readonly List<GameObject> _obstacles = new();

        #endregion

        public void Spawn(Vector3 pos, float rowGap, Vector2 moveSpeed)
        {
            var pipe = GameObjectPool.Instance.Get(ObstaclePoolName, _parent, pos, Quaternion.identity, Vector3.one);
            var controller = pipe.GetComponent<ObstacleController>();
            controller.Initialize(rowGap, this);
            _obstacles.Add(pipe);

            controller.StartMove(moveSpeed);
        }

        public void Recycle(GameObject obj)
        {
            _obstacles.Remove(obj);
            GameObjectPool.Instance.Return(obj);
        }

        public void ReturnAllObstacles()
        {
            _obstacles.ForEach(x => GameObjectPool.Instance.Return(x));
            _obstacles.Clear();
        }

        public void PauseAllObstacles()
        {
            _obstacles.ForEach(x => x.GetComponent<ObstacleController>().PauseMove());
        }

        public void ResumeAllObstacles()
        {
            _obstacles.ForEach(x => x.GetComponent<ObstacleController>().ResumeMove());
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            CreatePool();

            _parent = GameObject.FindGameObjectWithTag(WorldCanvasTag).transform;
        }

        private void CreatePool()
        {
            // 创建管道对象池
            var pipePoolConfig = new GameObjectPool.PoolConfig
            {
                poolName = ObstaclePoolName,
                prefab = ConfigManager.Instance.GlobalConfig.obstaclePrefab,
                initialSize = 3, // 预加载的数量
                maxSize = 20, // 池子最大数量，超过了直接销毁，不回收
                poolParent = transform,
                OnUsed = OnUseObstacle,
                OnRecycled = OnRecycleObstacle,
            };

            GameObjectPool.Instance.CreatePool(pipePoolConfig);
        }

        private void OnUseObstacle(GameObject go)
        {
            go.transform.SetParent(_parent);
        }

        private void OnRecycleObstacle(GameObject go)
        {
            var controller = go.GetComponent<ObstacleController>();
            controller.StopMove();
        }
    }
}