using UnityEngine;

public class BaseView : MonoBehaviour, IView
{
    public virtual void Show()
    {
    }

    public virtual void Hide()
    {
    }

    public virtual void Refresh()
    {
    }

    public virtual bool IsShowing => false;
}