using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIManager : MonoSingleton<UIManager>
{
    private Transform _uiRoot; // UI父节点
    private readonly Dictionary<string, UIBase> _loadedUIs = new(); // 已加载的UI字典
    private readonly Dictionary<string, AsyncOperationHandle<GameObject>> _uiHandles = new(); // Addressable资源句柄字典

    private const string UICanvasName = "UICanvas";

    public void ShowMenuPanel()
    {
        ShowUI(new LoadUIConfig { UIName = UIScreen.MainMenu });
    }

    public void HideMenuPanel()
    {
        HideUI(UIScreen.MainMenu);
    }

    public void ShowGamePanel()
    {
        ShowUI(new LoadUIConfig { UIName = UIScreen.Game });
    }

    public void HideGamePanel()
    {
        HideUI(UIScreen.Game);
    }

    public void ShowPausePanel()
    {
        ShowUI(new LoadUIConfig { UIName = UIScreen.Pause });
    }

    public void HidePausePanel()
    {
        HideUI(UIScreen.Pause);
    }

    public void ShowGameOverPanel()
    {
        ShowUI(new LoadUIConfig { UIName = UIScreen.GameOver });
    }

    public void HideGameOverPanel()
    {
        HideUI(UIScreen.GameOver);
    }

    public void ShowSettingPanel()
    {
        ShowUI(new LoadUIConfig { UIName = UIScreen.Settings });
    }

    public void HideSettingPanel()
    {
        HideUI(UIScreen.Settings);
    }


    public void ShowUI<T>(LoadUIConfig<T> config) where T : UIBase
    {
        if (!IsValid())
        {
            Debug.LogError("UI系统未初始化");
            return;
        }

        var uiName = config.UIName;
        var onComplete = config.OnComplete;
        if (config.Parent == null) config.Parent = _uiRoot;

        if (!_loadedUIs.ContainsKey(uiName))
        {
            LoadUI(config, OnLoaded);
        }
        else
        {
            OnLoaded(true);
        }

        return;

        void OnLoaded(bool success)
        {
            if (!success)
            {
                Debug.LogError($"加载UI失败: {uiName}");
                onComplete?.Invoke(null);
                return;
            }

            var ui = _loadedUIs[uiName];
            ui.Show(() => { onComplete?.Invoke(ui as T); });
        }
    }


    // 显示UI
    public void ShowUI(LoadUIConfig config)
    {
        if (!IsValid())
        {
            Debug.LogError("UI系统未初始化");
            return;
        }

        var uiName = config.UIName;
        var onComplete = config.OnComplete;
        if (config.Parent == null) config.Parent = _uiRoot;

        if (!_loadedUIs.ContainsKey(uiName))
        {
            LoadUI(config, OnLoaded);
        }
        else
        {
            OnLoaded(true);
        }

        return;

        void OnLoaded(bool success)
        {
            if (!success)
            {
                Debug.LogError($"加载UI失败: {uiName}");
                onComplete?.Invoke(null);
                return;
            }

            var ui = _loadedUIs[uiName];
            ui.Show(() => { onComplete?.Invoke(ui); });
        }
    }

    // 隐藏指定UI
    public void HideUI(string uiName)
    {
        if (!_loadedUIs.TryGetValue(uiName, out var ui))
        {
            Debug.LogError($"UI:{uiName}未加载");
            return;
        }

        ui.Hide();
    }

    // 预加载多个UI
    public void PreloadUIs(LoadUIConfig[] configs, Action<int> onProgress = null, Action onComplete = null)
    {
        StartCoroutine(PreloadUIsRoutine(configs, onProgress, onComplete));
    }

    private IEnumerator PreloadUIsRoutine(LoadUIConfig[] configs, Action<int> onProgress, Action onComplete)
    {
        var loadedCount = 0;

        foreach (var config in configs)
        {
            if (_loadedUIs.ContainsKey(config.UIName))
            {
                loadedCount++;
                onProgress?.Invoke(loadedCount * 100 / configs.Length);
                continue;
            }

            var loadCompleted = false;
            var loadSuccess = false;

            LoadUI(config, (success) =>
            {
                loadCompleted = true;
                loadSuccess = success;
            });

            // 等待这个UI加载完成
            yield return new WaitUntil(() => loadCompleted);

            if (!loadSuccess) continue;

            loadedCount++;
            onProgress?.Invoke(loadedCount * 100 / configs.Length);
        }

        onComplete?.Invoke();
    }

    // 获取UI实例
    public T GetUI<T>(string uiName) where T : UIBase
    {
        if (_loadedUIs.TryGetValue(uiName, out var i))
        {
            return i as T;
        }

        return null;
    }

    // 卸载UI
    private void UnloadUI(string uiName)
    {
        if (!_loadedUIs.TryGetValue(uiName, out var ui))
        {
            Debug.LogError($"UI:{uiName}未加载");
            return;
        }

        // 销毁GameObject
        ui.Destroy();
        _loadedUIs.Remove(uiName);

        // 释放Addressable资源
        if (!_uiHandles.TryGetValue(uiName, out var handle)) return;
        Addressables.Release(handle);
        _uiHandles.Remove(uiName);
    }

    private void LoadUI<T>(LoadUIConfig<T> config, Action<bool> onComplete = null) where T : UIBase
    {
        var uiName = config.UIName;
        var parent = config.Parent;
        if (_loadedUIs.ContainsKey(uiName))
        {
            Debug.LogWarning($"UI {uiName} 已经加载");
            onComplete?.Invoke(true);
            return;
        }

        if (_uiHandles.ContainsKey(uiName))
        {
            Debug.LogWarning($"UI {uiName} 正在加载中");
            return;
        }

        // 开始异步加载，但提供同步接口
        StartCoroutine(LoadUIRoutine(config, onComplete));
    }

    // 同步接口：加载UI（立即返回，内部异步加载）
    private void LoadUI(LoadUIConfig config, Action<bool> onComplete = null)
    {
        var uiName = config.UIName;
        var parent = config.Parent;
        if (_loadedUIs.ContainsKey(uiName))
        {
            Debug.LogWarning($"UI {uiName} 已经加载");
            onComplete?.Invoke(true);
            return;
        }

        if (_uiHandles.ContainsKey(uiName))
        {
            Debug.LogWarning($"UI {uiName} 正在加载中");
            return;
        }

        // 开始异步加载，但提供同步接口
        StartCoroutine(LoadUIRoutine(config, onComplete));
    }

    private IEnumerator LoadUIRoutine<T>(LoadUIConfig<T> config, Action<bool> onComplete = null) where T : UIBase
    {
        var uiName = config.UIName;
        var parent = config.Parent;

        // 异步加载Addressable资源
        var handle = Addressables.LoadAssetAsync<GameObject>(GetUIPath(uiName));
        _uiHandles[uiName] = handle;

        // 等待加载完成
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var instance = Instantiate(handle.Result, parent);
            Addressables.Release(handle);
            _uiHandles.Remove(uiName);

            var uiComponent = instance.GetComponent<UIBase>();

            if (uiComponent != null)
            {
                uiComponent.Initialize();
                uiComponent.Hide(); // 默认隐藏
                _loadedUIs[uiName] = uiComponent;
                onComplete?.Invoke(true);
            }
            else
            {
                Debug.LogError($"UI {uiName} 没有UIBase组件");
                onComplete?.Invoke(false);
            }
        }
        else
        {
            Debug.LogError($"加载UI失败: {uiName}");
            Addressables.Release(handle);
            _uiHandles.Remove(uiName);
            onComplete?.Invoke(false);
        }
    }

    // 内部协程处理异步加载
    private IEnumerator LoadUIRoutine(LoadUIConfig config, Action<bool> onComplete = null)
    {
        var uiName = config.UIName;
        var parent = config.Parent;

        // 异步加载Addressable资源
        var handle = Addressables.LoadAssetAsync<GameObject>(GetUIPath(uiName));
        _uiHandles[uiName] = handle;

        // 等待加载完成
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var instance = Instantiate(handle.Result, parent);
            Addressables.Release(handle);
            _uiHandles.Remove(uiName);

            var uiComponent = instance.GetComponent<UIBase>();

            if (uiComponent != null)
            {
                uiComponent.Initialize();
                uiComponent.Hide(); // 默认隐藏
                _loadedUIs[uiName] = uiComponent;
                onComplete?.Invoke(true);
            }
            else
            {
                Debug.LogError($"UI {uiName} 没有UIBase组件");
                onComplete?.Invoke(false);
            }
        }
        else
        {
            Debug.LogError($"加载UI失败: {uiName}");
            Addressables.Release(handle);
            _uiHandles.Remove(uiName);
            onComplete?.Invoke(false);
        }
    }

    private bool IsValid()
    {
        return _uiRoot != null;
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        var obj = GameObject.FindGameObjectWithTag(UICanvasName);
        if (obj == null)
        {
            Debug.LogError("找不到 DefaultCanvas 游戏对象");
            return;
        }

        _uiRoot = obj.transform;
    }

    private string GetUIPath(string viewName)
    {
        return $"Assets/Prefabs/UI/Views/{viewName}View.prefab";
    }
}