using System;
using UnityEngine;

public struct LoadUIConfig
{
    public string UIName;
    public Action<UIBase> OnComplete;

    public Transform Parent;
}

public struct LoadUIConfig<T> where T : UIBase
{
    public string UIName;
    public Action<T> OnComplete;

    public Transform Parent;
}