using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    private Text title;

    private void Awake()
    {
        title = transform.Find("Title").GetComponent<Text>();
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(string end)
    {
        if (end == "GameOver")
            title.text = "Game Over!";
        else if (end == "LevelCompleted")
            title.text = "Level Completed!";
        else if (end == "OutOfTime")
            title.text = "Out of Time!";

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
