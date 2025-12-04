using UnityEngine;

namespace Infra
{
    /// <summary>
    /// Unity MonoBehaviour 泛型单例
    /// 自动创建游戏对象并附加组件
    /// </summary>
    /// <typeparam name="T">继承MonoBehaviour的单例类型</typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;
        protected static bool _isApplicationQuitting = false;
    
        public static T Instance
        {
            get
            {
                if (_isApplicationQuitting)
                {
                    Debug.LogWarning($"[MonoSingleton] 应用程序正在退出，无法创建 {typeof(T)} 实例");
                    return null;
                }
            
                if (_instance != null) return _instance;
                
                // 在场景中查找现有实例
                _instance = FindFirstObjectByType<T>();
                if (_instance != null) return _instance;
                
                // 创建新的游戏对象
                var singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<T>();
                singletonObject.name = $"[MonoSingleton] {typeof(T).Name}";
                        
                // 如果不是编辑器模式，设置为跨场景不销毁
                if (Application.isPlaying)
                {
                    DontDestroyOnLoad(singletonObject);
                }
                        
                Debug.Log($"[MonoSingleton] 创建 {typeof(T)} 单例实例");
                return _instance;
            }
        }
    
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                if (Application.isPlaying)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else if (_instance != this)
            {
                Debug.LogWarning($"[MonoSingleton] 场景中存在多个 {typeof(T)} 实例，销毁重复实例");
                Destroy(gameObject);
                return;
            }
        
            OnInitialize();
        }

        private void Update()
        {
            OnUpdate();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate();
        }
    
        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    
        private void OnApplicationQuit()
        {
            _isApplicationQuitting = true;
        }
    
        /// <summary>
        /// 初始化方法，在Awake中自动调用
        /// </summary>
        protected virtual void OnInitialize() { }
    
        protected virtual void OnUpdate() { }
    
        protected virtual void OnFixedUpdate() { }
    }
}