using UnityEngine;

public class NinjaStarController : MonoBehaviour
{
    public float speed;

    public PlayerController player;

    public GameObject enemyDeathEffect;

    public GameObject impactEffect;

    public int pointsForKill;

    public float rotationSpeed;
    
    public int damageToGive;

    private Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        myRigidbody2D = GetComponent<Rigidbody2D>();
        
        if (player.transform.localScale.x < 0)
        {
            speed = -speed;
            rotationSpeed = -rotationSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody2D.velocity = new Vector2(speed, myRigidbody2D.velocity.y);

        myRigidbody2D.angularVelocity = rotationSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If it's the enemy
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().GiveDamage(damageToGive);
        }

        if (other.tag == "Boss")
        {
            other.GetComponent<BossHealthManager>().GiveDamage(damageToGive);
        }
        // Create an effect
        Instantiate(impactEffect, transform.position, transform.rotation);
        // Destroy
        Destroy(gameObject);
    }
}
