using Types;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CoalRobotMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    private const float sideLength = 3f;  // 4 tiles = move 3 units per side

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2[] directions = new Vector2[]
    {
        Vector2.right,
        Vector2.up,
        Vector2.left,
        Vector2.down
    };
    private int directionIndex = 0;
    private Vector2 pivot;  // where this leg started

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        pivot = rb.position;
    }

    void Update()
    {
        // Flip sprite based on horizontal direction
        var dir = directions[directionIndex];
        spriteRenderer.flipX = dir.x < 0;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.CurrentPhase != GamePhase.Execute)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        MoveAlongSquareWithBounce();
    }

    private void MoveAlongSquareWithBounce()
    {
        Vector2 dir = directions[directionIndex];
        Vector2 nextStep = rb.position + dir * moveSpeed * Time.fixedDeltaTime;

        // 1) Boundaries of your grid
        float minX = 0f, minY = 0f;
        float maxX = GameManager.Instance.GridWidth  - 1;
        float maxY = GameManager.Instance.GridHeight - 1;

        // 2) If next step goes out of the main grid bounds, bounce:
        if (nextStep.x < minX || nextStep.x > maxX ||
            nextStep.y < minY || nextStep.y > maxY)
        {
            // Reverse direction (turn 180°)
            directionIndex = (directionIndex + 2) % directions.Length;
            pivot = rb.position;     // start new leg from here
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // 3) No bounce — proceed as normal
        rb.linearVelocity = dir * moveSpeed;

        // 4) Check if we've completed this side
        float traveled = Vector2.Dot(rb.position - pivot, dir);
        if (traveled >= sideLength - 0.01f)
        {
            pivot += dir * sideLength;
            rb.position = pivot;  // snap to avoid drift
            directionIndex = (directionIndex + 1) % directions.Length;
        }
    }
}
