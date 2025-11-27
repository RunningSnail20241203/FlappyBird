public struct ReStartGameCommand : ICommand
{
    public string Name => GetType().Name;
    public ICommandArg Args { get; set; }
}