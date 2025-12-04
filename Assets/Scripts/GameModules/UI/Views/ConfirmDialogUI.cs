using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameModules.UI.Views
{
    public class ConfirmDialogUI : UIBase
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;

        private Action _onConfirm;
        private Action _onCancel;

        public override void Initialize()
        {
            base.Initialize();

            confirmButton.onClick.AddListener(OnClickConfirmButton);
            cancelButton.onClick.AddListener(OnClickCancelButton);
        }

        public void Show(ConfirmDialogUIData uiData)
        {
            base.Show();
            titleText.text = uiData.Title;
            messageText.text = uiData.Message;
            confirmButton.gameObject.SetActive(uiData.OnConfirm != null);
            cancelButton.gameObject.SetActive(uiData.OnCancel != null);
            _onConfirm = uiData.OnConfirm;
            _onCancel = uiData.OnCancel;
        }

        private void OnClickConfirmButton()
        {
            _onConfirm.Invoke();
            Hide();
        }

        private void OnClickCancelButton()
        {
            _onCancel.Invoke();
            Hide();
        }
    }
}