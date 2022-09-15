using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 cameraOffset;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        cameraOffset = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(cameraOffset.x, cameraOffset.y, playerTransform.position.z + cameraOffset.z), Time.deltaTime * 10f);
    }
}
