using System.Collections;
using System.Collections.Generic;
using GameModules.GameMode.GameModeData;
using Infra;
using UnityEngine.AddressableAssets;

namespace GameModules
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        private bool _isInitialized;
        public List<LevelConfig> Levels { get; }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            StartCoroutine(LoadConfig());
        }

        public bool IsValid()
        {
            return _isInitialized;
        }

        private IEnumerator LoadConfig()
        {
            for (var i = 1; i <= 2; i++)
            {
                var handle = Addressables.LoadAssetAsync<LevelConfig>($"Assets/Data/Levels/LevelConfig{i}.asset");
                yield return handle;
                Levels.Add(handle.Result);
            }

            _isInitialized = true;
        }
    }
}