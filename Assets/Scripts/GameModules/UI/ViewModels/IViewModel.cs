// ViewModel核心接口

using System;

namespace GameModules.UI.ViewModels
{
    public interface IViewModel : IDisposable
    {
        string ViewModelName { get; }
        bool IsInitialized { get; }
        void Initialize();
    }
}