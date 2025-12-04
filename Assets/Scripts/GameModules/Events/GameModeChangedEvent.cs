using Infra.Event;
using Infra.GameMode;

namespace GameModules.Events
{
    public class GameModeChangedEvent : IEvent
    {
        public string Name => GetType().Name;
        public IEventArg EventArgs { get; set; }
    }

    public class GameModeChangedEventArgs : IEventArg
    {
        public GameModeType PreviousMode { get; set; }
        public GameModeType NewMode { get; set; }
    }
}