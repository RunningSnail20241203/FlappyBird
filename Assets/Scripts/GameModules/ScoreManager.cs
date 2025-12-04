using System;
using System.Collections.Generic;
using Infra;

namespace GameModules
{
    public class ScoreManager : MonoSingleton<ScoreManager>
    {
        public event Action<string, int> OnScoreChanged;
        private readonly Dictionary<string, int> _scores = new();

        public void AddScore(string key, int score)
        {
            if (!_scores.TryAdd(key, score))
            {
                _scores[key] = score + _scores[key];
            }

            OnScoreChanged?.Invoke(key, _scores[key]);
        }

        public int QueryScore(string key)
        {
            return _scores.GetValueOrDefault(key, 0);
        }

        public void ClearAllScores()
        {
            foreach (var kv in _scores)
            {
                OnScoreChanged?.Invoke(kv.Key, 0);
            }
            _scores.Clear();
        }
    }
}