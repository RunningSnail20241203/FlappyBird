using Infra.Event;

namespace GameModules.Events
{
    /// <summary>
    /// 关卡完成事件
    /// </summary>
    public class LevelCompletedEvent : IEvent
    {
        public string Name { get; }
        public IEventArg EventArgs { get; set; }
    }

    public class LevelCompletedEventArgs : IEventArg
    {
        public int LevelId { get; set; }
        public int Stars { get; set; }
        public bool IsSuccess { get; set; }
    }
}