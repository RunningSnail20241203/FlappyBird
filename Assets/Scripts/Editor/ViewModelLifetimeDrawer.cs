using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ViewModelScope.ViewModelLifetime))]
    public class ViewModelLifetimeDrawer : PropertyDrawer
    {
        private Type[] _scriptableObjectTypes;
        private string[] _typeDisplayNames;
        private bool _typesCached;

        private void CacheTypes()
        {
            if (_typesCached) return;
        
            _scriptableObjectTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(ViewModelBase)) && !type.IsAbstract)
                .OrderBy(type => type.FullName)
                .ToArray();
            
            _typeDisplayNames = _scriptableObjectTypes
                .Select(type => $"{type.FullName} ({type.Name})")
                .ToArray();
            
            _typesCached = true;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CacheTypes();
        
            // 获取属性 - 现在使用 viewModelTypeName
            var viewModelTypeProperty = property.FindPropertyRelative("viewModelTypeName");
            var lifetimeProperty = property.FindPropertyRelative("lifetime");

            // 检查属性是否找到
            if (viewModelTypeProperty == null)
            {
                EditorGUI.LabelField(position, "Error: viewModelTypeName property not found");
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            var lineHeight = EditorGUIUtility.singleLineHeight;
            var verticalSpacing = EditorGUIUtility.standardVerticalSpacing;

            var typeRect = new Rect(position.x, position.y, position.width, lineHeight);
            var lifetimeRect = new Rect(position.x, position.y + lineHeight + verticalSpacing, position.width, lineHeight);

            // 自定义绘制 viewModelTypeName
            DrawTypeDropdown(typeRect, viewModelTypeProperty, new GUIContent("ViewModel Type"));

            // 使用默认绘制器绘制 lifetime
            EditorGUI.PropertyField(lifetimeRect, lifetimeProperty);

            EditorGUI.EndProperty();
        }

        private void DrawTypeDropdown(Rect position, SerializedProperty property, GUIContent label)
        {
            var currentTypeName = property.stringValue;
            var selectedIndex = 0;

            // 查找当前选择的索引
            if (!string.IsNullOrEmpty(currentTypeName))
            {
                var currentType = Type.GetType(currentTypeName);
                if (currentType != null)
                {
                    var currentDisplayName = $"{currentType.FullName} ({currentType.Name})";
                    selectedIndex = Array.IndexOf(_typeDisplayNames, currentDisplayName);
                    if (selectedIndex == -1) selectedIndex = 0;
                }
            }

            // 显示下拉框
            EditorGUI.BeginChangeCheck();
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, _typeDisplayNames);
            if (EditorGUI.EndChangeCheck() && selectedIndex >= 0 && selectedIndex < _scriptableObjectTypes.Length)
            {
                property.stringValue = _scriptableObjectTypes[selectedIndex].AssemblyQualifiedName;
                Debug.Log($"{property.stringValue}:{ Type.GetType(property.stringValue)}");
            }

            // 显示当前选择的类型信息
            if (!string.IsNullOrEmpty(property.stringValue))
            {
                var selectedType = Type.GetType(property.stringValue);
                if (selectedType != null)
                {
                    // 可选：在字段后面显示类型信息
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
        }
    }
}