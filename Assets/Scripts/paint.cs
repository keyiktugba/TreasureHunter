using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paint : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = waypoints.Count;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = true;
        lineRenderer.sortingOrder = 2;
    }

    private void Update()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            lineRenderer.SetPosition(i, waypoints[i].position);
        }
    }
}
