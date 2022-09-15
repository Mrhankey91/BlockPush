using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPickup : PowerUp
{
    private ScoreComponent scoreComponent;

    private void Awake()
    {
        base.Awake();
        scoreComponent = gameController.GetComponent<ScoreComponent>();
    }

    protected override void PowerUpCollected()
    {
        base.PowerUpCollected();
        scoreComponent.Score++;
    }
}
