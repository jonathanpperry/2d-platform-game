using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    public float startingTime;
    private float countingTime;

    private Text theText;

    private PauseMenu thePauseMenu;
    private HealthManager theHealth;

    void Start()
    {
        countingTime = startingTime;
        theText = GetComponent<Text>();
        thePauseMenu = FindObjectOfType<PauseMenu>();
        // Get the health manager
        theHealth = FindObjectOfType<HealthManager>();
    }

    void Update()
    {
        // Add a check for using the pause menu
        if (thePauseMenu.isPaused) 
            return;
        // If the time reaches zero
        if (countingTime <= 0)
        {
            theHealth.KillPlayer();
        }
        countingTime -= Time.deltaTime;
        theText.text = "" + Mathf.Round(countingTime);
    }

    public void ResetTime()
    {
        countingTime = startingTime;
    }
}
