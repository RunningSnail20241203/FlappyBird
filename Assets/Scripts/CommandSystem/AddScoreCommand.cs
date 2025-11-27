public class AddScoreCommand : ICommand
{
    public string Name => GetType().Name;
    public ICommandArg Args { get; set; }
}

public struct AddScoreArgs : ICommandArg
{
    public string RoleId { get; set; }
    public int Score { get; set; }
}