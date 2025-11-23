using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : UIBase
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button quitButton;

    private MainMenuViewModel _mainMenuViewModel;
    
    public override void Initialize()
    {
        base.Initialize();
        uiName = "MainMenu";

        startButton.onClick.AddListener(OnClickStartButton);
        optionButton.onClick.AddListener(OnClickOptionButton);
        quitButton.onClick.AddListener(OnClickQuitButton);
        
        _mainMenuViewModel = ViewModelContainer.Instance.GetViewModel<MainMenuViewModel>();
    }

    private void OnClickQuitButton()
    {
        _mainMenuViewModel.QuitGame();
    }

    private void OnClickOptionButton()
    {
        UIManager.Instance.ShowSettingPanel();
    }

    private void OnClickStartButton()
    {
        _mainMenuViewModel.StartGame();
    }
}