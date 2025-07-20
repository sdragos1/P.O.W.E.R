using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;

    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (moveDirection.x == -1)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveDirection.x == 1)
        {
            spriteRenderer.flipX = false;
        }
            ProcessInput();
        
    }
    void FixedUpdate()
    {
        Move();
    }
    void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }
    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
