using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private Transform prefabsParent;
    public GameObject[] prefabs;
    private Dictionary<string, GameObject> prefabsDict = new Dictionary<string, GameObject>();
    private Dictionary<string, List<GameObject>> objectsPool = new Dictionary<string, List<GameObject>>();

    public Material normalMaterial;
    public Material fixedMaterial;
    public Material neutralMaterial;
    public Material hazardousMaterial;

    public LevelData[] levelData;
    private int currentLevel;

    void Awake()
    {
        prefabsParent = GameObject.Find("Level").transform;
        CreateDictionaries();

        GetComponent<GameController>().onStart += OnStart;
    }

    private void CreateDictionaries()
    {
        for (int i = 0; i < prefabs.Length; ++i)
        {
            string id = prefabs[i].GetComponent<LevelObject>().id;
            prefabsDict.Add(id, prefabs[i]);
            objectsPool.Add(id, new List<GameObject>());
        }
    }

    private void SpawnLevel(int levelId)
    {
        currentLevel = levelId;
        foreach (ObstacleData obstacle in levelData[levelId].obstacles)
        {
            SpawnPrefab(obstacle);
        }
        foreach(PowerUpData powerUp in levelData[levelId].powerUps)
        {
            SpawnPrefab(powerUp);
        }
    }

    public GameObject SpawnPrefab(ObstacleData obstacle)
    {
        return SpawnPrefab(obstacle.id, obstacle.position, obstacle.type);
    }
    
    public GameObject SpawnPrefab(PowerUpData powerUp)
    {
        GameObject obj = SpawnPrefab(powerUp.id, powerUp.position);
        obj.GetComponent<PowerUp>().Init();
        return obj;
    }

    public GameObject SpawnPrefab(string id, Vector3 position)
    {
        GameObject obstacle = CreatePrefab(id, position);

        obstacle?.GetComponent<Obstacle>()?.Init(Obstacle.ObstacleType.Neutral);

        return obstacle;
    }

    public GameObject SpawnPrefab(string id, Vector3 position, Obstacle.ObstacleType obstacleType)
    {
        GameObject obstacle = CreatePrefab(id, position);

        obstacle?.GetComponent<Obstacle>().Init(obstacleType);

        return obstacle;
    }
    
    private GameObject CreatePrefab(string id, Vector3 position)
    {
        try
        {
            List<GameObject> pool = objectsPool[id];
            GameObject obj = null;
            if (pool.Count == 0)//Create new gameobject
            {
                obj = Instantiate(prefabsDict[id], position, Quaternion.identity, prefabsParent);
            }
            else //reuse gameobject form pool
            {
                obj = pool[0];
                obj.transform.position = position;
                obj.SetActive(true);
                pool.RemoveAt(0);
            }

            return obj;
        }
        catch (KeyNotFoundException e)
        {
            Debug.Log(string.Format("Key: {0} not found in prefabs", id));
            return null;
        }
    }

    public void Disable(GameObject obj, string id)
    {
        objectsPool[id].Add(obj);
        obj.SetActive(false);
    }

    public void DisableAll()
    {
        foreach (Transform t in prefabsParent.transform)
        {
            if (t != prefabsParent)
            {
                GameObject obj = t.gameObject;
                if (obj.activeSelf)
                {
                    objectsPool[obj.GetComponent<LevelObject>().id].Add(obj);
                    obj.SetActive(false);
                }
            }
        }
    }

    public int GetMaxTimeLevel()
    {
        return levelData[currentLevel].time;
    }

    public Material GetMaterial(Obstacle.ObstacleType obstacleType)
    {
        switch (obstacleType)
        {
            case Obstacle.ObstacleType.Normal: return normalMaterial;
            case Obstacle.ObstacleType.Fixed: return fixedMaterial; 
            case Obstacle.ObstacleType.Hazardous: return hazardousMaterial;
            default: return neutralMaterial;
        }
    }

    private void OnStart(int level)
    {
        DisableAll();
        SpawnLevel(level - 1);
    }
}
