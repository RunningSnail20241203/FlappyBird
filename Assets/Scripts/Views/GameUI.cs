using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : UIBase
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    private GameViewModel _gameViewModel;

    public override void Initialize()
    {
        base.Initialize();

        _gameViewModel = ViewModelContainer.Instance.GetViewModel<GameViewModel>();
        _gameViewModel.Score.OnValueChanged += UpdateScore;
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }

    private void UpdateScore(int obj)
    {
        scoreText.text = obj.ToString();
    }

    protected override void OnShow()
    {
        base.OnShow();
        UpdateScore(_gameViewModel.Score.Value);
    }

    private void OnPauseButtonClicked()
    {
        _gameViewModel.PauseGame();
    }
}