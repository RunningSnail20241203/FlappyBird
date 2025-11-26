using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : UIBase
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button thanksButton;

    public override void Initialize()
    {
        base.Initialize();

        startButton.onClick.AddListener(OnClickStartButton);
        optionButton.onClick.AddListener(OnClickOptionButton);
        thanksButton.onClick.AddListener(OnClickThanksButton);
    }

    private void OnClickThanksButton()
    {
        var mainMenuViewModel = GetViewModel<MainMenuViewModel>();
        mainMenuViewModel.OpenThanks();
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