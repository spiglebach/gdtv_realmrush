using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private bool buildable;
    [SerializeField] private Tower towerPrefab;

    private GridManager gridManager;
    private Pathfinder pathfinder;
    private Vector2Int coordinates = new Vector2Int();

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void Start() {
        if (gridManager) {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!buildable) {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    public bool IsBuildable => buildable;

    private void OnMouseDown() {
        if (!gridManager) return;
        if (!towerPrefab) return;
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates)) {
            bool isSuccessful = towerPrefab.CreateTowerAt(towerPrefab, transform.position);
            if (isSuccessful) {
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyRecievers();
            }
        }
    }
}
