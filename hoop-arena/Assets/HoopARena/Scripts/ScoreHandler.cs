using UnityEngine;
using System;
using TMPro;

//Written by Zachary
//The job of the score handler is to keep track and update the player's score.
public class ScoreHandler : MonoBehaviour
{

    // get a reference to the camera so that we can determine the player's distance 
    [SerializeField] Camera _arCamera;

    //variables to keep track of score 
    public float BaseScoreAddition = 1f;
    public float ScoreMultiplier = 3f;

    // variable for current score multiplier based on distance
    private int _currentScoreAddition;

    // the player's actual score 
    private int _currentScore;

    private int _streakTotal;

    //This event will be called when this object is created
    //The UI handler will be listening for this event to get 
    //the correct reference to this object.
    public event Action Created;

    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private TextMeshPro timerText;

    //Time section
    private float timeRemaining = 70f;
    private bool timerPaused = true;

    [SerializeField] private ParticleSystem _onScoreParticles;

    [SerializeField] private ParticleSystem[] _fireParticles;

    public static Action OnGameStart;

    private void OnEnable()
    {
        UIHandler.ResetGame += OnReset;
        EventHandler.OnScoreUpdated += UpdateScore;
        ScoreHandler.OnGameStart += GameStart;
        BasketBallHandler.WhenDestroyed += ResetStreak;
    }

    private void OnDisable()
    {
        UIHandler.ResetGame -= OnReset;
        EventHandler.OnScoreUpdated -= UpdateScore;
        ScoreHandler.OnGameStart -= GameStart;
        BasketBallHandler.WhenDestroyed -= ResetStreak;
    }

    private void Start()
    {
        OnGameStart?.Invoke();
    }

    private void Update()
    {
        DetermineDistance();
        if (timerPaused != true) UpdateTimer();
    }

    //If a ball leaves the hitbox of this collider, then update the score.
    private void OnTriggerExit(Collider collision)
    {
        //if the collided object is not a basketball, return
        if (!collision.gameObject.GetComponent<BasketBallHandler>()) return;

        //if the collided object has the basketballhandler script, score a point
        if (collision.gameObject.GetComponent<BasketBallHandler>())
        {
            //add 1 to the total streak value
            _streakTotal += 1;

            _currentScoreAddition = ScoreStreak();

            _currentScore += _currentScoreAddition;

            // tell the level changer there was a score
            EventHandler.InvokeScore(); 

            // call this event to display the score to screen
            EventHandler.ScoreAmountDetermined(_currentScoreAddition);

            //Call the event with the updated value
            EventHandler.ScoreUpdated(_currentScore);

            //Play a one shot of the particles
            _onScoreParticles.Play();

            //Destroy the basket ball
            Destroy(collision.gameObject);
        }
    }

    private void DetermineDistance()
    {
        // make sure the ar camera is not null 
        if (_arCamera == null) return;

        // find the distance 
        float distance = Vector3.Distance(_arCamera.transform.position, this.transform.position);

        // set score based on distance 
        float unrounded = BaseScoreAddition * Mathf.Pow(distance, ScoreMultiplier);
        _currentScoreAddition = Mathf.RoundToInt(unrounded);
        //Debug.Log(_currentScoreAddition);
    }

    #region UI region
    private void OnReset()
    {
        //reset the score to 0
        _currentScore = 0;
        UpdateScore(_currentScore);

        //reset to 70 seconds
        timeRemaining = 70f;

        //Unpause the game
        PauseResumeTimer();
    }

    public void UpdateScore(int newscore)
    {
        scoreText.text = $"{newscore}";
    }

    private void UpdateTimer()
    {
        if (timeRemaining <= 0f)
        {
            timerPaused = true;

            //Stop the player shooting
            EventHandler.StopPlayerShooting();

            timerText.text = "0:00";
            return;
        }
        timeRemaining -= Time.deltaTime;


        timerText.text = string.Format("{0:0}:{1:00}",
                                        Mathf.FloorToInt(timeRemaining / 60),
                                        Mathf.FloorToInt(timeRemaining % 60));
    }

    public void PauseResumeTimer()
    {
        //Timer paused is now equal to the opposite state
        timerPaused = !timerPaused;
    }
    #endregion
    public void GameStart()
    {
        if (_arCamera == null)
        {
            _arCamera = Camera.main;
        }

        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshPro>();
        }

        if (timerText == null)
        {
            timerText = GameObject.Find("TimerText").GetComponent<TextMeshPro>();
        }

        timeRemaining = 70f;

        _currentScore = 0;
        EventHandler.ScoreUpdated(_currentScore);

        timerPaused = false;
    }

    private int ScoreStreak()
    {
        //If the streak amount is greater than 3, double the added score
        if (_streakTotal > 3)
        {
            TriggerFireParticles(true);
            return _currentScoreAddition * 2;
        }
        //Otherwise, return the original score.
        return _currentScoreAddition;
    }

    private void ResetStreak()
    {
        _streakTotal = 0;
        TriggerFireParticles(false);
    }

    private void TriggerFireParticles(bool state)
    {
        //true is activate, false is deactivate. 
        bool active = _fireParticles[0].isPlaying;

        if (!active && !state) return;

        if (state == true)
        {
            foreach (ParticleSystem ps in _fireParticles)
            {
                ps.Play();
            }
        }
        else
        {
            foreach (ParticleSystem ps in _fireParticles)
            {
                ps.Stop();
            }
        }
    }
}
