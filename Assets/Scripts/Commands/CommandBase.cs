using System;


public class CommandBase : ICommand
{
    public virtual string Name { get; }
    public virtual BaseCommandArgs Args { get; }
}

public class BaseCommandArgs
{
    
}