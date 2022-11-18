using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string startLevel;

    public string levelSelect;

    public int playerLives;

    public int playerHealth;
    
    public string level1Tag;

    public void NewGame()
    {
        PlayerPrefs.SetInt("PlayerCurrentLives", playerLives);
        // Set score to 0 on a new game
        PlayerPrefs.SetInt("CurrentPlayerScore", 0);
        PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
        PlayerPrefs.SetInt("PlayerMaxHealth", playerHealth);
        // Set the level 1 as the default
        PlayerPrefs.SetInt(level1Tag, 1);
        // Set position selector to first position by default
        PlayerPrefs.SetInt("PlayerLevelSelectPosition", 0);
        // Load level after setting lives/score
        SceneManager.LoadScene(startLevel);
    }

    public void LevelSelect()
    {
        PlayerPrefs.SetInt("PlayerCurrentLives", playerLives);
        PlayerPrefs.SetInt("CurrentPlayerScore", 0);
        // Set the player current health
        PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
        PlayerPrefs.SetInt("PlayerMaxHealth", playerHealth);
        // Unlock first level
        PlayerPrefs.SetInt(level1Tag, 1);

        if (!PlayerPrefs.HasKey("PlayerLevelSelectPosition"))
        {
            PlayerPrefs.SetInt("PlayerLevelSelectPosition", 0);
        }
        
        // Load level select scene
        SceneManager.LoadScene(levelSelect);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
