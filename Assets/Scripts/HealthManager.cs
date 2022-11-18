using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxPlayerHealth;
    public static int playerHealth;

    // Slider object
    public Slider healthBar;

    private LevelManager levelManager;

    public bool isDead;

    private LifeManager lifeSystem;

    private TimeManager theTime;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Slider>();
        playerHealth = PlayerPrefs.GetInt("PlayerCurrentHealth");
        theTime = FindObjectOfType<TimeManager>();
        levelManager = FindObjectOfType<LevelManager>();
        lifeSystem = FindObjectOfType<LifeManager>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0 && !isDead)
        {
            playerHealth = 0;
            levelManager.RespawnPlayer();
            lifeSystem.TakeLife();
            isDead = true;
            // Reset the time
            theTime.ResetTime();
        }

        if (playerHealth > maxPlayerHealth)
            playerHealth = maxPlayerHealth;

        // Change value of health bar
        healthBar.value = playerHealth;
    }

    public static void HurtPlayer(int damageToGive)
    {
        playerHealth -= damageToGive;
        PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
    }

    public void FullHealth()
    {
        playerHealth = PlayerPrefs.GetInt("PlayerMaxHealth");
        PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
    }

    public void KillPlayer()
    {
        playerHealth = 0;
    }
}
