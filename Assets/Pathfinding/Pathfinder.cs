using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int destinationCoordinates;
    public Vector2Int StartCoordinates => startCoordinates;
    public Vector2Int DestinationCoordinates => destinationCoordinates;
    
    private Node startNode;
    private Node destinationNode;
    private Node currentSearchNode;

    private Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    private Queue<Node> frontier = new Queue<Node>();

    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    private GridManager gridManager;

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager) {
            startNode = gridManager.GetNode(startCoordinates);
            destinationNode = gridManager.GetNode(destinationCoordinates);
        }
    }

    void Start() {
        GetNewPath(startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates) {
        gridManager.ResetNodes();
        BreadthFirstSearchFrom(coordinates);
        return BuildPath();
    }

    private void ExploreNeighbors() {
        List<Node> neighbors = new List<Node>();
        foreach (var direction in directions) {
            Node neighbor = gridManager.GetNode(currentSearchNode.coordinate + direction);
            if (neighbor != null) {
                neighbors.Add(neighbor);
            }
        }

        foreach (var neighbor in neighbors) {
            if (!reached.ContainsKey(neighbor.coordinate) && neighbor.isWalkable) {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinate, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearchFrom(Vector2Int coordinates) {
        Node currentStartingNode = gridManager.GetNode(coordinates);
        currentStartingNode.isWalkable = true;
        destinationNode.isWalkable = true;
        frontier.Clear();
        reached.Clear();
        
        bool isRunning = true;
        frontier.Enqueue(currentStartingNode);
        reached.Add(coordinates, currentStartingNode);

        while (frontier.Count > 0 && isRunning) {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if (currentSearchNode.coordinate.Equals(destinationCoordinates)) {
                isRunning = false;
            }
        }
    }

    private List<Node> BuildPath() {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;
        
        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null) {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates) {
        var node = gridManager.GetNode(coordinates);
        if (node != null) {
            bool previousState = node.isWalkable;
            node.isWalkable = false;
            List<Node> newPath = GetNewPath(startCoordinates);
            node.isWalkable = previousState;

            if (newPath.Count <= 1) {
                GetNewPath(startCoordinates);
                return true;
            }
        }
        return false;
    }

    public void NotifyRecievers() {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
