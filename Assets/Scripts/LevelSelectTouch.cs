using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectTouch : MonoBehaviour
{

    public LevelSelectManager theLevelSelectManager;

    void Start()
    {
        theLevelSelectManager = FindObjectOfType<LevelSelectManager>();

        theLevelSelectManager.touchMode = true;
    }

    public void MoveLeft()
    {
        theLevelSelectManager.positionSelector -= 1;

        if (theLevelSelectManager.positionSelector < 0)
            theLevelSelectManager.positionSelector = 0;
    }

    public void MoveRight()
    {
        theLevelSelectManager.positionSelector += 1;

        if (theLevelSelectManager.positionSelector >= theLevelSelectManager.levelTags.Length)
            theLevelSelectManager.positionSelector = theLevelSelectManager.levelTags.Length - 1;

    }

    public void LoadLevel()
    {
        PlayerPrefs.SetInt("PlayerLevelSelectPosition", theLevelSelectManager.positionSelector);
        SceneManager.LoadScene(theLevelSelectManager.levelName[theLevelSelectManager.positionSelector]);
    }
}
