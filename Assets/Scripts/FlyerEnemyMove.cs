using UnityEngine;

public class FlyerEnemyMove : MonoBehaviour
{
    private PlayerController thePlayer;

    public float moveSpeed;

    public float playerRange;

    public LayerMask playerLayer;

    public bool playerInRange;
    public bool facingAway;

    public bool followOnLookAway;
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);
        
        if (!followOnLookAway)
        {
            // If the player is in range
            if (playerInRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
                return;
            }
        }
        // Determine if facing away
        if ((thePlayer.transform.position.x < transform.position.x && thePlayer.transform.localScale.x < 0) || (thePlayer.transform.position.x > transform.position.x && thePlayer.transform.localScale.x > 0))
        {
            facingAway = true;
        }
        else
        {
            facingAway = false;
        }
        
        // If the player is in range
        if (playerInRange && facingAway)
        {
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerRange);
    }
}
