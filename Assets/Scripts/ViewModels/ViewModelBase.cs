using System.Collections.Generic;

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
        _observableProperties?.Clear();
    }
    
    public virtual void Dispose()
    {
        UnbindAll();
    }
   
    protected bool SetProperty<T>(ref T field, T value, string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
            
        field = value;
        return true;
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
    private readonly Dictionary<string, object> _observableProperties = new();
    
    protected ObservableWithT<T> CreateObservable<T>(T initialValue = default, string propertyName = "")
    {
        var observable = new ObservableWithT<T>(initialValue);
        _observableProperties[propertyName] = observable;
        return observable;
    }
    
    // === 抽象方法 ===
    protected virtual void InitializeCommands() { }
    protected virtual void InitializeProperties() { }
}