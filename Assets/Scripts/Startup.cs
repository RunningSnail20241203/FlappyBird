using UnityEngine;

public class Startup : MonoBehaviour
{
    private void Awake()
    {
        GameStateManager.Instance.GoToMenu();
    }
}