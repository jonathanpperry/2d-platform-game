using UnityEngine;

public class ShootAtPlayerInRange : MonoBehaviour
{
    public float playerRange;

    public GameObject enemyStar;

    public PlayerController player;

    public Transform launchPoint;

    public float waitBetweenShots;
    private float shotCounter;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        shotCounter = waitBetweenShots;
    }


    void Update()
    {
        Debug.DrawLine(new Vector3(transform.position.x - playerRange, transform.position.y, transform.position.z), new Vector3(transform.position.x + playerRange, transform.position.y, transform.position.z));
        shotCounter -= Time.deltaTime;
        // When the enemy is moving right
        if (transform.localScale.x < 0 && player.transform.position.x > transform.position.x &&
            player.transform.position.x < transform.position.x + playerRange && shotCounter < 0)
        {
            Instantiate(enemyStar, launchPoint.position, launchPoint.rotation);
            // Reset the shot counter
            shotCounter = waitBetweenShots;
        }
        
        // When the enemy is moving left
        if (transform.localScale.x > 0 && player.transform.position.x < transform.position.x &&
            player.transform.position.x > transform.position.x - playerRange && shotCounter < 0)
        {
            Instantiate(enemyStar, launchPoint.position, launchPoint.rotation);
            // Reset the shot counter
            shotCounter = waitBetweenShots;
        }

    }
}
