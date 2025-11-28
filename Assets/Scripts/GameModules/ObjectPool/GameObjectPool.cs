using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity GameObject 对象池
/// </summary>
public class GameObjectPool : MonoSingleton<GameObjectPool>
{
    [Serializable]
    public class PoolConfig
    {
        public string poolName; // 对象池名称
        public GameObject prefab; // 预制体
        public int initialSize = 10; // 预加载的数量
        public int maxSize = 50; // 池子最大数量，超过了直接销毁，不回收
        public Transform poolParent; // 对象池的父节点
        public Action<GameObject> OnUsed; // 对象被使用时的回调
        public Action<GameObject> OnRecycled; // 对象被归还时的回调
    }

    [SerializeField] private List<PoolConfig> poolConfigs = new();

    private readonly Dictionary<string, ObjectPool<GameObject>> _pools = new();
    private readonly Dictionary<GameObject, string> _objectToPoolMap = new();

    protected override void OnInitialize()
    {
        base.OnInitialize();
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var config in poolConfigs)
        {
            CreatePool(config);
        }
    }

    /// <summary>
    /// 创建对象池
    /// </summary>
    public void CreatePool(PoolConfig config)
    {
        if (_pools.ContainsKey(config.poolName))
        {
            Debug.LogWarning($"对象池 {config.poolName} 已存在");
            return;
        }

        var pool = new ObjectPool<GameObject>(
            createFunc: () => CreateNewObject(config),
            onGet: obj => OnGetObject(obj, config),
            onReturn: obj => OnReturnObject(obj, config),
            onDestroy: Destroy,
            initialSize: config.initialSize,
            maxSize: config.maxSize
        );

        _pools.Add(config.poolName, pool);
    }

    private GameObject CreateNewObject(PoolConfig config)
    {
        var obj = Instantiate(config.prefab, config.poolParent ?? transform, true);
        obj.SetActive(false);

        // 添加池对象组件以便自动归还
        var poolObject = obj.GetComponent<PooledObject>() ?? obj.AddComponent<PooledObject>();
        poolObject.PoolName = config.poolName;

        _objectToPoolMap[obj] = config.poolName;

        return obj;
    }

    private void OnGetObject(GameObject obj, PoolConfig config)
    {
        obj.SetActive(true);
        config.OnUsed?.Invoke(obj);
    }

    private void OnReturnObject(GameObject obj, PoolConfig config)
    {
        obj.SetActive(false);
        obj.transform.SetParent(GetPoolParent(obj));
        config.OnRecycled?.Invoke(obj);
    }

    private Transform GetPoolParent(GameObject obj)
    {
        if (!_objectToPoolMap.TryGetValue(obj, out var poolName)) return transform;

        var config = poolConfigs.Find(c => c.poolName == poolName);
        return config?.poolParent ?? transform;
    }

    /// <summary>
    /// 从对象池获取对象
    /// </summary>
    public GameObject Get(string poolName, Transform parent, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        if (!_pools.TryGetValue(poolName, out var pool))
        {
            Debug.LogError($"对象池 {poolName} 不存在");
            return null;
        }

        var obj = pool.Get();
        if (obj == null) return obj;

        obj.transform.SetParent(parent);
        obj.transform.localPosition = position;
        obj.transform.localRotation = rotation;
        obj.transform.localScale = scale;

        return obj;
    }

    /// <summary>
    /// 从对象池获取对象
    /// </summary>
    public GameObject Get(string poolName, Transform parent)
    {
        return Get(poolName, parent, Vector3.zero, Quaternion.identity, Vector3.one);
    }

    /// <summary>
    /// 归还对象到对象池
    /// </summary>
    public void Return(GameObject obj)
    {
        if (obj == null) return;

        var pooledObject = obj.GetComponent<PooledObject>();
        if (pooledObject != null && _pools.TryGetValue(pooledObject.PoolName, out var pool))
        {
            pool.Return(obj);
        }
        else
        {
            // 如果没有找到对应的池，直接销毁
            Destroy(obj);
        }
    }

    /// <summary>
    /// 获取对象池统计信息
    /// </summary>
    public void GetPoolStats(out Dictionary<string, (int active, int inactive)> stats)
    {
        stats = new Dictionary<string, (int, int)>();
        foreach (var kvp in _pools)
        {
            stats[kvp.Key] = (kvp.Value.CountActive, kvp.Value.CountInactive);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        foreach (var pool in _pools.Values)
        {
            pool.Clear();
        }

        _pools.Clear();
        _objectToPoolMap.Clear();
    }
}