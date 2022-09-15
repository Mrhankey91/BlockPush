using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelData
{
    public int id = 1;
    public int time = 60;//in seconds
    public ObstacleData[] obstacles;
    public PowerUpData[] powerUps;
}
