using UnityEngine;

public class Waypoint : MonoBehaviour {
    [SerializeField] private bool buildable;
    [SerializeField] private GameObject towerPrefab;

    public bool IsBuildable => buildable;

    private void OnMouseDown() {
        if (!buildable) return;
        if (!towerPrefab) return;
        Instantiate(towerPrefab, transform.position, Quaternion.identity);
        buildable = false;
    }
}
