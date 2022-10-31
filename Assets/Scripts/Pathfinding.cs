using System.Runtime.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Transform player, target;
    PathRequestManager requestManager;
    Grid grid;

    void Awake() {
        grid = GetComponent<Grid>();
        requestManager = GetComponent<PathRequestManager>();
    }

    // void Update() {
    //     FindPath(player.position, target.position);
    // }

    public void StartPath(Vector3 startPosition, Vector3 endPosition) {
        StartCoroutine(FindPath(startPosition,endPosition));
    }

    IEnumerator FindPath(Vector3 startPosition, Vector3 endPosition) {

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.GetNodeFromWorldPosition(startPosition);
        startNode.gCost = 0;
        startNode.parent = null;
        Node endNode = grid.GetNodeFromWorldPosition(endPosition);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0) {
            Node current = openSet[0];
            
            for (int i = 1; i < openSet.Count; i++) {
                if (openSet[i].fCost < current.fCost || (openSet[i].fCost == current.fCost && openSet[i].hCost < current.hCost)) {
                    current = openSet[i];
                }
            }

            openSet.Remove(current);
            closedSet.Add(current);

            if (current == endNode) {
                pathSuccess = true;
                RetracePath(startNode, endNode);
                break;
            }

            List<Node> neighbors = grid.GetNodeNeighbors(current);
            
            foreach (Node node in neighbors) {
                if ((!node.walkable && !node || closedSet.Contains(node)) {
                    continue;
                }

                int newMovementCostToNeighbor = current.gCost + GetDistance(current, node);
                if (newMovementCostToNeighbor < node.gCost || !openSet.Contains(node)) {
                    node.gCost = newMovementCostToNeighbor;
                    node.hCost = GetDistance(node, endNode);
                    node.parent = current;

                    if (!openSet.Contains(node)) {
                        openSet.Add(node);
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess) {
            waypoints = RetracePath(startNode, endNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    int GetDistance(Node nodeA, Node nodeB) {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY) {
            return 14 * dstY + 10 * (dstX - dstY);
        } else {
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }

    Vector3[] RetracePath(Node startNode, Node endNode) {
        List<Vector3> path = new List<Vector3>();
        List<Node> nodePath = new List<Node>();
        Node current = endNode;
        //Debug.Log("Hello");

        while(current != startNode) {
            path.Add(current.worldPosition);
            nodePath.Add(current);
            current = current.parent;
        }
        path.Reverse();

        grid.path = nodePath;
        Vector3[] waypoints = path.ToArray();
        Array.Reverse(waypoints);
        return waypoints;
    }

    Node FindLowestF(List<Node> list) {
        Node current = list[0];

        foreach (Node n in list) {
            if (n.gCost < current.gCost) {
                current = n;
            }
        }

        return current;
    }

}
