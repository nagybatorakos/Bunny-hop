using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text highscore;
    public Text coins;

    private void Start()
    {
        highscore.text = PlayerPrefs.GetInt("highscore", 0).ToString();
        coins.text = PlayerPrefs.GetInt("coins", 0).ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
        //SceneManager.UnloadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
