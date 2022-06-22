using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class ScenesManager : MonoBehaviour
{
    private static ScenesManager instance;

    

    //Scene's indexes
    public enum scenes
    {
        MAIN_MENU = 0,
        MODE_ARCADE = 1,
        MODE_RIDDLE = 6
        
    };

    private GameManager gameManager;


    private void Start()
    {
       
        if (GameObject.Find("GameManager") != null)
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public GameObject menuV1;
    public GameObject menuV2;
    public void loadMenuV2()
    {
        menuV1.SetActive(false);
        menuV2.SetActive(true);
    }
    public void loadRetour()
    {
        menuV1.SetActive(true);
        menuV2.SetActive(false);
    }


    public void LOAD_MENU()
    {
        SceneManager.LoadSceneAsync((int)scenes.MAIN_MENU, LoadSceneMode.Single);
    }

    public void LOAD_MENU_TO_ARCADE()
    {
        gameManager.resetScore();
        gameManager.resetRoom();
        StartCoroutine(loadArcade());

    }
    IEnumerator loadArcade()
    {
        TransitionsSystem.Instance.CloseRid();
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("SCENE_ARCADE01", LoadSceneMode.Single);
    }

    IEnumerator loadEnigme()
    {
        TransitionsSystem.Instance.CloseRid();
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("SCENE_CONTEXTE", LoadSceneMode.Single);
    }

    public void LoadHighScore() => StartCoroutine(iLoadHighscore());
    IEnumerator iLoadHighscore()
    {
        TransitionsSystem.Instance.CloseRid();
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("SCENE_HIGHSCORE", LoadSceneMode.Single);
    }

    public void LOAD_MENU_TO_ENIGMA()
    {
        //AudioManager.Instance.playAudioClip(1);
        gameManager.resetScore();
        gameManager.resetRoom();
        StartCoroutine(loadEnigme());
        
    }


    public void ExitGame()
    {
        Application.Quit(0);
    }


    public GameObject creditsParents;
    public void Credits()
    {
        creditsParents.SetActive(!creditsParents.activeSelf);
    }




}
