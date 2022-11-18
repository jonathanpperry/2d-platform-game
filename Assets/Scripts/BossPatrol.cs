using UnityEngine;

public class BossPatrol : MonoBehaviour
{
    public float moveSpeed;
    public bool moveRight;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hittingWall;

    private bool notAtEdge;
    public Transform edgeCheck;

    private Rigidbody2D myrigidbody2D;

    private float ySize;

    // Start is called before the first frame update
    void Start()
    {
        // Get rigidbody to increase efficiency
        myrigidbody2D = GetComponent<Rigidbody2D>();
        ySize = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Wall hit
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        if (hittingWall || !notAtEdge)
            moveRight = !moveRight;

        if (moveRight)
        {
            transform.localScale = new Vector3(-ySize, transform.localScale.y, transform.localScale.z);
            myrigidbody2D.velocity = new Vector2(moveSpeed, myrigidbody2D.velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(ySize, transform.localScale.y, transform.localScale.z);
            myrigidbody2D.velocity = new Vector2(-moveSpeed, myrigidbody2D.velocity.y);
        }
    }
}
