using UnityEngine;
using System;

public static class EventHandler
{
    public static event Action<int> OnScoreUpdated;
    public static event Action<int> OnScoreAmountDetermined; 
    public static event Action OnStopPlayerShooting;
    public static event Action OnScore; 
    
    public static void InvokeScore()
    {
        OnScore?.Invoke(); 
    }
    
    public static void ScoreUpdated(int score)
    {
        OnScoreUpdated?.Invoke(score);
    }

    public static void ScoreAmountDetermined(int score)
    {
        OnScoreAmountDetermined?.Invoke(score); 
    }

    public static void StopPlayerShooting()
    {
        Debug.Log("Invoking Stopshooting");
        OnStopPlayerShooting?.Invoke();
    }
}
