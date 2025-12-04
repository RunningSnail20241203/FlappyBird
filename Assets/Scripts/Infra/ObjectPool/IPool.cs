namespace Infra.ObjectPool
{
    /// <summary>
    /// 对象池接口
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public interface IPool<T> where T : class
    {
        T Get();
        void Return(T obj);
        void Clear();
        int CountInactive { get; }
        int CountActive { get; }
    }
}