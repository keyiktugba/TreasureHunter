using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCtrl : MonoBehaviour
{
    public haritaolu�turucu haritaOlusturucu; // Unity Editor'den atay�n

    [SerializeField] private Transform targetObj;

    [SerializeField] private float smoothValue;
  
    private float minX, maxX, minY, maxY;


    private void Start()
    {
        if (targetObj == null)
        {
            Debug.LogError("L�tfen karakteri takip edecek bir nesne atay�n.");
            return;
        }
        
        minX = 0; 
        maxX = PlayerPrefs.GetInt("HaritaBoyutu", 50); 
        minY = 0 ; 
        maxY = PlayerPrefs.GetInt("HaritaBoyutu", 50);
    }

    private void LateUpdate()
    {
        if (targetObj != null)
        {
            Vector3 targetPos = new Vector3(
                Mathf.Clamp(targetObj.position.x, minX, maxX),
                Mathf.Clamp(targetObj.position.y, minY, maxY),
                transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPos, smoothValue * Time.deltaTime);
        }
    }
}