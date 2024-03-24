using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class newgbscript : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("Level_1");

    }

    public void butmenu()
    {
        SceneManager.LoadScene("butt");
    }
}
