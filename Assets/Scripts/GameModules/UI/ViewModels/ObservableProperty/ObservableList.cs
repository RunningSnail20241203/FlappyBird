using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace GameModules.UI.ViewModels.ObservableProperty
{
    public class ObservableList<T> : IObservable<List<T>>
    {
        public event Action<List<T>> OnValueChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        
        public List<T> Value { get; }

        public Type ValueType => typeof(List<T>);
        public int Count => Value.Count;
        
        
        public void SetValue(List<T> value)
        {
            var oldValue = new List<T>(Value);
            Value.Clear();
            Value.AddRange(value);
            
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Replace, value, oldValue, 0));
            RaiseValueChanged();
        }

        public void SetValue(object value)
        {
            SetValue((List<T>)value);
        }

        // 构造函数
        public ObservableList()
        {
            Value = new List<T>();
        }

        public ObservableList(IEnumerable<T> collection)
        {
            Value = new List<T>(collection);
        }

        public ObservableList(int capacity)
        {
            Value = new List<T>(capacity);
        }

        // 主要属性
        public T this[int index]
        {
            get => Value[index];
            set
            {
                var oldItem = Value[index];
                if (EqualityComparer<T>.Default.Equals(oldItem, value)) return;

                Value[index] = value;
                RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Replace, value, oldItem, index));
                RaiseValueChanged();
            }
        }

        // 集合操作方法
        public void Add(T item)
        {
            Value.Add(item);
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, item, Value.Count - 1));
            RaiseValueChanged();
        }

        public void AddRange(IEnumerable<T> items)
        {
            var newItems = new List<T>(items);
            if (newItems.Count == 0) return;

            var startIndex = Value.Count;
            Value.AddRange(newItems);
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, newItems, startIndex));
            RaiseValueChanged();
        }

        public bool Remove(T item)
        {
            var index = Value.IndexOf(item);
            if (index < 0) return false;
            
            RemoveAt(index);
            return true;

        }

        public void RemoveAt(int index)
        {
            var removedItem = Value[index];
            Value.RemoveAt(index);
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Remove, removedItem, index));
            RaiseValueChanged();
        }

        public void Clear()
        {
            if (Value.Count == 0) return;

            Value.Clear();
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Reset));
            RaiseValueChanged();
        }

        public bool Contains(T item) => Value.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => Value.CopyTo(array, arrayIndex);

        public int IndexOf(T item) => Value.IndexOf(item);

        public void Insert(int index, T item)
        {
            Value.Insert(index, item);
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, item, index));
            RaiseValueChanged();
        }

        // 转换为数组
        public T[] ToArray() => Value.ToArray();

        // 查找方法
        public T Find(Predicate<T> match) => Value.Find(match);
        public List<T> FindAll(Predicate<T> match) => Value.FindAll(match);

        // 排序方法
        public void Sort() => Value.Sort();
        public void Sort(Comparison<T> comparison) => Value.Sort(comparison);
        public void Sort(IComparer<T> comparer) => Value.Sort(comparer);

        public override string ToString()
        {
            return $"ObservableList<{typeof(T).Name}> Count = {Count}";
        }

        // 通知方法
        private void RaiseValueChanged()
        {
            OnValueChanged?.Invoke(Value);
            _objectValueChanged?.Invoke(Value);
        }

        private void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }


        object IObservable.Value => Value;// 非泛型属性（显式实现）
        private Action<object> _objectValueChanged;
        event Action<object> IObservable.OnValueChanged // 非泛型事件（显式实现）
        {
            add => _objectValueChanged += value;
            remove => _objectValueChanged -= value;
        }
       
    }
}