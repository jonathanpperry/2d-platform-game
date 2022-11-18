using UnityEngine;

public class LifePickup : MonoBehaviour
{
    private LifeManager lifeSystem;

    void Start()
    {
        lifeSystem = FindObjectOfType<LifeManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.name == "Player")
        {
            lifeSystem.GiveLife();
            Destroy(gameObject);
        }
    }

}
