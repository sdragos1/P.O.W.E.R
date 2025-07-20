using UnityEngine;

public class CoalRobotMovement : MonoBehaviour
{
    public float moveSpeed;
    private float tolerance;
    private bool unchosen;
    public Rigidbody2D rb;
    private Vector2 start;
    private Vector2 moveDirection;

    SpriteRenderer spriteRenderer;
    
    void Start()
    {
        

        unchosen = true;
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

        if (unchosen == true)
        {
            if (Input.GetKeyDown("h"))
            {
                moveDirection.y = 0;
                moveDirection.x = 1;
                unchosen = false;
                start = transform.position;

            }
            else if (Input.GetKeyDown("v"))
            {
                moveDirection.x = 0;
                moveDirection.y = 1;
                unchosen = false;
                start = transform.position;

            }
        }
    }
    void resetMove()
    {
        start = transform.position;
        float tempX = moveDirection.x;
        moveDirection.x = moveDirection.y;
        moveDirection.y = tempX;
    }
    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed,moveDirection.y * moveSpeed);
        Vector2 Current = transform.position;
        tolerance = 0.01f; 
        if (Mathf.Abs(start.x + 3f - Current.x) < tolerance)
          {
            
            resetMove();
          }
       else if (Mathf.Abs(start.y + 3f - Current.y) < tolerance)
        {

            moveDirection *= -1;
            resetMove();
        }
        else if (Mathf.Abs(start.x - 3f - Current.x) < tolerance)
        {
            resetMove();
        }
        else if (Mathf.Abs(start.y - 3f - Current.y) < tolerance)
        {

            moveDirection *= -1;
            resetMove();
        }






    }
    
}
