using GameModules.UI.Views;
using Infra;
using UnityEngine;

namespace GameModules
{
    public class ConfirmDialogManager : MonoSingleton<ConfirmDialogManager>
    {
        private Transform _container;
    
        public void ShowConfirmDialog(ConfirmDialogUIData uiData)
        {
            if (!IsValid()) return;
        
            UIManager.Instance.ShowUI(new LoadUIConfig<ConfirmDialogUI>
            {
                UIName = UIScreen.ConfirmDialog,
                Parent = _container,
                OnComplete = ui =>
                {
                    ui.Show(uiData);
                }
            });
        }

        public void CloseConfirmDialog(int dialogId)
        {
            if (!IsValid()) return;
        }

        private bool IsValid()
        {
            return _container != null;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            var obj = GameObject.FindGameObjectWithTag("DialogCanvas");
            if (obj == null)
            {
                Debug.LogError("找不到 DialogCanvas 游戏对象");
                return;
            }
            _container = obj.transform;
        }
    }
}