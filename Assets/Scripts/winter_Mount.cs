 using UnityEngine;
public class winter_Mount : Mount
{
    private int eklenenEngelSayisi = 0; 

    public override void Start()
    {
        base.Start();
        engelPrefab = Resources.Load<GameObject>("winter_mount");
        engelSayisi = 1;
        minMesafe = 2.0f;
    }

    public override void OluþturEngel(GameObject engelPrefab)
    {
        int haritaBoyutu = PlayerPrefs.GetInt("HaritaBoyutu", 50);

        while (eklenenEngelSayisi < engelSayisi)
        {
            
            float randomX = Random.Range(6, haritaBoyutu / 2 - 6);
            
            float randomY = Random.Range(6 ,haritaBoyutu- 6);
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
                
                Collider2D[] collidersAtPosition = Physics2D.OverlapCircleAll(randomPosition, 0.1f);
                if (collidersAtPosition.Length == 0)
                {
                    
                    Instantiate(engelPrefab, randomPosition, Quaternion.identity);
                    eklenenEngelSayisi++; 
                }
            }
        }
    }
}
