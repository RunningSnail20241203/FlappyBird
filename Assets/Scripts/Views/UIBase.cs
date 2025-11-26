using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    private readonly List<IViewModel> _viewModels = new();

    public string UIName => GetType().Name;
    public bool IsVisible => _isVisible;
    private bool _isVisible = false;


    public virtual void Initialize()
    {
    }

    public virtual void Show(Action onComplete = null)
    {
        if (_isVisible)
        {
            onComplete?.Invoke();
            return;
        }

        gameObject.SetActive(true);
        _isVisible = true;

        // 简单的显示逻辑，子类可以重写添加动画
        OnShow();
        onComplete?.Invoke();
    }

    public virtual void Hide(Action onComplete = null)
    {
        if (!_isVisible)
        {
            onComplete?.Invoke();
            return;
        }

        gameObject.SetActive(false);
        _isVisible = false;

        OnHide();
        onComplete?.Invoke();
    }

    public virtual void Destroy()
    {
        OnDestroy();
        if (gameObject != null)
            Destroy(gameObject);
    }

    protected T GetViewModel<T>() where T : class, IViewModel, new()
    {
        var viewModel = ViewModelContainer.Instance.GetViewModel<T>();
        _viewModels.Add(viewModel);
        return viewModel;
    }

    protected virtual void OnShow()
    {
    }

    protected virtual void OnHide()
    {
        ReleaseAllViewModels();
    }

    protected virtual void OnDestroy()
    {
        ReleaseAllViewModels();
    }

    private void ReleaseAllViewModels()
    {
        foreach (var viewModel in _viewModels)
        {
            ViewModelContainer.Instance?.ReleaseViewModel(viewModel);
        }
        _viewModels.Clear();
    }
}