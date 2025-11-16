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

    private void Awake()
    {
        transform.position = birdBirthPoint.position;
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

    private void Jump()
    {
        rb.velocity = new Vector2(0, jumpVelocity);
    }
}