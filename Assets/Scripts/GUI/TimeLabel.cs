using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLabel : MonoBehaviour
{

    private Text label;

    void Awake()
    {
        label = GetComponent<Text>();
        GameObject.Find("GameController").GetComponent<GameController>().onTimeUpdate += OnTimeUpdate;
    }

    private void OnTimeUpdate(int timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = timeInSeconds % 60;
        label.text = string.Format("{0}:{1}", (minutes < 10 ? "0"+minutes : ""+minutes), (seconds < 10 ? "0" + seconds : "" + seconds));
    }
}
