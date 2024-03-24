using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Chest;
using UnityEngine.VFX;

public class CharContr : MonoBehaviour
{
    public float interactionRadius = 2f; 
    public haritaoluþturucu haritaOlusturucuScripti; 
    private float minX, maxX, minY, maxY;
    private Vector2 startPosition;
    public Animator anim;
    private GameObject[] chests;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private Vector2 movement;
    private Grid grid;
    private Character.CharacterState currentState = Character.CharacterState.Idle;
    private bool hasCollectedAllChests = false;
    public LayerMask chestLayer;
    public GameObject greenDotPrefab;
    public VisualEffect vfxRenderer;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        minX = 0;
        maxX = PlayerPrefs.GetInt("HaritaBoyutu", 50);
        minY = 0;
        maxY = PlayerPrefs.GetInt("HaritaBoyutu", 50);
        startPosition = transform.position;
        
        grid = FindObjectOfType<Grid>();
        FindChests();
        
    }
    void FindChests()
    {
        chests = GameObject.FindGameObjectsWithTag("Chest");
    }
    GameObject FindNearestChest()
    {
        GameObject nearestChest = null;
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject chest in chests)
        {
            float distance = Vector2.Distance(transform.position, chest.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestChest = chest;
            }
        }
        return nearestChest;
    }

    void Update()
    {
        if (!hasCollectedAllChests)
        {
            GameObject nearestChest = FindNearestChest();
            if (nearestChest != null)
            {
                MoveTowardsChest(nearestChest.transform.position);
                float distanceToChest = Vector2.Distance(transform.position, nearestChest.transform.position);
                if (distanceToChest <= interactionRadius && distanceToChest <= 0.5f)
                {
                    nearestChest.GetComponent<Chest>().Collect();
                    UpdateChests();
                }
            }
            else
            {
                hasCollectedAllChests = true;
            }
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        UpdateCharacterState();
        vfxRenderer.SetVector3("colliderpos",transform.position);

    }


    void MoveTowardsChest(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }


    void FixedUpdate()
    {
       
        rb.AddForce(movement.normalized * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);

        
        rb.position = new Vector2(
            Mathf.Clamp(rb.position.x, minX, maxX),
            Mathf.Clamp(rb.position.y, minY, maxY)
        );
        DrawGreenDot(rb.position);
        UpdateAnimator();

    }
    void DrawGreenDot(Vector2 position)
    {
      
        Vector3 newPosition = position;
        newPosition.z = 0; 

        
        GameObject greenDot = Instantiate(greenDotPrefab, newPosition, Quaternion.identity);
        
        greenDot.name = "GreenDot";
    }



    void UpdateCharacterState()
    {
        if (movement.x > 0)
            currentState = Character.CharacterState.TurningRight;
      
        else if (movement.x < 0)
            currentState = Character.CharacterState.TurningLeft;
        
        else if (movement.y > 0)
            currentState = Character.CharacterState.MovingUp;
        
        else if (movement.y < 0)
                currentState = Character.CharacterState.MovingDown;
        
        else
                currentState = Character.CharacterState.Idle;
        }

  

    void UpdateAnimator()
    {
        anim.SetInteger("CharacterState", (int)currentState);
    }
   
    void UpdateChests()
    {
        FindChests();
    }
  

}
