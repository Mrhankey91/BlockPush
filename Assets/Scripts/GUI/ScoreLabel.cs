using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLabel : MonoBehaviour
{
    private Text label;

    void Awake()
    {
        label = GetComponent<Text>();
        GameObject.Find("GameController").GetComponent<ScoreComponent>().onScoreUpdate += OnScoreUpdate;
    }

    private void OnScoreUpdate(int score, int change)
    {
        label.text = "" + score;
    }
}
