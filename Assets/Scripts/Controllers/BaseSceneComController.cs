using UnityEngine;

public class BaseSceneComController : MonoBehaviour, ISceneComController, IRecycle
{
    private bool _isMoving;
    private float _speed;
    private Camera _camera;
    private RectTransform _rectTransform;

    public void StartMove(float moveSpeed)
    {
        _speed = moveSpeed;
        _isMoving = true;
    }

    public void StopMove()
    {
        _isMoving = false;
    }

    public void OnRecycle()
    {
        StopMove();
    }

    public void OnUse()
    {
    }

    private void Awake()
    {
        _camera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!_isMoving) return;
        CheckOutScreen();
    }

    private void FixedUpdate()
    {
        if (!_isMoving) return;
        transform.Translate(Vector2.left * (_speed * Time.fixedDeltaTime));
    }

    private void CheckOutScreen()
    {
        // 将物体的世界坐标转换为视口坐标
        // 视口坐标中，X和Y在[0,1]范围内表示在屏幕内，之外表示在屏幕外。
        var maxPoint = _rectTransform.rect.max;
        var worldPos = _rectTransform.TransformPoint(maxPoint);
        var viewportPos = RectTransformUtility.WorldToScreenPoint(_camera, worldPos);

        // 如果物体的X坐标小于0（完全离开屏幕左侧），并且还在向左移动...
        if (!(viewportPos.x < -0.5f)) return; // 使用-0.5是为了确保物体完全离开屏幕再加一个缓冲

        _isMoving = false;
        PipeSpawner.Instance.ReturnPipe(this);
    }
}