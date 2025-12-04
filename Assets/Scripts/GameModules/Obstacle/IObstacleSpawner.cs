using UnityEngine;

namespace GameModules.Obstacle
{
    public interface IObstacleSpawner
    {
        void Recycle(GameObject obj);
    }
}