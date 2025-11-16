using UnityEngine;

/// <summary>
/// 池对象组件，用于自动归还
/// </summary>
public class PooledObject : MonoBehaviour
{
    public string PoolName { get; set; }

    private void OnDisable()
    {
        // 对象被禁用时自动归还到池中（可选）
        // 根据具体需求决定是否启用此功能
        // if (!string.IsNullOrEmpty(PoolName))
        // {
        //     GameObjectPool.Instance.Return(gameObject);
        // }
    }
}