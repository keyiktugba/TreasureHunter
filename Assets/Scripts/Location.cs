using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost { get { return GCost + HCost; } }
    public Location Parent { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Vector2 Position { get; set; }
    public bool IsObstacle { get; set; }
    public List<Location> Neighbors { get; set; }

    public Location(Vector2 position, bool isObstacle = false)
    {
        Position = position;
        IsObstacle = isObstacle;
        Neighbors = new List<Location>();
    }

    public override bool Equals(object obj)
    {
        return obj is Location location &&
               X == location.X &&
               Y == location.Y;
    }
    public void AddNeighbor(Location neighbor)
    {
        Neighbors.Add(neighbor);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(Location a, Location b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Location a, Location b)
    {
        return !(a == b);
    }
}
