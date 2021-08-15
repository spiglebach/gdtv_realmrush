using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour {
    [SerializeField] private List<Waypoint> path;
    [SerializeField] private float transitionTime = 1f;
    
    void Start() {
        StartCoroutine(PrintWaypointNames());
    }

    IEnumerator PrintWaypointNames() {
        foreach (var waypoint in path) {
            transform.SetPositionAndRotation(waypoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(transitionTime);
        }
    }

}
