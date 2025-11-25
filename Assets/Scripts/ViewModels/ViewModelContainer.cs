using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ViewModelContainer : MonoSingleton<ViewModelContainer>
{
    public enum LifetimeScope
    {
        Scene, // 场景切换时销毁
        Manual, // 手动管理
        Permanent // 永久存在（谨慎使用）
    }

    private readonly Dictionary<Type, IViewModel> _viewModels = new();
    private readonly Dictionary<Type, LifetimeScope> _lifetimeScopes = new();
    private readonly Dictionary<Type, int> _referenceCounts = new();

    private bool _initialized;
    private const string ConfigPath = "Assets/Configs/ViewModelScopeConfig.asset";

    public T GetViewModel<T>() where T : class, IViewModel, new()
    {
        if (!IsValid())
        {
            Debug.LogError("ViewModelContainer,Not Initialized");
            return null;
        }

        var type = typeof(T);

        if (!_viewModels.TryGetValue(type, out var vm))
        {
            vm = new T();
            vm.Initialize();
            _viewModels[type] = vm;
            _referenceCounts[type] = 0;
        }

        _referenceCounts[type]++;
        return vm as T;
    }

    public void ReleaseViewModel<T>() where T : class, IViewModel
    {
        if (!IsValid())
        {
            Debug.LogError("ViewModelContainer,Not Initialized");
            return;
        }

        var type = typeof(T);
        ReleaseViewModel(type);
    }

    public void ReleaseViewModel(IViewModel viewModel)
    {
        if (!IsValid())
        {
            Debug.LogError("ViewModelContainer,Not Initialized");
            return;
        }

        var type = viewModel.GetType();
        ReleaseViewModel(type);
    }

    public bool IsValid()
    {
        return _initialized;
    }

    private bool ShouldDestroyViewModel(Type viewModelType)
    {
        if (!_referenceCounts.TryGetValue(viewModelType, out var count) || count > 0)
            return false;

        if (!_lifetimeScopes.TryGetValue(viewModelType, out var lifetime))
            return true; // 未配置的默认销毁

        return lifetime != LifetimeScope.Permanent;
    }

    private void DestroyViewModel(Type viewModelType)
    {
        if (!_viewModels.TryGetValue(viewModelType, out var vm)) return;
        vm.Dispose();
        _viewModels.Remove(viewModelType);
        _referenceCounts.Remove(viewModelType);
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        StartCoroutine(LoadConfig());
    }

    private IEnumerator LoadConfig()
    {
        var handle = Addressables.LoadAssetAsync<ViewModelScope>(ConfigPath);
        yield return handle;

        // 检查加载是否成功
        if (handle.Status != AsyncOperationStatus.Succeeded || handle.Result == null)
        {
            Debug.LogError($"Failed to load ViewModelScopeConfig from path: {ConfigPath}");
            yield break;
        }

        var configData = handle.Result;

        // 配置生命周期
        foreach (var config in configData.viewModels)
        {
            // 安全获取 Type
            var viewModelType = config.ViewModelType;
            if (viewModelType == null)
            {
                Debug.LogWarning($"Skipping config with null ViewModelType: {config.viewModelTypeName}");
                continue;
            }

            // 检查重复类型
            if (_lifetimeScopes.ContainsKey(viewModelType))
            {
                Debug.LogWarning($"Duplicate ViewModelType found: {viewModelType.Name}. Using latest value.");
            }

            _lifetimeScopes[viewModelType] = config.lifetime;
        }

        _initialized = true;
        Debug.Log($"Successfully loaded {_lifetimeScopes.Count} view model configurations.");
    }

    private void ReleaseViewModel(Type type)
    {
        if (!_referenceCounts.TryGetValue(type, out var count)) return;
        _referenceCounts[type] = count - 1;

        // 根据生命周期决定是否销毁
        if (ShouldDestroyViewModel(type))
        {
            DestroyViewModel(type);
        }
    }
}