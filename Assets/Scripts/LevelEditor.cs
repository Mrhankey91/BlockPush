using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    [SerializeField]
    public ObstacleData[] obstacles;

    [SerializeField]
    public PowerUpData[] powerUps;

    void Start()
    {
        GetAllObstacles();
        GetAllPowerUps();
    }

    private void GetAllObstacles()
    {
        Obstacle[] temp = GameObject.Find("Obstacles").GetComponentsInChildren<Obstacle>();

        obstacles = new ObstacleData[temp.Length];
        for(int i = 0; i < obstacles.Length; ++i)
        {
            obstacles[i] = new ObstacleData(temp[i].id, temp[i].transform.position, temp[i].type);
        }
    }

    private void GetAllPowerUps()
    {
        PowerUp[] temp = GameObject.Find("PowerUps").GetComponentsInChildren<PowerUp>();

        powerUps = new PowerUpData[temp.Length];
        for (int i = 0; i < powerUps.Length; ++i)
        {
            powerUps[i] = new PowerUpData(temp[i].id, temp[i].transform.position);
        }
    }

}
