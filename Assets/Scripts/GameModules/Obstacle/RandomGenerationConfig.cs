using UnityEngine;

namespace GameModules.Obstacle
{
    public class RandomGenerationConfig
    {
        public float GenerateInterval { get; set; } // 生成障碍物的时间间隔
        public Vector2 YRange1 { get; set; } // 障碍物中心点的高度范围
        public Vector2 YRange2 { get; set; } // 障碍物上下两块的间距范围
        public float XSpawn { get; set; } // 障碍物生成位置的横坐标
        public Vector2 MoveSpeed { get; set; } // 障碍物移动速度
    }
}