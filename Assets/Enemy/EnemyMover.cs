using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour {
    [SerializeField][Range(0f,5f)] private float speed = 1f;

    private List<Node> path = new List<Node>();
    private Enemy enemy;
    private GridManager gridManager;
    private Pathfinder pathfinder;
    
    void OnEnable() {
        ReturnToStart();
        RecalculatePath(true);
    }

    private void Awake() {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void RecalculatePath(bool resetPath) {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath) {
            coordinates = pathfinder.StartCoordinates;
        } else {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    private void ReturnToStart() {
        transform.SetPositionAndRotation(gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates), Quaternion.identity);
    }

    IEnumerator FollowPath() {
        for (int i = 1; i < path.Count; i++) {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinate);
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
        FinishPath();
    }
    
    private void FinishPath() {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
}
