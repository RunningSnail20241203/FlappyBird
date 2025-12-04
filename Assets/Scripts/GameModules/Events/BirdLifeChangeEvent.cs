using Infra.Event;

namespace GameModules.Events
{
    public class BirdLifeChangeEvent : EventBase
    {
    
    }

    public class BirdLifeChangeEventArgs : IEventArg
    {
        public string BirdId;
        public int ChangeCount;
        public int NewLife;
    }
}