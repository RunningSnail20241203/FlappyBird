public class AddScoreCommand : CommandBase
{
}

public class AddScoreArgs : BaseCommandArgs
{
    public string Target { get; set; }
    public int Score { get; set; }
}