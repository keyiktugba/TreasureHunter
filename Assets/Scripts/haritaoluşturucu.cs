using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class haritaoluşturucu : MonoBehaviour
{
    
    
    public float minX = 0;
    public float maxX;
    public float minY = 0;
    public float maxY;

    public GameObject yazObjesi;
    public GameObject kisObjesi;

    public void Start()
    {
       
        
       

       int  haritaBoyutu = PlayerPrefs.GetInt("HaritaBoyutu", 50);
        maxX = haritaBoyutu;
        maxY = haritaBoyutu;
        Debug.Log($"minX: {minX}, maxX: {maxX}, minY: {minY}, maxY: {maxY}");
        HaritaOlustur(haritaBoyutu);

    }

    public void HaritaOlustur(int boyut)
    {
       
        if (boyut <= 0)
        {
            Debug.LogError("Harita boyutu sıfırdan büyük olmalıdır.");
            return;
        }

       
        var kisBolgesi = new List<GameObject>();
        var yazBolgesi = new List<GameObject>();

        
        for (int i = 0; i < boyut; i++)
        {
            for (int j = 0; j < boyut; j++)
            {
                
                if (i < boyut / 2)
                {
                    yazBolgesi.Add(OlusturEngel(i, j, yazObjesi)); 
                }
                else
                {
                    kisBolgesi.Add(OlusturEngel(i, j, kisObjesi)); 
                }
            }
        }

        
        HaritayaEngelleriYerlestir(kisBolgesi);
        HaritayaEngelleriYerlestir(yazBolgesi);
    }

    GameObject OlusturEngel(int x, int y, GameObject prefab)
    {
       
        float posX = x + 0.5f;
        float posY = y + 0.5f;
        Vector2 position = new Vector2(posX, posY);

        
        return Instantiate(prefab, position, Quaternion.identity);
    }

    void HaritayaEngelleriYerlestir(List<GameObject> engelListesi)
    {
        
    }

 
}
