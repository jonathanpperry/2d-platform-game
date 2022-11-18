using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthManager : MonoBehaviour
{
    public int enemyHealth;

    public GameObject deathEffect;

    public int pointsOnDeath;

    public GameObject bossPrefab;

    public float minSize;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            ScoreManager.AddPoints(pointsOnDeath);
            // If the boss is bigger than minimum size
            if (transform.localScale.y > minSize)
            {
                // Create duplicates
                GameObject clone1 = Instantiate(bossPrefab,
                    new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
                GameObject clone2 = Instantiate(bossPrefab,
                    new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
                clone1.transform.localScale = new Vector3(transform.localScale.y * 0.5f, transform.localScale.y * 0.5f, transform.localScale.z);
                clone1.GetComponent<BossHealthManager>().enemyHealth = 10;
                clone2.transform.localScale = new Vector3(transform.localScale.y * 0.5f, transform.localScale.y * 0.5f, transform.localScale.z);
                clone1.GetComponent<BossHealthManager>().enemyHealth = 10;
            }
            
            // Destroy the boss
            Destroy(gameObject);
        }
        
        
    }

    public void GiveDamage(int damageToGive)
    {
        enemyHealth -= damageToGive;
        GetComponent<AudioSource>().Play();
    }
}
