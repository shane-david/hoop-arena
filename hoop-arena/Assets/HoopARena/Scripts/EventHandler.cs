using UnityEngine;
using System;

public static class EventHandler
{
    public static event Action<int> OnScoreUpdated;

    public static void ScoreUpdated(int score)
    {
        OnScoreUpdated?.Invoke(score);
    }
}
