using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : UIBase
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button quitButton;
    
    private MenuViewModel _menuViewModel;

    public override void Initialize()
    {
        base.Initialize();
        uiName = "MainMenu";
        
        startButton.onClick.AddListener(OnClickStartButton);
        optionButton.onClick.AddListener(OnClickOptionButton);
        quitButton.onClick.AddListener(OnClickQuitButton);
    }

    private void OnClickQuitButton()
    {
        _menuViewModel.QuitGame();
    }

    private void OnClickOptionButton()
    {
        UIManager.Instance.ShowUI(UIScreen.Settings);
    }

    private void OnClickStartButton()
    {
        UIManager.Instance.ShowUI(UIScreen.Game);
    }
}