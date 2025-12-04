using System.Collections;
using GameModules.Config;
using GameModules.Obstacle;
using Infra;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameModules
{
    public class ConfigManager : MonoSingleton<ConfigManager>
    {
        public GlobalConfig GlobalConfig { get; private set; }
        public RandomGenerationConfig RandomGenerationConfig { get; private set; }

        private const string GlobalConfigPath = "Assets/Configs/GlobalConfig.asset";

        public bool IsValid()
        {
            return RandomGenerationConfig != null && GlobalConfig != null;;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            RandomGenerationConfig = new RandomGenerationConfig()
            {
                GenerateInterval = 2f,
                YRange1 = new Vector2(-150f, 150f),
                YRange2 = new Vector2(350f, 600f),
                XSpawn = 1100f,
                MoveSpeed = new Vector2(-200f, 0f),
            };

            StartCoroutine(LoadConfig());
        }

        IEnumerator LoadConfig()
        {
            var handle = Addressables.LoadAssetAsync<GlobalConfig>(GlobalConfigPath);
            yield return handle;
            GlobalConfig = handle.Result;
        }
    }
}