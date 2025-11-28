using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class ViewModelScope : ConfigBase
{
    [Serializable]
    public class ViewModelLifetime
    {
        // 改为 string 类型来存储类型名称
        public string viewModelTypeName;
        public ViewModelContainer.LifetimeScope lifetime = ViewModelContainer.LifetimeScope.Scene;

        // 提供一个属性来在运行时获取 Type 对象
        public Type ViewModelType
        {
            get => string.IsNullOrEmpty(viewModelTypeName) ? null : Type.GetType(viewModelTypeName);
            set => viewModelTypeName = value?.AssemblyQualifiedName;
        }
    }

    public List<ViewModelLifetime> viewModels = new();
}