using UnityEngine;

namespace Infra.ObjectPool
{
    /// <summary>
    /// 池对象组件，用于自动归还
    /// </summary>
    public class PooledObject : MonoBehaviour
    {
        public string PoolName { get; set; }
    }
}