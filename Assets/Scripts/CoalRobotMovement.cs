using UnityEngine;

public class CoalRobotMovement : MonoBehaviour
{
    public float moveSpeed;
    private float tolerance;
    private bool unchosen;
    public Rigidbody2D rb;
    private Vector2 start;
    private Vector2 moveDirection;
    private float changer;

    SpriteRenderer spriteRenderer;
   
    void Start()
    {
       changer = 1;
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
        if (unchosen == false)
        {
        Move();
        }

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
        if (changer == 2)
            {
            moveDirection *= -1;
            changer = 0;
            }
        start = transform.position;
        float tempX = moveDirection.x;
        moveDirection.x = moveDirection.y;
        moveDirection.y = tempX;
        changer += 1;

    }
    void explosion()
    {
        Destroy(gameObject);
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
            resetMove();
        }
        else if (Mathf.Abs(start.x - 3f - Current.x) < tolerance)
        {
            resetMove();
        }
        else if (Mathf.Abs(start.y - 3f - Current.y) < tolerance)
        {
            resetMove();
        }
        //Explosion
        if (Mathf.Round(Current.x) == 20) 
        {
            
            explosion();
        }
        else if (Mathf.Round(Current.y) == 10)
        {
            explosion();


        }
        else if (Mathf.Round(Current.x) == -1)
        {
            explosion();


        }
        else if (Mathf.Round(Current.y) == -1)
        {
            explosion();


        }




    }
    
}
