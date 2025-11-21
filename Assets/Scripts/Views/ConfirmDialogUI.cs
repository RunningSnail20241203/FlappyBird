using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmDialogUI : UIBase
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    public override void Initialize()
    {
        base.Initialize();
        
        confirmButton.onClick.AddListener(OnClickConfirmButton);
        cancelButton.onClick.AddListener(OnClickCancelButton);
    }
    
    

    private void OnClickConfirmButton()
    {
        
    }

    private void OnClickCancelButton()
    {
        
    }
}