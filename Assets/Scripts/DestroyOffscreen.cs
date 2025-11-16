using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    private Camera _camera;
    private RectTransform  _rectTransform;
    // Start is called before the first frame update
    private void Start()
    {
        _camera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
    }

    // 更可靠的方法是手动检测
    private void Update()
    {
        // 将物体的世界坐标转换为视口坐标
        // 视口坐标中，X和Y在[0,1]范围内表示在屏幕内，之外表示在屏幕外。
        var maxPoint = _rectTransform.rect.max;
        var worldPos = _rectTransform.TransformPoint(maxPoint);
        var viewportPos =  RectTransformUtility.WorldToScreenPoint(_camera, worldPos);

        // 如果物体的X坐标小于0（完全离开屏幕左侧），并且还在向左移动...
        if (viewportPos.x < -0.5f) // 使用-0.5是为了确保物体完全离开屏幕再加一个缓冲
        {
            // 那么销毁它或将其放回对象池
            Destroy(gameObject);

            // --- 强烈推荐使用对象池 ---
            // ObjectPool.Instance.ReturnToPool(gameObject);
        }
    }
}
