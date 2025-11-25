using System;


public class CommandBase : ICommand
{
    public virtual string Name => GetType().Name;
    public virtual BaseCommandArgs Args { get; set; }
}

public class BaseCommandArgs
{
    
}