using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

    public virtual void UnbindAll()
    {
        _commands?.Clear();
        _observables?.Clear();
    }

    public virtual void Dispose()
    {
        UnbindAll();
        OnDispose();
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

    protected Observable<T> CreateObservable<T>(T initialValue = default, [CallerMemberName] string propertyName = "")
    {
        var observable = new Observable<T>(initialValue);
        _observables[propertyName] = observable;
        return observable;
    }
    
    // 通过名称获取可观察属性
    public IObservable GetObservable(string propertyName)
    {
        return _observables.GetValueOrDefault(propertyName);
    }
    
    // 通过名称获取泛型可观察属性
    public IObservable<T> GetObservable<T>(string propertyName)
    {
        return GetObservable(propertyName) as IObservable<T>;
    }
    
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
        if (Equals(field, value))
            return false;
            
        field = value;
        return true;
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