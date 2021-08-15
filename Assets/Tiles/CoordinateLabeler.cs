using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour {
    private TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int();
    private Waypoint waypoint;

    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color blockedColor = Color.magenta;

    private void Awake() {
        label = GetComponent<TextMeshPro>();
        waypoint = GetComponentInParent<Waypoint>();
        //label.enabled = false;
        DisplayCoordinates();
    }

    void Update() {
        ColorCoordinates();
        ToggleLabels();
        if (Application.isPlaying) return;
        // in edit mode
        DisplayCoordinates();
        UpdateObjectName();
    }

    private void ColorCoordinates() {
        if (!waypoint) return;
        label.color = waypoint.IsBuildable ? defaultColor : blockedColor;
    }

    private void DisplayCoordinates() {
        var parentPosition = transform.parent.position;
        var snapSettings = UnityEditor.EditorSnapSettings.move;
        coordinates.x = Mathf.RoundToInt(parentPosition.x / snapSettings.x);
        coordinates.y = Mathf.RoundToInt(parentPosition.z / snapSettings.z);
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
