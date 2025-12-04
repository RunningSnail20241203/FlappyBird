using System.Collections.Generic;
using GameModules.GameMode.GameModeData;
using GameModules.UI.ViewModels.ObservableProperty;
using Infra.GameMode;
using UnityEngine;

namespace GameModules.UI.ViewModels
{
    public class LevelViewModel : ViewModelBase
    {
        public Observable<int> CurrentLevel => GetObservable<int>() as Observable<int>;
        public ObservableList<LevelData> LevelInfos => GetObservable<List<LevelData>>() as ObservableList<LevelData>;

        public void ReturnMainMenu()
        {
            GameStateManager.Instance.GoToMenu();
        }

        public void PlayGame(int levelId)
        {
            GameStateManager.Instance.StartPlay(GameModeType.Level, new LevelGameData()
            {
                LevelId = levelId,
            });
        }

        protected override void InitializeProperties()
        {
            base.InitializeProperties();

            var progress = PlayerPrefs.GetString("progress", string.Empty);
            var datas = new List<LevelData>();
            if (!string.IsNullOrEmpty(progress))
            {
                datas = JsonUtility.FromJson<List<LevelData>>(progress);
            }

            var configs = LevelManager.Instance.Levels;
            for (int i = 0; i < configs.Count; i++)
            {
                
            }

            LevelInfos.SetValue(datas);
        }

        public class LevelData
        {
            public int LevelId;
            public int StarCount;
            public bool IsPlayed;
        }
    }
}