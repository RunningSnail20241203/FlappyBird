using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameModules.GameMode.GameModeData
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "GameModes/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public List<LevelInfo> levelInfos;
        public float xSpawn = 1100f;
        public Vector2 moveSpeed = new(-200f, 0f);
    }

    [Serializable]
    public class LevelInfo
    {
        public float spawnTime; // 从小鸟开始飞行时，障碍物被创建出来的时间点
        public float posInY; // 障碍物中心点在Y轴上的位置
        public float height = 300f; // 障碍物上下之间的空隙
    }
}