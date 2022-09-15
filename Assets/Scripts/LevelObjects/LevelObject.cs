using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    protected GameController gameController;
    protected LevelController levelController;

    public string id;

    protected virtual void Awake()
    {
        gameController = GameObject.Find("GameController")?.GetComponent<GameController>();
        levelController = gameController?.GetComponent<LevelController>();
    }

    protected virtual void Start()
    {

    }

    public virtual void Disable()
    {
        levelController.Disable(gameObject, id);
    }
}
