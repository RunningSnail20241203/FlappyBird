using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BirdController : MonoBehaviour, IController
{
    [SerializeField] private float jumpVelocity = 5f;
    [SerializeField] private Transform birdBirthPoint;
    private Rigidbody2D _rb;
    private BirdTrajectoryData _currentTrajectory;
    private int _logicFrame;
    private bool _jumpAtThisFrame;
    private bool _canJump = true;
    private Vector2 _oldVelocity;
    private PointerEventData _eventData;
    private int _uiLayer;

    private const string CollisionTag = "Obstacle";
    private const string ScoreTriggerTag = "ScoreTrigger";

    private readonly List<string> _jumpSounds = new()
    {
        "impactSoft_heavy_000",
        "impactSoft_heavy_001",
        "impactSoft_heavy_002",
        "impactSoft_heavy_003",
        "impactSoft_heavy_004",
    };

    public void ResetBird()
    {
        transform.position = birdBirthPoint.position;
        _rb.gravityScale = 0;
        _rb.velocity = Vector2.zero;
        _oldVelocity = Vector2.zero;
        _canJump = false;
    }

    public void StartBird()
    {
        _rb.velocity = _oldVelocity;
        _oldVelocity = Vector2.zero;

        _rb.gravityScale = 100;
        _canJump = true;
    }

    public void PauseBird()
    {
        if (_oldVelocity == Vector2.zero) _oldVelocity = _rb.velocity;
        _rb.gravityScale = 0;
        _rb.velocity = Vector2.zero;
        _canJump = false;
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(0, jumpVelocity);
        var audioName = _jumpSounds[Random.Range(0, _jumpSounds.Count)];
        AudioManager.Instance.PlaySound(audioName);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _eventData = new PointerEventData(EventSystem.current);
        _uiLayer = LayerMask.NameToLayer("UI");
        ResetBird();

        // _currentTrajectory = new BirdTrajectoryData
        // {
        //     trajectoryId = DateTime.UtcNow.Ticks
        // };
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_canJump) return;
        // 优先使用触摸输入
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
        
            if (touch.phase == TouchPhase.Began)
            {
                if (IsPointerOverUIElement(touch.position)) return;
                _jumpAtThisFrame = true;
            }
        }else if(Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUIElement(Input.mousePosition)) return;
            _jumpAtThisFrame = true;
        }
     
        // _currentTrajectory.inputEvents.Add(new InputEvent
        // {
        //     timeSinceStart = _logicFrame
        // });
        // _jumpQueue.Enqueue(_logicFrame);
        // Debug.Log($"Jump at logicFrame:{_logicFrame}");
    }

    private void FixedUpdate()
    {
        // 目前先设计成，只接受最近的一次输入
        if (_jumpAtThisFrame)
        {
            _jumpAtThisFrame = false;
            Jump();
            Debug.Log($"Jump at logicFrame:{_logicFrame}");
        }

        _logicFrame += 1;
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     CheckCollision(other.gameObject);
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckCollision(other.gameObject);
    }

    private void CheckCollision(GameObject other)
    {
        if (other.CompareTag(CollisionTag))
        {
            GameStateManager.Instance.AddCommand(new GameOverCommand());
        }
        else if (other.CompareTag(ScoreTriggerTag))
        {
            GameStateManager.Instance.AddCommand(new AddScoreCommand
                { Args = new AddScoreArgs { Score = 1, Target = name } });
        }
    }

    private bool IsPointerOverUIElement(Vector2 position)
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults(position));
    }

    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaycastResults)
    {
        return eventSystemRaycastResults.Any(raycastResult => raycastResult.gameObject.layer == _uiLayer);
    }

    private List<RaycastResult> GetEventSystemRaycastResults(Vector2 position)
    {
        _eventData.position = position;
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventData, raycastResults);
        return raycastResults;
    }
}