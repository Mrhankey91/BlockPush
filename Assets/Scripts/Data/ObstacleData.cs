using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleData
{
    public string id;
    public Vector3 position;
    public Obstacle.ObstacleType type;

    public ObstacleData(string id, Vector3 position, Obstacle.ObstacleType type)
    {
        this.id = id;
        this.position = position;
        this.type = type;
    }
}
