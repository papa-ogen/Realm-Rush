using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    float yOffset = 5f;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    private void OnMouseDown()
    {
        if(isPlaceable)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);

            Instantiate(towerPrefab, position, Quaternion.identity);

            isPlaceable = false;      
        }    
    }
}
