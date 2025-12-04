using System.Collections;
using System.Collections.Generic;
using GameModules.GameMode;
using Infra;
using Infra.GameMode;
using UnityEngine;

namespace GameModules
{
    public class GameModeManager : MonoSingleton<GameModeManager>
    {
        private GameModeType _gameModeType = GameModeType.Infinite;

        private readonly Dictionary<GameModeType, IGameMode> _gameModeCache = new();
        public IGameMode CurrentMode { get; private set; }

        public void SetGameMode(GameModeType gameModeType)
        {
            _gameModeType = gameModeType;
            InitializeGameMode();
        }

        /// <summary>
        /// 初始化游戏模式
        /// </summary>
        private void InitializeGameMode()
        {
            CurrentMode = GetOrCreateGameMode(_gameModeType);

            if (CurrentMode != null)
            {
                Debug.Log($"游戏模式已初始化为: {_gameModeType}");
            }
            else
            {
                Debug.LogError($"无法创建游戏模式: {_gameModeType}");
            }
        }

        /// <summary>
        /// 获取或创建游戏模式实例（带缓存）
        /// </summary>
        private IGameMode GetOrCreateGameMode(GameModeType modeType)
        {
            // 如果缓存中已存在，直接返回
            if (_gameModeCache.TryGetValue(modeType, out var cachedMode))
            {
                cachedMode.Initialize();
                return cachedMode;
            }

            // 创建新的游戏模式实例
            var newGameMode = CreateGameMode(modeType);
            if (newGameMode == null) return null;
            newGameMode.Initialize();
            _gameModeCache[modeType] = newGameMode;

            return newGameMode;
        }

        /// <summary>
        /// 创建游戏模式实例
        /// </summary>
        private IGameMode CreateGameMode(GameModeType modeType)
        {
            return modeType switch
            {
                GameModeType.Infinite => new InfiniteGameMode(),
                GameModeType.Level => new LevelGameMode(),
                GameModeType.Challenge => new ChallengeGameMode(),
                GameModeType.Online => new OnlineGameMode(),
                _ => null
            };
        }

        /// <summary>
        /// 预加载所有游戏模式（可选）
        /// </summary>
        public void PreloadAllGameModes()
        {
            var allTypes = System.Enum.GetValues(typeof(GameModeType));
            foreach (GameModeType type in allTypes)
            {
                GetOrCreateGameMode(type);
            }

            Debug.Log($"已预加载所有游戏模式，共 {_gameModeCache.Count} 个");
        }

        /// <summary>
        /// 设置当前游戏模式的数据
        /// </summary>
        public IEnumerator SetCurrentModeData(IGameModeArg modeData)
        {
            yield return CurrentMode?.SetGameModeData(modeData);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            CurrentMode?.OnUpdate(Time.deltaTime);
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            CurrentMode?.OnFixedUpdate(Time.fixedDeltaTime);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            // 清理所有缓存的游戏模式
            foreach (var gameMode in _gameModeCache.Values)
            {
                gameMode?.Cleanup();
            }

            _gameModeCache.Clear();

            Debug.Log("已清理所有游戏模式缓存");
        }
    }
}