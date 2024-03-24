using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chest : MonoBehaviour, IInventory
{
    haritaolu�turucu haritaOlusturucu;
    public GameObject chestPrefab;
    public int chestCount;
    public float minDistance;
    public static List<Vector2> chestPositions = new List<Vector2>();

    public enum ChestType
    {
        Bronze,
        Silver,
        Gold,
        Diamond
    }

    public int cashValue = 1;

    public ChestType chestType;
    private static List<(ChestType type, Vector2 position)> collectedChests = new List<(ChestType, Vector2)>();
    public int Money { get; set; }


    void Start()
    {
        haritaOlusturucu = FindObjectOfType<haritaolu�turucu>();

        for (int i = 0; i < chestCount; i++)
        {
            MakeChest(chestPrefab);
        }
    }

    private void SortCollectedChests()
    {
        var sortedChestTypes = collectedChests
            .GroupBy(chest => chest.type)
            .OrderByDescending(group => group.Key)
            .Select(group => new { Type = group.Key, Count = group.Count() });

        Debug.Log("S�ralanm�� sand�k t�rleri ve say�lar�:");
        foreach (var chestType in sortedChestTypes)
        {
            Debug.Log($"{GetChestTypeName(chestType.Type)}: {chestType.Count} adet");
        }
    }


    public List<Vector2> GetChestPositions()
    {
        return chestPositions;
    }

    protected void MakeChest(GameObject chestPrefab)
    {
        int haritaBoyutu = PlayerPrefs.GetInt("HaritaBoyutu", 50);

        while (true)
        {
            float randomX = Random.Range(1, haritaBoyutu);
            float randomY = Random.Range(1, haritaBoyutu);
            Vector2 randomPosition = new Vector2(randomX, randomY);

            bool mesafeUygun = true;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPosition, minDistance);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Chest") || collider.CompareTag("Obstacle"))
                {
                    mesafeUygun = false;
                    break;
                }
            }

            if (mesafeUygun)
            {
                Collider2D[] collidersAtPosition = Physics2D.OverlapCircleAll(randomPosition, 0.1f);
                if (collidersAtPosition.Length == 0)
                {
                    ChestType randomType = (ChestType)Random.Range(0, System.Enum.GetValues(typeof(ChestType)).Length);
                    GameObject newChestObject = Instantiate(chestPrefab, randomPosition, Quaternion.identity);
                    Chest newChest = newChestObject.GetComponent<Chest>();
                    newChest.chestType = randomType;
                    chestPositions.Add(randomPosition);
                    collectedChests.Add((randomType, randomPosition));
                    break;
                }
            }
        }
    }

    public void Collect()
    {
        gameObject.SetActive(false);
        collectedChests.Add((chestType, transform.position));

        Debug.Log($"Sand�k topland�: {GetChestTypeName(chestType)} sand�k topland�! {transform.position} konumunda bulundu.");

        if (collectedChests.Count == 20)
        {
            SortCollectedChests();
        }
    }

    private string GetChestTypeName(ChestType type)
    {
        switch (type)
        {
            case ChestType.Bronze:
                return "Bak�r";
            case ChestType.Silver:
                return "G�m��";
            case ChestType.Gold:
                return "Alt�n";
            case ChestType.Diamond:
                return "Elmas";
            default:
                return "Bilinmeyen";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IInventory inventory = other.GetComponent<IInventory>();

            if (inventory != null)
            {
                inventory.Money += cashValue;
                Vector3 cashPosition = transform.position;
                gameObject.SetActive(false);
            }
        }
    }
}
