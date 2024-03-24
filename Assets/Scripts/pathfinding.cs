using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfinding : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public LayerMask obstacleLayer; 
    public LayerMask chestLayer; 
    private Grid grid;
    private float nodeRadius;
    
    void Start()
    {
        grid = GetComponent<Grid>();
        nodeRadius = grid.nodeRadius;
    }

    
    void Update()
    {
        FindPath(player.position, target.position);
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        Node startNode = grid.WorldPointToNode(startPos);
        Node targetNode = grid.WorldPointToNode(targetPos);

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                
                break;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);
    }

    
}
