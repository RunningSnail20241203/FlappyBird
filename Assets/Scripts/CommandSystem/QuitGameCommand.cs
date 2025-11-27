public struct QuitGameCommand : ICommand
{
    public string Name => GetType().Name;
    public ICommandArg Args { get; set; }
}