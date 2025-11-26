// 非泛型基础接口

using System;

public interface IObservable
{
    event Action<object> OnValueChanged;
    object Value { get; }
    Type ValueType { get; }
    void SetValue(object value);
}

// 泛型接口（继承基础接口）
public interface IObservable<T> : IObservable
{
    new event Action<T> OnValueChanged;
    new T Value { get; }
    void SetValue(T value);
}