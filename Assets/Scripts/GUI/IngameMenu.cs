using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameMenu : MonoBehaviour
{
    private GameController gameController;

    private bool show = true;

    void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        ShowInGameMenu(false);
    }

    public void ToggleInGameMenu()
    {
        ShowInGameMenu(!show);
    }

    public void ShowInGameMenu(bool show)
    {
        this.show = show;
        gameObject.SetActive(show);
        gameController.PauseGame(show);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
