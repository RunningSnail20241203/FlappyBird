using DefaultNamespace;
using UnityEngine;

public class MoveToLeft : MonoBehaviour
{
    [SerializeField] private GlobalConfig globalConfig;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * (globalConfig.pipeMoveSpeed * Time.fixedDeltaTime));
    }
}
