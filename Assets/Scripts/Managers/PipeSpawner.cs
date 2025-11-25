using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PipeSpawner : MonoSingleton<PipeSpawner>
{
    private const string PipePoolName = "PipePool";
    private const string PipeTriggerPoolName = "PipeTriggerPool";
    private const string GlobalConfigPath = "Assets/Configs/GlobalConfig.asset";
    private const string WorldCanvasTag = "WorldCanvas";


    private readonly List<BaseSceneComController> _sceneComs = new();
    private float _timer;
    private bool _isSpawning;
    private GlobalConfig _globalConfig;
    private bool _isInitialized;
    private Transform _parent;

    public void StartSpawning()
    {
        _isSpawning = true;
        _timer = 0f;
        SpawnOnePairPipe();
    }

    public void PauseSpawning()
    {
        _isSpawning = false;
        foreach (var pipe in _sceneComs)
        {
            pipe.StopMove();
        }
    }

    public void ResumeSpawning()
    {
        _isSpawning = true;
        foreach (var pipe in _sceneComs)
        {
            pipe.ResumeMove();
        }
    }

    public bool IsValid()
    {
        return _isInitialized;
    }

    /// <summary>
    /// 回收所有管道
    /// </summary>
    public void ReturnAllPipes()
    {
        foreach (var pipe in _sceneComs)
        {
            GameObjectPool.Instance.Return(pipe.gameObject);
        }

        _sceneComs.Clear();
    }

    public void ReturnPipe(BaseSceneComController pipe)
    {
        GameObjectPool.Instance.Return(pipe.gameObject);
        _sceneComs.Remove(pipe);
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        StartCoroutine(InitializeAsync());
    }


    private void Update()
    {
        if (!_isSpawning) return;

        _timer += Time.deltaTime;

        if (_timer < _globalConfig.spawnInterval) return;
        _timer = 0f;
        SpawnOnePairPipe();
    }

    private void SpawnOnePairPipe()
    {
        SpawnPipe(_globalConfig.pipeCenterY, _globalConfig.pipeIntervalY, _globalConfig.spawnXPosition,
            _globalConfig.pipeMoveSpeed);
        SpawnTrigger(_globalConfig.spawnXPosition + _globalConfig.pipeTriggerOffsetX, _globalConfig.pipeMoveSpeed);
    }

    private void SpawnPipe(Vector2 pipeCenterY, Vector2 pipeIntervalY, float pipeXPosition, float pipeMoveSpeed)
    {
        var randomY = Random.Range(pipeCenterY.x, pipeCenterY.y);
        var randomIntervalY = Random.Range(pipeIntervalY.x, pipeIntervalY.y);
        Debug.Log($"SpawnPipe: {randomY}, {randomIntervalY}");
        var spawnPosition1 = new Vector3(pipeXPosition, randomY + randomIntervalY / 2, 0f);
        var spawnPosition2 = new Vector3(pipeXPosition, randomY - randomIntervalY / 2, 0f);
        SpawnPipe(spawnPosition1, pipeMoveSpeed, true);
        SpawnPipe(spawnPosition2, pipeMoveSpeed, false);
    }

    private void SpawnPipe(Vector3 spawnPosition, float moveSpeed, bool isUp)
    {
        var pipe = GameObjectPool.Instance.Get(PipePoolName, spawnPosition, Quaternion.identity);
        var rect = pipe.GetComponent<RectTransform>();
        rect.pivot = new Vector2(0.5f, isUp ? 0f : 1f);

        var col = pipe.GetComponent<BoxCollider2D>();
        col.offset = new Vector2(col.offset.x, isUp ? col.size.y / 2 : -col.size.y / 2);

        if (pipe == null) return;
        var controller = pipe.GetComponent<PipeController>();
        _sceneComs.Add(controller);
        // 设置管道移动
        controller.StartMove(moveSpeed);
    }

    private void SpawnTrigger(float spawnXPosition, float moveSpeed)
    {
        var spawnPosition = new Vector3(spawnXPosition, 0f, 0f);
        var pipe = GameObjectPool.Instance.Get(PipeTriggerPoolName, spawnPosition, Quaternion.identity);
        if (pipe == null) return;
        var controller = pipe.GetComponent<PipeTriggerController>();
        _sceneComs.Add(controller);
        // 设置管道移动
        controller.StartMove(moveSpeed);
    }

    private IEnumerator InitializeAsync()
    {
        yield return LoadConfig();
        CreatePool();
        _parent = GameObject.FindGameObjectWithTag(WorldCanvasTag).transform;
        _isInitialized = true;
    }

    private IEnumerator LoadConfig()
    {
        var handle = Addressables.LoadAssetAsync<GlobalConfig>(GlobalConfigPath);
        yield return handle;

        _globalConfig = handle.Result;
    }

    private void CreatePool()
    {
        // 创建管道对象池
        var pipePoolConfig = new GameObjectPool.PoolConfig
        {
            poolName = PipePoolName,
            prefab = _globalConfig.pipePrefab,
            initialSize = 5,
            maxSize = 20,
            poolParent = transform,
            OnUsed = OnUsePipe,
            OnRecycled = OnRecyclePipe,
        };

        GameObjectPool.Instance.CreatePool(pipePoolConfig);

        // 创建管道触发器，用来检测小鸟是否飞过管道
        var pipeTriggerPoolConfig = new GameObjectPool.PoolConfig
        {
            poolName = PipeTriggerPoolName,
            prefab = _globalConfig.pipeTriggerPrefab,
            initialSize = 5,
            maxSize = 20,
            poolParent = transform,
            OnUsed = OnUsePipe,
            OnRecycled = OnRecyclePipe,
        };

        GameObjectPool.Instance.CreatePool(pipeTriggerPoolConfig);
    }

    private void OnRecyclePipe(GameObject obj)
    {
        var controller = obj.GetComponent<BaseSceneComController>();
        controller.OnRecycle();
    }

    private void OnUsePipe(GameObject obj)
    {
        obj.transform.SetParent(_parent);

        var controller = obj.GetComponent<BaseSceneComController>();
        controller.OnUse();
    }
}