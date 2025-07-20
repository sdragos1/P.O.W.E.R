using Types;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class CoalRobotMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private const float sideLength = 3f;    // 3 units per side = 4×4 perimeter
    private const float arriveTolerance = 0.05f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private List<Vector2> waypoints;
    private int index = 0;
    private int stepDir = 1;

    void Start()
    {
        rb             = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        BuildPerimeterWaypoints();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.CurrentPhase != GamePhase.Execute)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        MoveAlongWaypoints();
    }

    private void MoveAlongWaypoints()
    {
        Vector2 target = waypoints[index];
        Vector2 dir    = (target - rb.position).normalized;

        // Flip sprite for left movement
        spriteRenderer.flipX = dir.x < 0;

        rb.linearVelocity = dir * moveSpeed;

        // Arrived?
        if (Vector2.Distance(rb.position, target) < arriveTolerance)
        {
            index += stepDir;

            // Reverse at ends
            if (index >= waypoints.Count)
            {
                index = waypoints.Count - 2; // step back in bounds
                stepDir = -1;
            }
            else if (index < 0)
            {
                index = 1;   // step forward in bounds
                stepDir = 1;
            }
        }
    }

    private void BuildPerimeterWaypoints()
    {
        // Starting pivot = current position at Start
        Vector2 origin = rb.position;
        waypoints = new List<Vector2> { origin };

        // Directions in order: right, up, left, down
        Vector2[] dirs = { Vector2.right, Vector2.up, Vector2.left, Vector2.down };

        Vector2 current = origin;

        foreach (Vector2 d in dirs)
        {
            for (int step = 1; step <= sideLength; step++)
            {
                current += d;
                waypoints.Add(current);
            }
        }
        // Now waypoints contains: origin, R, R, R, U, U, U, L, L, L, D, D, D
        // which is a 12‑point loop. We’ll patrol back and forth along these.
    }
}
