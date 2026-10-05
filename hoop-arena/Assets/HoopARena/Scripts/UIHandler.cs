using UnityEngine;
using TMPro;
using System;

//Written by Zachary
//The UI handler will update and manage the UI whenever it updates.
public class UIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timerText;

    [Header("Object References")]
    

    private int scoreValue = 0;
    private int timer;

    private void OnEnable()
    {
        EventHandler.OnScoreUpdated += UpdateScore;
    }

    private void OnDisable()
    {
        EventHandler.OnScoreUpdated -= UpdateScore;
    }


    public void UpdateScore(int newscore)
    {
        scoreValue = newscore;

        _scoreText.text = $"Score: {scoreValue}";
    }

}
