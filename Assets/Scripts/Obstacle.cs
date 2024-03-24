using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected bool oyuncuGecis;
    protected int boyutX; 
    protected int boyutY; 

    protected haritaolu�turucu haritaOlusturucu;
    public GameObject engelPrefab;
    public int engelSayisi;
    public float minMesafe; 
    protected List<Vector2> obstaclePositions = new List<Vector2>();
    public virtual void Start()
    {
        haritaOlusturucu = FindObjectOfType<haritaolu�turucu>();

        for (int i = 0; i < engelSayisi; i++)
        {
            Olu�turEngel(engelPrefab);
        }
    }

    public virtual void Olu�turEngel(GameObject engelPrefab)
    {
        int haritaBoyutu = PlayerPrefs.GetInt("HaritaBoyutu", 50);

        while (true)
        {
           
            float randomX = Random.Range(1, haritaBoyutu);
            float randomY = Random.Range(1, haritaBoyutu);
            Vector2 randomPosition = new Vector2(randomX, randomY);

            
            bool mesafeUygun = true;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPosition, minMesafe);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Obstacle"))
                {
                    mesafeUygun = false;
                    break;
                }
            }

            if (mesafeUygun)
            {
                
                Instantiate(engelPrefab, randomPosition, Quaternion.identity);
                obstaclePositions.Add(randomPosition);
                break;

            }
        }
    }

}

