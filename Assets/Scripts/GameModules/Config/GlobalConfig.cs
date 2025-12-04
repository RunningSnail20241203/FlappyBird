using UnityEngine;

namespace GameModules.Config
{
    [CreateAssetMenu(fileName = "GlobalConfig", menuName = "Configs/GlobalConfig")]
    public class GlobalConfig : ScriptableObject
    {
        public GameObject obstaclePrefab;
    }
}
