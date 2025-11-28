using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : UIBase
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    public override void Initialize()
    {
        base.Initialize();
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }

    private void UpdateScore(int obj)
    {
        scoreText.text = obj.ToString();
    }

    protected override void OnShow()
    {
        base.OnShow();
        var gameViewModel = GetViewModel<GameViewModel>();
        UpdateScore(gameViewModel.Score.Value);
        
        gameViewModel.Score.OnValueChanged += UpdateScore;
    }

    protected override void OnHide()
    {
        var gameViewModel = GetViewModel<GameViewModel>();
        gameViewModel.Score.OnValueChanged -= UpdateScore;
        base.OnHide();
    }

    private void OnPauseButtonClicked()
    {
        var gameViewModel = GetViewModel<GameViewModel>();
        gameViewModel.PauseGame();
    }
}