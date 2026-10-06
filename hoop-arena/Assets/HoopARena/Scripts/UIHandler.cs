using UnityEngine;
using TMPro;
using System;

//Written by Zachary
//The UI handler will update and manage the UI whenever it updates.
public class UIHandler : MonoBehaviour
{

    [Header("Object References")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timerText;


    //Time section
    private float timeRemaining = 120f;
    private bool timerPaused = true;

    private void OnEnable()
    {
        EventHandler.OnScoreUpdated += UpdateScore;
    }

    private void OnDisable()
    {
        EventHandler.OnScoreUpdated -= UpdateScore;
    }

    private void Start()
    {
        //Start the timer
        timerPaused = false;
    }
    private void Update()
    {
        if (timerPaused != true) UpdateTimer();
    }

    #region methods
    public void UpdateScore(int newscore)
    { 
        _scoreText.text = $"Score: {newscore}";
    }

    private void UpdateTimer()
    {
        if (timeRemaining <= 0f)
        {
            timerPaused = true;

            //Stop the player shooting
            EventHandler.StopPlayerShooting();

            _timerText.text = "0:00 :Time";
            return;
        }
        timeRemaining -= Time.deltaTime;


        _timerText.text = string.Format("{0:0}:{1:00} :Time", 
                                        Mathf.FloorToInt(timeRemaining / 60), 
                                        Mathf.FloorToInt(timeRemaining % 60));
    }

    public void PauseResumeTimer()
    {
        //Timer paused is now equal to the opposite state
        timerPaused = !timerPaused;
    }
#endregion
}
