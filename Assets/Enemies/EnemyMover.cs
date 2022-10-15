using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    List<Node> path = new List<Node>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    [SerializeField] float yOffset = 5f;

    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if(resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        } else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();

        path.Clear();
        path = pathfinder.GetNewPath(coordinates);

        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        Vector3 startPosition = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
        // Offset due to asset
        Vector3 position = new Vector3(startPosition.x, startPosition.y + yOffset, startPosition.z);
        transform.position = position;
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 destinationPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            Vector3 endPosition = new Vector3(destinationPosition.x, destinationPosition.y + yOffset, destinationPosition.z); ;
            float travelPercent = 0;

            var targetRotation = Quaternion.LookRotation(destinationPosition - transform.position);


            //transform.LookAt(endPosition);

            while (travelPercent < 1f) {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, travelPercent);

                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }
}
