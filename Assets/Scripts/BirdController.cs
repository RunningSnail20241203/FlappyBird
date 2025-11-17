using System;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float jumpVelocity = 5f;
    [SerializeField] private Transform birdBirthPoint;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rb;
    private BirdTrajectoryData _currentTrajectory;
    private int _logicFrame = 0;
    private Queue<int> _jumpQueue = new();
    private bool _jumpAtThisFrame = false;

    public bool IsDead { get; set; }
    public bool IsIdle { get; set; }

    public void ResetBird()
    {
        transform.position = birdBirthPoint.position;
        IsIdle = true;
        IsDead = false;
    }

    public void StartBird()
    {
        IsIdle = false;
        IsDead = false;
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(0, jumpVelocity);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        // _currentTrajectory = new BirdTrajectoryData
        // {
        //     trajectoryId = DateTime.UtcNow.Ticks
        // };
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsIdle) return;
        if (IsDead) return;
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
        IsDead = true;
    }
}