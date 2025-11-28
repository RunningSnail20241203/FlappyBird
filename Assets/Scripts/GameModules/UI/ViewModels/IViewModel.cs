// ViewModel核心接口
using System;

public interface IViewModel : IDisposable
{
    string ViewModelName { get; }
    bool IsInitialized { get; }
    void Initialize();
}