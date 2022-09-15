using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : LevelObject
{
    public enum ObstacleType
    {
        Normal, Fixed, Neutral, Hazardous
    }

    private Rigidbody rigidBody;
    private MeshRenderer meshRenderer;

    public bool isPushable = true;
    public ObstacleType type = ObstacleType.Normal;

    void Awake()
    {
        base.Awake();
        rigidBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void FixedUpdate()
    {
        if(transform.position.y < -3f)
        {
            Disable();
            if (type == ObstacleType.Hazardous)
                gameController.GameOver();
        }
    }

    public void Init(ObstacleType type)
    {
        transform.rotation = Quaternion.Euler(0f,0f,0f);
        ChangeType(type);
    }

    public void ChangeType(ObstacleType type)
    {
        this.type = type;
        isPushable = type == ObstacleType.Normal;
        rigidBody.velocity = rigidBody.angularVelocity = Vector3.zero;
        rigidBody.isKinematic = type == ObstacleType.Fixed;
        meshRenderer.material = levelController.GetMaterial(type);
    }

    public bool IsVisible()
    {
        return meshRenderer.isVisible;
    }
}
