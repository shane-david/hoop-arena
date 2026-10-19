using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

//Written by Zachary
//The UI handler will update and manage the UI whenever it updates.
public class UIHandler : MonoBehaviour
{

    [Header("Object References")]

    [SerializeField] private Button _resetButton;

    [SerializeField] private TextMeshProUGUI _scanAreaPrompt;

    public static event Action ResetGame;
    //Time section
    private float timeRemaining = 120f;
    private bool timerPaused = true;
    private bool gameRunning = true; 

    private void OnEnable()
    {
        EventHandler.OnStopPlayerShooting += ShowButton;
        ScoreHandler.OnGameStart += GameStart;
        EventHandler.OnWallDetected += WallScanned; 
        TouchInputHandler.OnTouchedScreen += OnResetGame;
    }

    private void OnDisable()
    {
        EventHandler.OnStopPlayerShooting -= ShowButton;
        ScoreHandler.OnGameStart -= GameStart;
        EventHandler.OnWallDetected -= WallScanned; 
        TouchInputHandler.OnTouchedScreen -= OnResetGame;
    }


    #region methods
    public void ShowButton()
    {
        _scanAreaPrompt.gameObject.SetActive(true); 
        _scanAreaPrompt.text = "Tap to Reset!"; 
        gameRunning = false; 
    }

    public void OnResetGame(Vector2 pos)
    {
        if (!gameRunning)
        {
            ResetGame?.Invoke(); 
            GameStart(); 
        }
    }

    public void GameStart()
    {
        //if the area prompt is active, disable it.
        if (_scanAreaPrompt.IsActive())
        {
            _scanAreaPrompt.gameObject.SetActive(false);
        }
        gameRunning = true; 
    }

    // if a wall has been scanned change the prompt 
    private void WallScanned()
    {
        _scanAreaPrompt.text = "Tap the wall to begin!"; 
    }

    #endregion
}
