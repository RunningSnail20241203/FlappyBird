using UnityEngine;

[CreateAssetMenu(fileName = "GlobalConfig", menuName = "Configs/GlobalConfig")]
public class GlobalConfig : ScriptableObject
{
    public float pipeMoveSpeed = 200f;
    public float spawnInterval = 2f;
    public float spawnXPosition = 1100f;
    public float pipeTriggerOffsetX = 100f;
    public Vector2 upPipeYRange = new(350f, 600f);
    public Vector2 downPipeYRange = new(-600f, -350f);
    public GameObject pipePrefab;
    public GameObject pipeTriggerPrefab;
}
