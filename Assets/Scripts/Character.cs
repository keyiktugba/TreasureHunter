using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public haritaoluþturucu haritaOlusturucuScripti; 
    private float minX, maxX, minY, maxY;
    private int gridSizeX;
    private int gridSizeY;
    public float moveSpeed = 5f;
    public LayerMask obstacleLayer; 

    private Rigidbody2D rb;
    private List<Vector2> path = new List<Vector2>(); 
    private int pathIndex = 0;

    private bool hasCollectedAllChests = false;
    public enum CharacterState
    {
        Idle = 0,
        TurningRight = 1,
        TurningLeft = 2,
        MovingUp = 3,
        MovingDown = 4
    }
    void Start()
    {
        minX = 0;
        maxX = PlayerPrefs.GetInt("HaritaBoyutu", 50);
        minY = 0;
        maxY = PlayerPrefs.GetInt("HaritaBoyutu", 50);

        Chest[] chests = FindObjectsOfType<Chest>();
        foreach (Chest chest in chests)
        {
            path.AddRange(chest.GetChestPositions());
        }

        rb = GetComponent<Rigidbody2D>();
        haritaoluþturucu mapGenerator = FindObjectOfType<haritaoluþturucu>();
        if (mapGenerator != null)
        {
            gridSizeX = (int)mapGenerator.maxX;
            gridSizeY = (int)mapGenerator.maxY; 
        }
        PlaceCharacterRandomly();
    }

    void Update()
    {
        if (!hasCollectedAllChests && pathIndex < path.Count)
        {
            MoveCharacter();
        }
        else
        {
            path.Clear();
            pathIndex = 0;
            hasCollectedAllChests = true; 
        }
    }

    private void ClampCharacterPosition()
    {
        
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        
        transform.position = new Vector2(clampedX, clampedY);
    }

    private void MoveCharacter()
    {
        Vector2 nextPosition = path[pathIndex];
        Vector2 movement = nextPosition - (Vector2)transform.position;
         if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
         {
             movement = new Vector2(Mathf.Sign(movement.x), 0f);
         }
        
         else
         {
             movement = new Vector2(0f, Mathf.Sign(movement.y));
         }

         if (movement != Vector2.zero)
         {
             movement = movement.normalized;
             rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
         }

         if (Vector2.Distance(transform.position, nextPosition) < 0.1f)
         {
             pathIndex++;
         }
        
    }
    void PlaceCharacterRandomly()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 randomStartPos = new Vector2(randomX, randomY);
        rb.position = randomStartPos;
    }
}

