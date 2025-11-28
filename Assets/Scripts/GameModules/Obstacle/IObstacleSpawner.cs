using UnityEngine;

namespace GameModules.Pipe
{
    public interface IObstacleSpawner
    {
        void Recycle(GameObject obj);
    }
}