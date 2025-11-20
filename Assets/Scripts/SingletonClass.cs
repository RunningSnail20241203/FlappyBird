namespace DefaultNamespace
{
    public class SingletonClass<T> where T : class, new()
    {
        private static T _instance;
        private static readonly object Lock = new();
    
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (Lock)
                {
                    _instance ??= new T();
                }
                return _instance;
            }
        }
    }
}