using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_menu : MonoBehaviour
{
    public InputField boyutInputField;


    public void playGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.SetActive(true); 
        }
        else
        {
            Debug.LogError("Oyuncu atanmamýþ!");
            return;
        }

        SceneManager.LoadScene("New Game");
    }

    public void restartGame()
    { 
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (boyutInputField != null)
        {
           
            PlayerPrefs.SetInt("HaritaBoyutu", int.Parse(boyutInputField.text));
        }
        else
        {
            Debug.LogError("boyutInputField atanmamýþ!");
            return; 
        }
        if (player != null)
        {
            player.SetActive(false);
        }
        SceneManager.LoadScene("New Game");
    }

    public void settingsMenu()
    {
        SceneManager.LoadScene("Settings Menu");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void exitGame()
    {
        Application.Quit();
    }




}
