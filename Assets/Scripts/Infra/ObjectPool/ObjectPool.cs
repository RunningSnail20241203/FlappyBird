using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infra.ObjectPool
{
    /// <summary>
    /// 基础泛型对象池
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class ObjectPool<T> : IPool<T> where T : class
    {
        private readonly Stack<T> _pool;
        private readonly Func<T> _createFunc;
        private readonly Action<T> _onGet;
        private readonly Action<T> _onReturn;
        private readonly Action<T> _onDestroy;

        private int _maxSize;
        private int _activeCount;

        public int CountInactive => _pool.Count;
        public int CountActive => _activeCount;
        public int MaxSize => _maxSize;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="createFunc">创建对象的函数</param>
        /// <param name="onGet">获取对象时的回调</param>
        /// <param name="onReturn">归还对象时的回调</param>
        /// <param name="onDestroy">销毁对象时的回调</param>
        /// <param name="initialSize">初始大小</param>
        /// <param name="maxSize">最大大小</param>
        public ObjectPool(
            Func<T> createFunc,
            Action<T> onGet = null,
            Action<T> onReturn = null,
            Action<T> onDestroy = null,
            int initialSize = 10,
            int maxSize = 100)
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            _onGet = onGet;
            _onReturn = onReturn;
            _onDestroy = onDestroy;
            _maxSize = maxSize;
            _activeCount = 0;
            _pool = new Stack<T>(initialSize);

            // 预创建对象
            for (var i = 0; i < initialSize; i++)
            {
                _pool.Push(createFunc());
            }
        }

        public T Get()
        {
            var obj = _pool.Count > 0 ? _pool.Pop() : _createFunc();
            _onGet?.Invoke(obj);
            _activeCount++;

            return obj;
        }

        public void Return(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            _onReturn?.Invoke(obj);

            if (_pool.Count < _maxSize)
            {
                _pool.Push(obj);
            }
            else
            {
                _onDestroy?.Invoke(obj);
            }

            _activeCount = Mathf.Max(0, _activeCount - 1);
        }

        public void Clear()
        {
            while (_pool.Count > 0)
            {
                var obj = _pool.Pop();
                _onDestroy?.Invoke(obj);
            }

            _activeCount = 0;
        }

        public void Resize(int newMaxSize)
        {
            _maxSize = newMaxSize;

            // 如果新的大小小于当前池大小，移除多余对象
            while (_pool.Count > _maxSize)
            {
                var obj = _pool.Pop();
                _onDestroy?.Invoke(obj);
            }
        }
    }
}