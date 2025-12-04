using System.Collections;
using GameModules;
using GameModules.UI.ViewModels;
using UnityEngine;

public class Startup : MonoBehaviour
{
    private IEnumerator Start()
    {
        Application.targetFrameRate = 60;
        
        yield return new WaitUntil(() => ViewModelContainer.Instance.IsValid());
        yield return new WaitUntil(() => ConfigManager.Instance.IsValid());
        yield return new WaitUntil(() => LevelManager.Instance.IsValid());
        
        GameStateManager.Instance.GoToMenu();
    }
}