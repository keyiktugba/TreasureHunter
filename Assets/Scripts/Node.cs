using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public bool isChest; 
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public Node parent;

    public int fCost { get { return gCost + hCost; } }

    public Node(bool _walkable, bool _isChest, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        isChest = _isChest;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }
}
