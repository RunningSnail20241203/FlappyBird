public interface ICommand
{
    string Name { get; }
    public ICommandArg Args { get; set; }
}

public interface ICommandArg
{
    
}