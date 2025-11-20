using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmDialogManager : MonoSingleton<ConfirmDialogManager>
{
    private Transform _container;
    private int _dialogId = 0;
    // private Dictionary<int, >
    
    public int ShowConfirmDialog(string title, string message, Action onConfirm)
    {
        if (!IsValid()) return -1;
        _dialogId += 1;
        return _dialogId;
    }

    public int ShowConfirmDialog(string title, string message, Action onConfirm, Action onCancel)
    {
        if (!IsValid()) return -1;
        _dialogId += 1;
        return _dialogId;
    }

    public void CloseConfirmDialog(int dialogId)
    {
        if (!IsValid()) return;
    }

    private bool IsValid()
    {
        return _container != null;
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        var obj = GameObject.FindGameObjectWithTag("DialogCanvas");
        if (obj == null)
        {
            Debug.LogError("找不到 DialogCanvas 游戏对象");
            return;
        }
        _container = obj.transform;
    }
}