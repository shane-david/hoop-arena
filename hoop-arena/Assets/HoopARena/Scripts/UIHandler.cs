using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

//Written by Zachary
//The UI handler will update and manage the UI whenever it updates.
public class UIHandler : MonoBehaviour
{

    [Header("Object References")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Button _resetButton;

    public static event Action ResetGame;
    //Time section
    private float timeRemaining = 120f;
    private bool timerPaused = true;

    private void OnEnable()
    {
        EventHandler.OnStopPlayerShooting += ShowButton;
    }

    private void OnDisable()
    {
        EventHandler.OnStopPlayerShooting -= ShowButton;

    }


    #region methods
    public void UpdateScore(int newscore)
    { 
        _scoreText.text = $"Score: {newscore}";
    }



    public void PauseResumeTimer()
    {
        //Timer paused is now equal to the opposite state
        timerPaused = !timerPaused;
    }

    public void ShowButton()
    {
        _resetButton.gameObject.SetActive(true);
    }

    public void OnResetGame()
    {
        _scoreText.text = "Score: 0";
        timeRemaining = 120f;
        timerPaused = false;
        _resetButton.gameObject.SetActive(false);
        ResetGame?.Invoke();
    }
    #endregion
}
