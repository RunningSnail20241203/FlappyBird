using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : UIBase
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button quitButton;

    private MenuBaseViewModel _menuBaseViewModel;
    
    public override void Initialize()
    {
        base.Initialize();
        uiName = "MainMenu";

        startButton.onClick.AddListener(OnClickStartButton);
        optionButton.onClick.AddListener(OnClickOptionButton);
        quitButton.onClick.AddListener(OnClickQuitButton);
        
        _menuBaseViewModel = ViewModelContainer.Instance.GetViewModel<MenuBaseViewModel>();
    }

    private void OnClickQuitButton()
    {
        _menuBaseViewModel.QuitGame();
    }

    private void OnClickOptionButton()
    {
        UIManager.Instance.ShowSettingPanel();
    }

    private void OnClickStartButton()
    {
        _menuBaseViewModel.StartGame();
    }
}