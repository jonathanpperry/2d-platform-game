using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float moveVelocity;
    public float jumpHeight;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    private bool doubleJumped;

    private Animator anim;

    public Transform firePoint;
    public GameObject ninjaStar;

    public float shotDelay;
    private float shotDelayCounter;

    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

    private Rigidbody2D myrigidbody2D;

    public bool onLadder;

    public float ClimbSpeed;

    private float climbVelocity;
    private float gravityStore;

    void Start()
    {
        anim = GetComponent<Animator>();
        myrigidbody2D = GetComponent<Rigidbody2D>();
        // Set the gravity
        gravityStore = myrigidbody2D.gravityScale;
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    void Update()
    {
        if (grounded)
            doubleJumped = false;

        anim.SetBool("Grounded", grounded);
        
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetButtonDown("Jump") && grounded)
        {
            // Make it jump!
            Jump();
        }

        if (Input.GetButtonDown("Jump") && !doubleJumped && !grounded)
        {
            Jump();
            doubleJumped = true;
        }

        // Get movement from horizontal axis
        Move(Input.GetAxisRaw("Horizontal"));
#endif
        
        if (knockbackCount <= 0)
        {
            myrigidbody2D.velocity = new Vector2(moveVelocity, myrigidbody2D.velocity.y);
        }
        else
        {
            if (knockFromRight)
                myrigidbody2D.velocity = new Vector2(-knockback, knockback);
            if (!knockFromRight)
                myrigidbody2D.velocity = new Vector2(knockback, knockback);
            knockbackCount -= Time.deltaTime;
        }

        // Set animation speed float value to the value of the players horiz velocity
        anim.SetFloat("Speed", Mathf.Abs(myrigidbody2D.velocity.x));

        if (myrigidbody2D.velocity.x > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (myrigidbody2D.velocity.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        
#if UNITY_STANDALONE || UNITY_WEBPLAYER

        if (Input.GetButtonDown("Fire1"))
        {
            FireStar();
            shotDelayCounter = shotDelay;
        }

        if (Input.GetButton("Fire1"))
        {
            shotDelayCounter -= Time.deltaTime;

            if (shotDelayCounter <= 0)
            {
                shotDelayCounter = shotDelay;
                FireStar();
            }
        }

        if (anim.GetBool("Sword"))
            ResetSword();

        if (Input.GetButtonDown("Fire2"))
        {
            Sword();
        }
        
#endif
        // If on ladder
        if (onLadder)
        {
            // Zero the gravity
            myrigidbody2D.gravityScale = 0f;

            climbVelocity = ClimbSpeed * Input.GetAxisRaw("Vertical");
            myrigidbody2D.velocity = new Vector2(myrigidbody2D.velocity.x, climbVelocity);
        }
        if (!onLadder)
        {
            myrigidbody2D.gravityScale = gravityStore;

        }
    }

    public void Move(float moveInput)
    {
        moveVelocity = moveSpeed * moveInput;
    }

    public void FireStar()
    {
        Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
    }

    public void Sword()
    {
        anim.SetBool("Sword", true);
    }

    public void ResetSword()
    {
        anim.SetBool("Sword", false);

    }

    public void Jump()
    {
        myrigidbody2D.velocity = new Vector2(myrigidbody2D.velocity.x, jumpHeight);
        if (grounded)
        {
            myrigidbody2D.velocity = new Vector2(myrigidbody2D.velocity.x, jumpHeight);        }

        if (!doubleJumped && !grounded)
        {
            myrigidbody2D.velocity = new Vector2(myrigidbody2D.velocity.x, jumpHeight);
            doubleJumped = true;
        }
    } 
    void OnCollisionEnter2D(Collision2D other)
    { 
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }
    
    void OnCollisionExit2D(Collision2D other)
    { 
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

}
