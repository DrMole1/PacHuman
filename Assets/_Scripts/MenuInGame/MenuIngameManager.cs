using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuIngameManager : MonoBehaviour
{
    public Sprite[] pauseSprites;
    public GameObject pauseButton;
    bool isPause;

    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
    }

    public void SetPause()
    {
        if (isPause)
        {
            Time.timeScale = 1;
            isPause = false;
            pauseButton.GetComponent<Image>().sprite = pauseSprites[0];
        }
        else
        {
            Time.timeScale = 0;
            isPause = true;
            pauseButton.GetComponent<Image>().sprite = pauseSprites[1];
        }
    }

    public void Quitter()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MAIN_MENU", LoadSceneMode.Single);
    }
}
