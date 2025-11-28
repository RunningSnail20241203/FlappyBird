using UnityEngine;

public class ChallengeGameMode : GameModeBase
{
    public override GameModeType ModeType => GameModeType.Challenge;
    
    private string _challengeId;
    private int _targetScore;
    private string _randomSeed;

    public override void Initialize()
    {
        // // 初始化挑战相关服务
        // ChallengeService.Instance.Initialize();
    }

    public void SetChallenge(string challengeId, int targetScore, string seed)
    {
        _challengeId = challengeId;
        _targetScore = targetScore;
        _randomSeed = seed;
    }

    public override void Start()
    {
        base.Start();
        // 设置随机种子确保地图一致性
        Random.InitState(_randomSeed.GetHashCode());
        
        // 开始挑战
        StartChallenge();
    }

    private void StartChallenge()
    {
        // 实现挑战模式特定逻辑
        // EventManager.Instance.Subscribe<ScoreChangedEvent>(OnScoreChanged);
    }

    // private void OnScoreChanged(ScoreChangedEvent e)
    // {
    //     if (e.NewScore > _targetScore)
    //     {
    //         // 挑战成功
    //         ChallengeService.Instance.CompleteChallenge(_challengeId, true);
    //         EventManager.Instance.Publish(new ChallengeCompletedEvent 
    //         { 
    //             ChallengeId = _challengeId, 
    //             IsSuccess = true 
    //         });
    //     }
    // }
}