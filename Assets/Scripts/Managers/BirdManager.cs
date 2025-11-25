using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoSingleton<BirdManager>
{
    public List<BirdController> Birds { get; } = new();
    
    private const string BirdControllerTag = "BirdController";

    public BirdController MyBird => Birds[0];

    public void InitializeBirds()
    {
        
    }
    
    protected override void OnInitialize()
    {
        base.OnInitialize();

        // 后续要重构，现在是直接获取所有的小鸟，应该由服务器下发小鸟的位置，然后动态创建小鸟，并设置位置
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