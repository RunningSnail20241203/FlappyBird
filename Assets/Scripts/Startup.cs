using System.Collections;
using UnityEngine;

public class Startup : MonoBehaviour
{
    private IEnumerator Start()
    {
        
        yield return new WaitUntil(() => ViewModelContainer.Instance.IsValid());
        yield return new WaitUntil(() => PipeSpawner.Instance.IsValid());
        
        GameStateManager.Instance.GoToMenu();
    }
}