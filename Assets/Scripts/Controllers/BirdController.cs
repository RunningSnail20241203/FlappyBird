using UnityEngine;

public class BirdController : MonoBehaviour, IController
{
    [SerializeField] private float jumpVelocity = 5f;
    [SerializeField] private Transform birdBirthPoint;
    private Rigidbody2D _rb;
    private BirdTrajectoryData _currentTrajectory;
    private int _logicFrame;
    private bool _jumpAtThisFrame;
    private const string CollisionTag = "Obstacle";

    public void ResetBird()
    {
        transform.position = birdBirthPoint.position;
        _rb.gravityScale = 0;
    }

    public void StartBird()
    {
        _rb.gravityScale = 100;
    }

    public void PauseBird()
    {
        _rb.gravityScale = 0;
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(0, jumpVelocity);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        GetComponent<BoxCollider2D>();

        ResetBird();

        // _currentTrajectory = new BirdTrajectoryData
        // {
        //     trajectoryId = DateTime.UtcNow.Ticks
        // };
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        _jumpAtThisFrame = true;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(CollisionTag))
        {
            GameStateManager.Instance.AddCommand(new GameOverCommand());
        }
    }
}