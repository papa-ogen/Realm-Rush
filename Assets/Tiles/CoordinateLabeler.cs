using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.red;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    Waypoint waypoint;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        waypoint = GetComponentInParent<Waypoint>();
        label.enabled = false;

        DisplayCoordinates();
    }

    void Update()
    {
         if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    private void SetLabelColor()
    {
        if(waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        } else
        {
            label.color = blockedColor;
        }
    }

    private void DisplayCoordinates()
    {
        // Funkar inte: UnityEditor.EditorSnapSettings.move.x
        
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / 10);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / 10);

        label.text = coordinates.x + "," + coordinates.y;
    }

    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
