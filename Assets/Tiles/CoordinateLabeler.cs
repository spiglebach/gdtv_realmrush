using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour {
    private TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int();
    private GridManager gridManager;

    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color blockedColor = Color.magenta;
    [SerializeField] private Color exploredColor = Color.yellow;
    [SerializeField] private Color pathColor = new Color(1f, .5f, 0f);

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        //label.enabled = false;
        DisplayCoordinates();
    }

    void Update() {
        SetLabelColor();
        ToggleLabels();
        if (Application.isPlaying) return;
        // in edit mode
        DisplayCoordinates();
        UpdateObjectName();
    }

    private void SetLabelColor() {
        if (!gridManager) return;
        Node node = gridManager.GetNode(coordinates);
        if (node == null) return;
        if (!node.isWalkable) {
            label.color = blockedColor;
        } else if (node.isPath) {
            label.color = pathColor;
        } else if (node.isExplored) {
            label.color = exploredColor;
        } else {
            label.color = defaultColor;
        }
    }

    private void DisplayCoordinates() {
        if (!gridManager) return;
        var parentPosition = transform.parent.position;
        
        coordinates.x = Mathf.RoundToInt(parentPosition.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(parentPosition.z / gridManager.UnityGridSize);
        label.text = $"x,y\n{coordinates.x.ToString()},{coordinates.y.ToString()}";
    }

    void UpdateObjectName() {
        transform.parent.name = coordinates.ToString();
    }

    void ToggleLabels() {
        if (Input.GetKeyDown(KeyCode.C)) {
            label.enabled = !label.isActiveAndEnabled;
        }
    }
}
