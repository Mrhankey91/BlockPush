using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerUpData
{
    public string id;
    public Vector3 position;

    public PowerUpData(string id, Vector3 position)
    {
        this.id = id;
        this.position = position;
    }
}