// ViewModel核心接口

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public interface IViewModel : IDisposable
{
    string ViewModelName { get; }
    bool IsInitialized { get; }
    void Initialize();
    void UnbindAll();
}

// 支持属性通知的接口
public interface INotifyPropertyChanged
{
    event PropertyChangedEventHandler PropertyChanged;
    void RaisePropertyChanged([CallerMemberName] string propertyName = "");
}


// 支持命令的接口
public interface ICommandProvider
{
    ICommand GetCommand(string commandName);
    void RegisterCommand(string commandName, ICommand command);
}