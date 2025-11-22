using UnityEditor;
using UnityEngine;

public class CreateConfigSo : EditorWindow
{
    [MenuItem("Tools/Create Config")]
    public static void ShowWindow()
    {
        GetWindow<CreateConfigSo>("Create Config");
    }
    
    private void OnGUI()
    {
        if (GUILayout.Button("Create ViewModel Scope Config"))
            CreateConfig<ViewModelScopeConfig>();
    }
    
    private static void CreateConfig<T>() where T : ScriptableObject
    {
        var config = CreateInstance<T>();
        
        // 自动设置文件名
        var path = $"Assets/Configs/{typeof(T).Name}.asset";

        if (string.IsNullOrEmpty(path)) return;
        AssetDatabase.CreateAsset(config, path);
        AssetDatabase.SaveAssets();
        Selection.activeObject = config;
    }
}