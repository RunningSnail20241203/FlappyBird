public struct PauseGameCommand : ICommand
{
    public string Name => GetType().Name;
    public ICommandArg Args { get; set; }
}