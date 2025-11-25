using UnityEngine;

[CreateAssetMenu(fileName = "GlobalConfig", menuName = "Configs/GlobalConfig")]
public class GlobalConfig : ScriptableObject
{
    public float pipeMoveSpeed = 200f;
    public float spawnInterval = 2f;
    public float spawnXPosition = 1100f;
    public float pipeTriggerOffsetX = 100f;
    public Vector2 pipeIntervalY = new(350f, 600f);
    public Vector2 pipeCenterY = new(-150f, 150f);
    public float centerOffset = 50f;
    public GameObject pipePrefab;
    public GameObject pipeTriggerPrefab;
}
