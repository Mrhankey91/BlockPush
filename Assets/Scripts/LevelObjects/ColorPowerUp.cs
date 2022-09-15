using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPowerUp : PowerUp
{
    private Transform levelPrefab;

    protected override void Awake()
    {
        base.Awake();
        levelPrefab = GameObject.Find("Level").transform;
    }

    protected override void PowerUpCollected()
    {
        foreach(Obstacle obstacle in levelPrefab.GetComponentsInChildren<Obstacle>())
        {
            if (obstacle.IsVisible())
            {
                obstacle.ChangeType(Obstacle.ObstacleType.Normal);
            }
        }
    }
}
