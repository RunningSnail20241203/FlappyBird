using System;

public class BirdLifeChangeEvent : EventBase
{
    
}

public class BirdLifeChangeEventArgs : IEventArg
{
    public string BirdId;
    public int ChangeCount;
    public int NewLife;
}