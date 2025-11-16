using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "GlobalConfig", menuName = "Configs/GlobalConfig")]
    public class GlobalConfig : ScriptableObject
    {
        public float pipeMoveSpeed = 200f;
        public GameObject pipePrefab;
    }
}