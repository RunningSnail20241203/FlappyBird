using GameModules.Bird;
using GameModules.Commands;

namespace GameModules.UI.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        public Observable<int> Score => GetObservable<int>() as Observable<int>;

        public void PauseGame()
        {
            GameStateManager.Instance.AddCommand(new PauseGameCommand());
        }

        protected override void InitializeProperties()
        {
            base.InitializeProperties();
            OnScoreChanged(string.Empty, ScoreManager.Instance.QueryScore(BirdManager.Instance.MyBird.name));

            ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            ScoreManager.Instance.OnScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(string key, int score)
        {
            Score.SetValue(score);
        }
    }
}