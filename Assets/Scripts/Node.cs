using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public Vector3 worldPosition;
    public bool walkable;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;
    int heapIndex;

    public Node(Vector3 _worldPosition, bool _walkable, int _gridX, int _gridY) {
        worldPosition = _worldPosition;
        walkable = _walkable;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost {
        get {
            return gCost + hCost;
        }
    }

    public int HeapIndex {
        get {
            return heapIndex;
        }
        set {
            heapIndex = value;
        }
    }

    public int CompareTo(Node other) {
        int compare = fCost.CompareTo(other.fCost);

        if (compare == 0) {
            compare = hCost.CompareTo(other.hCost);
        }
        return -compare;
    }
}
