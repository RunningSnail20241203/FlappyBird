using System;
using System.Collections.Generic;

public class Observable<T> : IObservable<T>
{
    private T _value;
    
    // 泛型事件
    public event Action<T> OnValueChanged;
    
    // 非泛型事件（显式实现）
    event Action<object> IObservable.OnValueChanged
    {
        add => _objectValueChanged += value;
        remove => _objectValueChanged -= value;
    }
    private Action<object> _objectValueChanged;
    
    // 泛型属性
    public T Value
    {
        get => _value;
        set
        {
            if (EqualityComparer<T>.Default.Equals(_value, value)) return;
            _value = value;
            OnValueChanged?.Invoke(value);
            _objectValueChanged?.Invoke(value);
        }
    }
    
    // 非泛型属性（显式实现）
    object IObservable.Value
    {
        get => Value;
        set => Value = (T)value;
    }
    
    public Type ValueType => typeof(T);
    
    public Observable(T initialValue = default)
    {
        _value = initialValue;
    }
}