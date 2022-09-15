using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreComponent : MonoBehaviour
{
    private int score = 0;
    public int Score
    {
        set {
            int scoreChange = value - score;
            score = value; onScoreUpdate?.Invoke(score, scoreChange); 
        }
        get { return score; }
    }

    public delegate void OnScoreUpdate(int score, int change);
    public OnScoreUpdate onScoreUpdate;

}
