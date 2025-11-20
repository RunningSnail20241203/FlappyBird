using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIManager : MonoSingleton<UIManager>
{
    private Transform _uiRoot; // UI父节点
    private readonly Dictionary<string, UIBase> _loadedUIs = new();// 已加载的UI字典
    private readonly Dictionary<string, AsyncOperationHandle<GameObject>> _uiHandles = new(); // Addressable资源句柄字典
    private UIBase _currentUI;  // 当前正在显示的UI

    public void ShowMenuPanel()
    {
        ShowUI(UIScreen.MainMenu);
    }

    public void ShowGamePanel()
    {
        ShowUI(UIScreen.Game);
    }

    public void ShowPausePanel()
    {
        ShowUI(UIScreen.Pause);
    }

    public void ShowGameOverPanel()
    {
        ShowUI(UIScreen.GameOver);
    }

    public void ShowSettingPanel()
    {
        ShowUI(UIScreen.Settings);
    }


    // 显示UI
    public void ShowUI(string uiName, Action<UIBase> onComplete = null)
    {
        if (!_loadedUIs.ContainsKey(uiName))
        {
            LoadUI(uiName, OnLoaded);
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

            // 隐藏当前UI
            if (_currentUI != null)
            {
                _currentUI.Hide(ShowCurrentUI);
            }
            else
            {
                ShowCurrentUI();
            }

            return;

            void ShowCurrentUI()
            {
                // 显示新UI
                _currentUI = _loadedUIs[uiName];
                _currentUI.Show(() => { onComplete?.Invoke(_currentUI); });
            }
        }
    }

    // 隐藏当前UI
    public void HideCurrentUI(Action onComplete = null)
    {
        if (_currentUI != null)
        {
            _currentUI.Hide(() =>
            {
                _currentUI = null;
                onComplete?.Invoke();
            });
        }
        else
        {
            onComplete?.Invoke();
        }
    }

    // 预加载多个UI
    public void PreloadUIs(string[] uiNames, Action<int> onProgress = null, Action onComplete = null)
    {
        StartCoroutine(PreloadUIsRoutine(uiNames, onProgress, onComplete));
    }

    private IEnumerator PreloadUIsRoutine(string[] uiNames, Action<int> onProgress, Action onComplete)
    {
        var loadedCount = 0;

        foreach (var uiName in uiNames)
        {
            if (_loadedUIs.ContainsKey(uiName))
            {
                loadedCount++;
                onProgress?.Invoke(loadedCount * 100 / uiNames.Length);
                continue;
            }

            var loadCompleted = false;
            var loadSuccess = false;

            LoadUI(uiName, (success) =>
            {
                loadCompleted = true;
                loadSuccess = success;
            });

            // 等待这个UI加载完成
            yield return new WaitUntil(() => loadCompleted);

            if (!loadSuccess) continue;

            loadedCount++;
            onProgress?.Invoke(loadedCount * 100 / uiNames.Length);
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
    private void UnloadUI(string uiName, Action onComplete = null)
    {
        if (_loadedUIs.ContainsKey(uiName))
        {
            if (_currentUI == _loadedUIs[uiName])
            {
                _currentUI = null;
            }

            // 销毁GameObject
            _loadedUIs[uiName].Destroy();
            _loadedUIs.Remove(uiName);

            // 释放Addressable资源
            if (_uiHandles.ContainsKey(uiName))
            {
                Addressables.Release(_uiHandles[uiName]);
                _uiHandles.Remove(uiName);
            }
        }

        onComplete?.Invoke();
    }

    // 同步接口：加载UI（立即返回，内部异步加载）
    private void LoadUI(string uiName, Action<bool> onComplete = null)
    {
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
        StartCoroutine(LoadUIRoutine(uiName, onComplete));
    }

    // 内部协程处理异步加载
    private IEnumerator LoadUIRoutine(string uiName, Action<bool> onComplete)
    {
        // 异步加载Addressable资源
        var handle = Addressables.LoadAssetAsync<GameObject>(uiName);
        _uiHandles[uiName] = handle;

        // 等待加载完成
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var instance = Instantiate(handle.Result, _uiRoot);
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

    protected override void OnAwake()
    {
        base.OnAwake();

        var obj = GameObject.FindGameObjectWithTag("DefaultCanvas");
        if (obj == null)
        {
            Debug.LogError("找不到 DefaultCanvas 游戏对象");
            return;
        }

        _uiRoot = obj.transform;
    }
}