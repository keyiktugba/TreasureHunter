
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class   Dynamic_obstacle: Obstacle
{
    private haritaoluþturucu haritaOlusturucu;
    public GameObject waysPrefab;
    public int wayCount;
    public int maxFlyingPerWay = 1;
    public float minDistance;
    public GameObject engelprefab;
    public List<Transform> waypoints = new List<Transform>();
    public virtual void Start()
    {
        haritaOlusturucu = FindObjectOfType<haritaoluþturucu>();

        for (int i = 0; i < wayCount; i++)
        {
            MakeWay(waysPrefab, maxFlyingPerWay);
        }
    }

    public virtual void MakeWay(GameObject waysPrefab, int maxFlyings)
    {
        int haritaBoyutu =PlayerPrefs.GetInt("HaritaBoyutu", 50);

        Vector2 randomPosition = GetRandomPosition(haritaBoyutu);

        GameObject newWay = Instantiate(waysPrefab, randomPosition, Quaternion.identity);

      
        CreateOnWay(newWay, maxFlyings);
        paint wayPath = newWay.AddComponent<paint>();
        wayPath.waypoints = new List<Transform>(newWay.GetComponentsInChildren<Transform>());

        bool mesafeUygun = CheckDistance(randomPosition);

        if (mesafeUygun)
        {
            Collider2D[] collidersAtPosition = Physics2D.OverlapCircleAll(randomPosition, 0.1f);

            if (collidersAtPosition.Length == 0)
            {
                Instantiate(waysPrefab, randomPosition, Quaternion.identity).GetComponent<Dynamic_obstacle>();
            }
        }
    }

    public virtual Vector2 GetRandomPosition(int haritaBoyutu)
    {
        float randomX = Random.Range(1, haritaBoyutu - 1);
        float randomY = Random.Range(1, haritaBoyutu - 1);
        return new Vector2(randomX, randomY);
    }

    public virtual void CreateOnWay(GameObject way, int maxFlying)
    {
        for (int i = 0; i < maxFlying; i++)
        {
            
            Vector3 randomPosition = GetRandomPositionOnWay(way);

           
            GameObject newEngel = Instantiate(engelPrefab, randomPosition, Quaternion.identity, way.transform);

          
            hareketn Movement = newEngel.GetComponent<hareketn>();
            if (Movement != null)
            {
                Movement.wayPoints = way.GetComponentsInChildren<Transform>();
                Movement.Start();
            }
        }
       
    }

    public virtual Vector3 GetRandomPositionOnWay(GameObject way)
    {
        
        Vector3 randomPosition = way.transform.position + Random.insideUnitSphere * 5f;
        randomPosition.z = 0f; 
        return randomPosition;
    }

    public virtual bool CheckDistance(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, minDistance);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Chest") || collider.CompareTag("Obstacle"))
            {
                return false;
            }
        }

        return true;
    }
}