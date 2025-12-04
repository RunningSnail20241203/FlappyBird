using Infra;

namespace GameModules
{
    public class GlobalLogicFrame : MonoSingleton<GlobalLogicFrame>
    {
        public int FrameCount { get; private set; }
        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            FrameCount += 1;
        }
    }
}