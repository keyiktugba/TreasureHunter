using UnityEngine;
using UnityEngine.UI;

public class maptext : MonoBehaviour
{
    public InputField boyutInputField;

    public void HaritayiOlustur()
    {
        int haritaBoyutu;
        if (int.TryParse(boyutInputField.text, out haritaBoyutu))
        {
            PlayerPrefs.SetInt("HaritaBoyutu", haritaBoyutu); 
            PlayerPrefs.Save(); 
        }
        else
        {
            Debug.LogError("Geçersiz harita boyutu.");
        }
    }
}
