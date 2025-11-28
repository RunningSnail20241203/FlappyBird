using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : UIBase
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button reStartButton;
    [SerializeField] private Button returnMainMenuButton;
    
    public override void Initialize()
    {
        base.Initialize();
        
        reStartButton.onClick.AddListener(OnReStartButtonClicked);
        returnMainMenuButton.onClick.AddListener(OnReturnMainMenuButtonClicked);

    }

    protected override void OnShow()
    {
        base.OnShow();
        
        var gameOverViewModel = GetViewModel<GameOverViewModel>();
        scoreText.text = $"{gameOverViewModel.Score.Value}";
    }

    private void OnReturnMainMenuButtonClicked()
    {
        var gameOverViewModel = GetViewModel<GameOverViewModel>();
        gameOverViewModel.ReturnMainMenu();
    }

    private void OnReStartButtonClicked()
    {
        var gameOverViewModel = GetViewModel<GameOverViewModel>();
        gameOverViewModel.RestartGame();
    }
}