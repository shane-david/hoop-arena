using UnityEngine;
using TMPro;

//Written by Zachary
//The UI handler will update and manage the UI whenever it updates.
public class UIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timerText;

    private int scoreValue = 0;
    private int timer;


    public void UpdateScore(int newscore)
    {
        scoreValue = newscore;

        _scoreText.text = $"Score: {scoreValue}";
    }

}
