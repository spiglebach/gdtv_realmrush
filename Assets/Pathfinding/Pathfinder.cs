using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int destinationCoordinates;
    
    private Node startNode;
    private Node destinationNode;
    private Node currentSearchNode;

    private Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    private Queue<Node> frontier = new Queue<Node>();

    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    private GridManager gridManager;

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
    }

    void Start() {
        startNode = gridManager.GetNode(startCoordinates);
        destinationNode = gridManager.GetNode(destinationCoordinates);
        BreadthFirstSearch();
        BuildPath();
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

    void BreadthFirstSearch() {
        bool isRunning = true;
        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

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
}
