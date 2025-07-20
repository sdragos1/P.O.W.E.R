using Types;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    public float moveSpeed;
    public float tolerance;
    public bool unchosen;
    public Rigidbody2D rb;
    private float Timer;
    private Vector2 moveDirection;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        Timer = 60f;

        unchosen = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Timer < 60f)
        {
            Timer += 1;
        }

        if (GameManager.Instance.CurrentPhase != GamePhase.Execute)
        {
            return;
        }

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
        if (GameManager.Instance.CurrentPhase == GamePhase.Execute)
        {
            Move();
        }
    }

    void ProcessInput()
    {
        if (unchosen)
        {
            if (Input.GetKeyDown("h"))
            {
                moveDirection.y = 0;
                moveDirection.x = 1;
                unchosen = false;
            }
            else if (Input.GetKeyDown("v"))
            {
                moveDirection.x = 0;
                moveDirection.y = 1;
                unchosen = false;
            }
        }
    }

    void resetMove()
    {
        moveDirection *= -1;
        Timer = 0f;
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        Vector2 Bar = transform.position;
        tolerance = 0.01f;
        Debug.Log(Timer);
        if (Timer == 60f)
        {
            if (Mathf.Abs(Bar.x - 19f) < tolerance)
            {
                resetMove();
            }
            else if (Mathf.Abs(Bar.y - 9f) < tolerance)
            {
                resetMove();
            }
            else if (Mathf.Abs(Bar.x) < tolerance)
            {
                resetMove();
            }
            else if (Mathf.Abs(Bar.y) < tolerance)
            {
                resetMove();
            }
        }
    }
}