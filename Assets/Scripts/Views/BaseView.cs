using UnityEngine;

public class BaseView : MonoBehaviour, IView
{
    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
    }

    protected virtual void OnDestroy()
    {
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Refresh()
    {
    }

    public virtual bool IsShowing => false;
}