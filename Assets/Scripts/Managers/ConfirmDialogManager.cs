using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmDialogManager : MonoSingleton<ConfirmDialogManager>
{
    private Transform _container;
    private int _dialogId = 0;
    private Dictionary<int, UIBase> _dialogs = new();
    
    public int ShowConfirmDialog(LoadConfirmDialogConfig config)
    {
        if (!IsValid()) return -1;
        _dialogId += 1;
        
        var tempDialogId = _dialogId;
        UIManager.Instance.ShowUI(new LoadUIConfig<ConfirmDialogUI>
        {
            UIName = UIScreen.ConfirmDialog,
            Parent = _container,
            OnComplete = ui =>
            {
                _dialogs.Add(tempDialogId, ui);
                ui.Show();
            }
        });
        
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