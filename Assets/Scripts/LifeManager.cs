using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LifeManager : MonoBehaviour
{
    //public int startingLives;
    private int lifeCounter;

    private Text theText;

    public GameObject gameOverScreen;

    public PlayerController player;

    public string mainMenu;

    public float waitAfterGameOver;

    void Start()
    {
        theText = GetComponent<Text>();
        lifeCounter = PlayerPrefs.GetInt("PlayerCurrentLives");
        // Get the player object
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCounter < 0)
        {
         }
        theText.text = "x " + lifeCounter;

        if (gameOverScreen.activeSelf)
        {
            waitAfterGameOver -= Time.deltaTime;
        }
        // Start the game over again
        if (waitAfterGameOver < 0)
        {
            SceneManager.LoadScene(mainMenu);
        }
    }

    public void GiveLife()
    {
        lifeCounter++;
        PlayerPrefs.SetInt("PlayerCurrentLives", lifeCounter);
    }
    public void TakeLife()
    {
        lifeCounter--;
        PlayerPrefs.SetInt("PlayerCurrentLives", lifeCounter);

    }

}
