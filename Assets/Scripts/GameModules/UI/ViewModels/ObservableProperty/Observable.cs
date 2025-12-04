using System;
using System.Collections.Generic;

namespace GameModules.UI.ViewModels
{
    public class Observable<T> : IObservable<T>
    {
        public T Value => _value;// 泛型属性
        public Type ValueType => typeof(T);
        public event Action<T> OnValueChanged;// 泛型事件
        public void SetValue(T value)
        {
            if (EqualityComparer<T>.Default.Equals(_value, value)) return;
            _value = value;
            OnValueChanged?.Invoke(value);
            _objectValueChanged?.Invoke(value);
        }

        public void SetValue(object value)
        {
            SetValue((T)value);
        }

        public Observable(T initialValue = default)
        {
            _value = initialValue;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    
    
        private T _value;
        // 非泛型属性（显式实现）
        object IObservable.Value => _value;

        // 非泛型事件（显式实现）
        private Action<object> _objectValueChanged;
        event Action<object> IObservable.OnValueChanged
        {
            add => _objectValueChanged += value;
            remove => _objectValueChanged -= value;
        }
    }
}