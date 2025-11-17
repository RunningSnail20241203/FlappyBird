using System;


public class BaseCommand : ICommand
{
    public virtual string Name { get; }
    public virtual BaseCommandArgs Args { get; }
}

public class BaseCommandArgs
{
    
}