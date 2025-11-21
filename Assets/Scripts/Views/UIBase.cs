using System;
using Unity.VisualScripting;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [SerializeField] protected string uiName;
    [SerializeField] protected bool isVisible = false;

    public string UIName => uiName;
    public bool IsVisible => isVisible;

    public virtual void Initialize() { }
    
    public virtual void Show(Action onComplete = null)
    {
        if (isVisible) 
        {
            onComplete?.Invoke();
            return;
        }
        
        gameObject.SetActive(true);
        isVisible = true;
        
        // 简单的显示逻辑，子类可以重写添加动画
        OnShow();
        onComplete?.Invoke();
    }
    
    public virtual void Hide(Action onComplete = null)
    {
        if (!isVisible) 
        {
            onComplete?.Invoke();
            return;
        }
        
        gameObject.SetActive(false);
        isVisible = false;
        
        OnHide();
        onComplete?.Invoke();
    }
    
    public virtual void Destroy()
    {
        OnDestroy();
        if (gameObject != null)
            Destroy(gameObject);
    }
    
    protected virtual void OnShow() { }
    protected virtual void OnHide() { }
    protected virtual void OnDestroy() { }
}