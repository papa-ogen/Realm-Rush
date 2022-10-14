using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    [SerializeField] float yOffset = 5f;

    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }


    void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach(Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if(waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart()
    {
        Vector3 position = new Vector3(path[0].transform.position.x, path[0].transform.position.y + yOffset, path[0].transform.position.z);
        transform.position = position;
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(waypoint.transform.position.x, waypoint.transform.position.y + yOffset, waypoint.transform.position.z); ;
            float travelPercent = 0;

            var targetRotation = Quaternion.LookRotation(waypoint.transform.position - transform.position);


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
