using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStarController : MonoBehaviour
{
    public float speed;

    public PlayerController player;

    public GameObject impactEffect;

    public float rotationSpeed;
    
    public int damageToGive;
    
    private Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        myRigidbody2D = GetComponent<Rigidbody2D>();
        // If player is to the left of the ninja star
        if (player.transform.position.x < transform.position.x)
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
        if (other.name == "Player")
        {
            HealthManager.HurtPlayer(damageToGive);
        }
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
