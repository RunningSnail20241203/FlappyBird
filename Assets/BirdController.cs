using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float jumpVelocity = 5f;
    [SerializeField] private Transform birdBirthPoint;
    private Rigidbody2D rb;

    private void Awake()
    {
        transform.position = birdBirthPoint.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(0, jumpVelocity);
        }
    }
}