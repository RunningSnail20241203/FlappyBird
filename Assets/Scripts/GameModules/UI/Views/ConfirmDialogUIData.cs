using System;

namespace GameModules.UI.Views
{
    public struct ConfirmDialogUIData
    {
        public string Title;
        public string Message;
        public Action OnConfirm;
        public Action OnCancel;
    }
}