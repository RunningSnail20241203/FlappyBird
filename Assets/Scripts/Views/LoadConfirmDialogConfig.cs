using System;

public struct LoadConfirmDialogConfig
{
    public string Title;
    public string Message;
    public Action OnConfirm;
    public Action OnCancel;
}