using System;
using Types;
using UnityEngine;

public class SolarRobotMovement : MonoBehaviour
{
    public float moveSpeed = 0.4f;
    public float tolerance;
    public Rigidbody2D rb;
    private float Timer;
    private Vector2 moveDirection;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (RobotSelectionManager.Instance.RobotOrientation == "horizontal")
        {
            moveDirection = new Vector2(1, 0);
        }
        else if (RobotSelectionManager.Instance.RobotOrientation == "vertical")
        {
            moveDirection = new Vector2(0, 1);
        }
        else
        {
            moveDirection = new Vector2(1, 0);
        }
    }

    void Start()
    {
        Timer = 60f;

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
    }

    void FixedUpdate()
    {
        Move();
    }

    void resetMove()
    {
        moveDirection *= -1;
        Timer = 0f;
    }

    void Move()
    {
        if (GameManager.Instance.CurrentPhase != GamePhase.Execute)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        Vector2 Bar = transform.position;
        tolerance = 0.01f;
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