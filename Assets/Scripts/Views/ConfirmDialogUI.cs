using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public void Show(LoadConfirmDialogConfig config)
    {
        base.Show();
        titleText.text = config.Title;
        messageText.text = config.Message;
        confirmButton.gameObject.SetActive(config.OnConfirm != null);
        cancelButton.gameObject.SetActive(config.OnCancel != null);
        _onConfirm = config.OnConfirm;
        _onCancel = config.OnCancel;
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