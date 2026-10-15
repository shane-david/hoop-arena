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

    private void OnEnable()
    {
        EventHandler.OnStopPlayerShooting += ShowButton;
        ScoreHandler.OnGameStart += GameStart;
    }

    private void OnDisable()
    {
        EventHandler.OnStopPlayerShooting -= ShowButton;
        ScoreHandler.OnGameStart -= GameStart;

    }


    #region methods
    public void ShowButton()
    {
        _resetButton.gameObject.SetActive(true);
    }

    public void OnResetGame()
    {
        _resetButton.gameObject.SetActive(false);
        ResetGame?.Invoke();
    }

    public void GameStart()
    {
        //if the area prompt is active, disable it.
        if (_scanAreaPrompt.IsActive())
        {
            _scanAreaPrompt.gameObject.SetActive(false);
        }
    }
    #endregion
}
