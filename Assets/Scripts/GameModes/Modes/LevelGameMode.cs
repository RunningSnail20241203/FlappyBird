using UnityEngine;

public class LevelGameMode : GameModeBase
{
    public override GameModeType ModeType => GameModeType.Level;

    public override void SetGameModeData(IGameModeArg arg)
    {
        base.SetGameModeData(arg);
        
        if (arg is LevelGameData levelData)
        {
            Debug.Log($"设置关卡ID: {levelData.LevelId}");
            // 可以在这里处理关卡ID
        }
    }
}