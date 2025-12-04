namespace Infra.ObjectPool
{
    public interface IRecycle
    {
        void OnRecycle();
        void OnUse();
    }
}