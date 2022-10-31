using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;
    static PathRequestManager instance;
    Pathfinding pathfinding;
    bool isProcessingPath;

    void Awake () {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector3 startPosition, Vector3 endPosition, Action<Vector3[], bool> callback, bool flying) {
        PathRequest pathRequest = new PathRequest(startPosition, endPosition, callback, flying);
        instance.pathRequestQueue.Enqueue(pathRequest);
        instance.TryProcessingNext();
    } 

    void TryProcessingNext() {
        if (!isProcessingPath && pathRequestQueue.Count > 0) {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartPath(currentPathRequest.startPosition, currentPathRequest.endPosition);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success) {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessingNext();
    }

    struct PathRequest {
        public Vector3 startPosition;
        public Vector3 endPosition;
        public Action<Vector3[], bool> callback;
        bool flying;

        public PathRequest(Vector3 _startPosition, Vector3 _endPosition, Action<Vector3[], bool> _callback, bool _flying) {
            startPosition = _startPosition;
            endPosition = _endPosition;
            callback = _callback;
            flying = _flying;
        }
    }
}
