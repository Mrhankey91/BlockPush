using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPart : MonoBehaviour
{
    private float moveZ = 200f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            transform.parent.position += new Vector3(0f, 0f, moveZ);
        }
    }
}
