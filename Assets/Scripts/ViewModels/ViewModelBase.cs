using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class ViewModelBase : IViewModel
{
    // === IViewModel 实现 ===
    public string ViewModelName => GetType().Name;
    public bool IsInitialized { get; private set; }

    public virtual void Initialize()
    {
        if (IsInitialized) return;

        InitializeCommands();
        InitializeProperties();
        IsInitialized = true;
    }

    public virtual void Dispose()
    {
        OnDispose();
        _commands?.Clear();
        _observables?.Clear();
    }

    // === ICommandProvider 实现 ===
    private readonly Dictionary<string, ICommand> _commands = new();

    public ICommand GetCommand(string commandName)
    {
        return _commands.GetValueOrDefault(commandName);
    }

    public void RegisterCommand(string commandName, ICommand command)
    {
        _commands[commandName] = command;
    }

    // === 可观察属性支持 ===
    private readonly Dictionary<string, IObservable> _observables = new();

    // ✅ 统一访问所有Observable
    public IReadOnlyDictionary<string, IObservable> GetAllObservables() => _observables;

    private IObservable<T> CreateObservable<T>(T initialValue = default, [CallerMemberName] string propertyName = "")
    {
        var observable = new Observable<T>(initialValue);
        _observables[propertyName] = observable;
        return observable;
    }

    // 通过名称获取泛型可观察属性
    protected IObservable<T> GetObservable<T>([CallerMemberName] string propertyName = null)
    {
        if (propertyName == null)
        {
            Debug.LogError($"{GetType().Name} Property {typeof(T).Name} name cannot be null.");
            return null;
        }

        if (_observables.TryGetValue(propertyName, out var observable))
        {
            return observable as IObservable<T>;
        }

        return CreateObservable<T>(propertyName: propertyName);
    }

    // === 抽象方法 ===
    protected virtual void InitializeCommands()
    {
    }

    protected virtual void InitializeProperties()
    {
    }

    protected virtual void OnDispose()
    {
    }
}