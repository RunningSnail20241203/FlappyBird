using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoSingleton<BirdManager>
{
    public List<BirdController> Birds { get; } = new();
    
    private const string BirdControllerTag = "BirdController";

    protected override void OnInitialize()
    {
        base.OnInitialize();

        var objs = GameObject.FindGameObjectsWithTag(BirdControllerTag);
        foreach (var obj in objs)
        {
            var controller = obj.GetComponent<BirdController>();
            if (controller != null)
            {
                Birds.Add(controller);
            }
        }
    }
}