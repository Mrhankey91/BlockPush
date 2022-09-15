using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : LevelObject
{
    public void Init()
    {
        
    }

    protected virtual void PowerUpCollected()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PowerUpCollected();
            Disable();
        }
    }
}
