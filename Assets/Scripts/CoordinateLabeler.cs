using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour {
    private TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int();

    private void Awake() {
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
    }

    void Update() {
        if (Application.isPlaying) return;
        // in edit mode
        DisplayCoordinates();
        UpdateObjectName();
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
}
