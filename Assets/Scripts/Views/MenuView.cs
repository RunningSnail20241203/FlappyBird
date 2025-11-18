using UnityEngine;
using UnityEngine.UI;

public class MenuView : BaseView
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button quitButton;

    protected override void Awake()
    {
        base.Awake();
        startButton.onClick.AddListener(OnClickStartButton);
        optionButton.onClick.AddListener(OnClickOptionButton);
        quitButton.onClick.AddListener(OnClickQuitButton);
    }

    private void OnClickQuitButton()
    {
    }

    private void OnClickOptionButton()
    {
    }

    private void OnClickStartButton()
    {
        UIManager.Instance.ProcessUIEvent(new UIEvent(){UIEventType = UIEventType.StartGame});
    }
}