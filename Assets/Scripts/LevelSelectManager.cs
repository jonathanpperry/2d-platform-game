using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public string[] levelTags;

    public GameObject[] locks;
    public bool[] levelUnlocked;

    public int positionSelector;
    public float distanceBelowLock;

    public string[] levelName;

    public float moveSpeed;

    private bool isPressed;

    public bool touchMode;
    
    private void Start()
    {
        PlayerPrefs.SetInt(levelTags[0], 1);
        levelUnlocked[0] = true;
        for (int i = 0; i < levelTags.Length; i++)
        {
            // The level is not unlocked yet
            if (PlayerPrefs.GetInt(levelTags[i]) == null)
            {
                levelUnlocked[i] = false;
            } else if (PlayerPrefs.GetInt(levelTags[i]) == 0)
            {
                levelUnlocked[i] = false;
            }
            else
            {
                levelUnlocked[i] = true;
            }
            
            // Remove the lock icon
            if (levelUnlocked[i])
            {
                locks[i].SetActive(false);
            }
        }
        // Get the position to start at from playerprefs
        positionSelector = PlayerPrefs.GetInt("PlayerLevelSelectPosition");

        transform.position = locks[positionSelector].transform.position + new Vector3(0, distanceBelowLock, 0);
    }

    private void Update()
    {
        // If not pressed
        if (!isPressed)
        {
            if (Input.GetAxis("Horizontal") > 0.25f)
            {
                positionSelector += 1;
                isPressed = true;
            }

            if (Input.GetAxis("Horizontal") < -0.25f)
            {
                positionSelector -= 1;
                isPressed = true;
            }

            // If going over the max, reset to end
            if (positionSelector >= levelTags.Length)
            {
                positionSelector = levelTags.Length - 1;
            }
            // Error check for is selector is less than 0
            if (positionSelector < 0)
            {
                positionSelector = 0;
            }
        }
        
        // If pressed
        if (isPressed)
        {
            if (Input.GetAxis("Horizontal") < 0.25f && Input.GetAxis("Horizontal") > -0.25f)
            {
                isPressed = false;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position,
        locks[positionSelector].transform.position + new Vector3(0, distanceBelowLock, 0),
        moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            // Load up the level if not in touch mode
            if (levelUnlocked[positionSelector] && !touchMode)
            {
                // Set the level to return to
                PlayerPrefs.SetInt("PlayerLevelSelectPosition", positionSelector);
                // Load scene
                SceneManager.LoadScene(levelName[positionSelector]);
            }
        }
    }
}
