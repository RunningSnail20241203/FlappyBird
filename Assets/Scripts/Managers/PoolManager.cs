using UnityEngine;

/// <summary>
/// 对象池管理器
/// </summary>
public class PoolManager : MonoSingleton<PoolManager>
{
    protected override void OnInitialize()
    {
        base.OnInitialize();
        PreloadPools();
        Debug.Log("PoolManager 初始化完成");
    }

    /// <summary>
    /// 预加载所有对象池
    /// </summary>
    private void PreloadPools()
    {
        // 这里可以预加载常用的对象池
        // 例如：子弹、特效、敌人等
    }

    /// <summary>
    /// 显示对象池统计信息
    /// </summary>
    public void LogPoolStats()
    {
        GameObjectPool.Instance.GetPoolStats(out var stats);
        
        foreach (var stat in stats)
        {
            Debug.Log($"对象池 [{stat.Key}]: 活跃={stat.Value.active}, 闲置={stat.Value.inactive}");
        }
    }
}