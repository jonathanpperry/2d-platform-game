using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckpoint;

    private PlayerController player;

    // Particle Game Objects
    public GameObject deathParticle;
    public GameObject respawnParticle;

    public int pointPenaltyOnDeath;

    public float respawnDelay;

    private CameraController camera;

    private float gravityStore;

    public HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        camera = FindObjectOfType<CameraController>();
        healthManager = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
        // Launch the blood death particle system anim
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        // Disable the player and renderer
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        // Get the gravity value and set players to zero
        gravityStore = player.GetComponent<Rigidbody2D>().gravityScale;
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        // Zero the velocity of the player to prevent sliding on death
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        // Subtract points from coins for death penalty
        ScoreManager.AddPoints(-pointPenaltyOnDeath);
        // Put a pause between death and respawn
        yield return new WaitForSeconds(respawnDelay);
        player.GetComponent<Rigidbody2D>().gravityScale = gravityStore;
        // Respawn player to first checkpoint
        player.transform.position = currentCheckpoint.transform.position;
        // Reset knockback count
        player.knockbackCount = 0;
        // Re-enable both the player and renderer
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        // Set back to full heatlh
        healthManager.FullHealth();
        healthManager.isDead = false;
        Instantiate(respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
    }
}
