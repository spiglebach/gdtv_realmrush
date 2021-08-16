using UnityEngine;

public class Waypoint : MonoBehaviour {
    [SerializeField] private bool buildable;
    [SerializeField] private Tower towerPrefab;

    public bool IsBuildable => buildable;

    private void OnMouseDown() {
        if (!buildable) return;
        if (!towerPrefab) return;
        bool isPlaced = towerPrefab.CreateTowerAt(towerPrefab, transform.position);
        buildable = !isPlaced;
    }
}
