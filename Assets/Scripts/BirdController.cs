using System;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float jumpVelocity = 5f;
    [SerializeField] private Transform birdBirthPoint;
    private Rigidbody2D rb;
    private BirdTrajectoryData currentTrajectory;
    private bool jumpInputThisFrame = false;
    private int logicFrame = 0;
    private StateMachine<BirdState> stateMachine;

    public bool IsDead { get; set; }

    public void ResetBird()
    {
        
    }

    public void PlayDeathEffect()
    {
        
    }

    public void EnablePhysics(bool enable)
    {
        
    }

    public void Jump()
    {
        rb.velocity = new Vector2(0, jumpVelocity);
    }    
    
    private void Awake()
    {
        transform.position = birdBirthPoint.position;
        
        stateMachine = new StateMachine<BirdState>();
        stateMachine.AddState(new BirdDeadState(this));
        stateMachine.AddState(new BirdFlyingState(this));
        stateMachine.AddState(new BirdIdleState(this));
        
        // 添加状态过渡
        stateMachine.AddTransition<BirdIdleState, BirdFlyingState>(() => Input.GetKeyDown(KeyCode.Space));
        stateMachine.AddTransition<BirdDeadState, BirdIdleState>(() => Input.GetKeyDown(KeyCode.Escape));
        stateMachine.AddTransition<BirdFlyingState, BirdDeadState>(() => IsDead);
        stateMachine.AddTransition<BirdFlyingState, BirdIdleState>(() => Input.GetKeyDown(KeyCode.Escape));

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentTrajectory = new BirdTrajectoryData
        {
            trajectoryId = DateTime.UtcNow.Ticks
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpInputThisFrame = true;
        }
    }

    private void FixedUpdate()
    {
        logicFrame += 1;
        if (!jumpInputThisFrame) return;
        jumpInputThisFrame = false;

        Jump();
        Debug.Log($"Jump at logicFrame:{logicFrame}");

        currentTrajectory.inputEvents.Add(new InputEvent
        {
            timeSinceStart = logicFrame
        });
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        IsDead = true;
    }
}