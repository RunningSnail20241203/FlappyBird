using System.Collections.Generic;

[System.Serializable] // 使其可被序列化为JSON，便于存储和传输
public class BirdTrajectoryData
{
    public long trajectoryId; // 轨迹ID，用于匹配
    public float startPosY; // 小鸟的初始Y位置（因为X是固定的）
    public float gameSpeed; // 游戏的移动速度（管道的moveSpeed）
    public List<InputEvent> inputEvents = new List<InputEvent>(); // 输入事件列表
}

[System.Serializable]
public class InputEvent
{
    public int timeSinceStart; // 从游戏开始到此次输入经过的时间
    // 理论上可以有其他输入类型，但Flappy Bird只有跳跃
}