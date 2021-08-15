using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour {
    [SerializeField] private List<Waypoint> path;
    [SerializeField][Range(0f,5f)] private float speed = 1f;
    
    void Start() {
        StartCoroutine(PrintWaypointNames());
    }

    IEnumerator PrintWaypointNames() {
        foreach (var waypoint in path) {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            //transform.LookAt(endPosition);
            Vector3 directionVector = endPosition - startPosition;
            Quaternion rotation = Quaternion.identity;
            if (directionVector.sqrMagnitude > Mathf.Epsilon) {
                rotation = Quaternion.LookRotation(directionVector, Vector3.up);
            }
            float travelProgress = 0f;
            while (travelProgress < 1f) {
                travelProgress += Time.deltaTime * speed;
                transform.SetPositionAndRotation(
                    Vector3.Lerp(startPosition, endPosition, travelProgress),
                    rotation);
                yield return new WaitForEndOfFrame();
            }
        }
    }

}
