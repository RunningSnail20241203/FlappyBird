using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : UIBase
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button quitButton;

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
        var mainMenuViewModel = GetViewModel<MainMenuViewModel>();
        mainMenuViewModel.QuitGame();
    }

    private void OnClickOptionButton()
    {
        var mainMenuViewModel = GetViewModel<MainMenuViewModel>();
        mainMenuViewModel.OpenSetting();
    }

    private void OnClickStartButton()
    {
        var mainMenuViewModel = GetViewModel<MainMenuViewModel>();
        mainMenuViewModel.StartGame();
    }
}