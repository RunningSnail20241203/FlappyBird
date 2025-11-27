public class StartGameCommand : ICommand
{
    public string Name => GetType().Name;
    public ICommandArg Args { get; set; }
}