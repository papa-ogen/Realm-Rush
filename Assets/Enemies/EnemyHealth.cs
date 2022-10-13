using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;

    int currentHitPoints = 0;

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;    
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("HIT");
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitPoints--;

        if(currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
