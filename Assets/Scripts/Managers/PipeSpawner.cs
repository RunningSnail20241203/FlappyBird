using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PipeSpawner : MonoSingleton<PipeSpawner>
{
    [SerializeField] private GlobalConfig globalConfig;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnXPosition = 1100f;
    [SerializeField] private Vector2 upPipeYRange = new Vector2(350f, 600f);
    [SerializeField] private Vector2 downPipeYRange = new Vector2(-600f, -350f);

    private const string PipePoolName = "PipePool";
    private float _timer;
    private readonly List<GameObject> _pipes = new();

    public void StartSpawning()
    {
    }

    public void StopSpawning()
    {
    }

    private void Start()
    {
        // 创建管道对象池
        var pipePoolConfig = new GameObjectPool.PoolConfig
        {
            poolName = PipePoolName,
            prefab = globalConfig.pipePrefab,
            initialSize = 5,
            maxSize = 20,
            parent = transform
        };

        GameObjectPool.Instance.CreatePool(pipePoolConfig);
    }

    // private void Update()
    // {
    //     _timer += Time.deltaTime;
    //
    //     if (_timer < spawnInterval) return;
    //     SpawnOnePairPipe();
    //     _timer = 0f;
    // }

    private void SpawnOnePairPipe()
    {
        SpawnPipe(upPipeYRange);
        SpawnPipe(downPipeYRange);
    }

    private void SpawnPipe(Vector2 yPosRange)
    {
        var randomY = Random.Range(yPosRange.x, yPosRange.y);
        var spawnPosition = new Vector3(spawnXPosition, randomY, 0f);

        var pipe = GameObjectPool.Instance.Get(PipePoolName, spawnPosition, Quaternion.identity);

        if (pipe == null) return;
        _pipes.Add(pipe);
        // 设置管道移动
        var pipeMover = pipe.GetComponent<PipeMover>();
        if (pipeMover != null)
        {
            pipeMover.ResetPipe();
        }
    }

    /// <summary>
    /// 回收所有管道
    /// </summary>
    public void ReturnAllPipes()
    {
        foreach (var pipe in _pipes)
        {
            GameObjectPool.Instance.Return(pipe);
        }

        _pipes.Clear();
    }
}