// ViewModel核心接口
using System;
using System.Collections.Generic;

public interface IViewModel : IDisposable
{
    string ViewModelName { get; }
    bool IsInitialized { get; }
    void Initialize();
}