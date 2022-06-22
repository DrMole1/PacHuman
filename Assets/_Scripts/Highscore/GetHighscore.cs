using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class GetHighscore : MonoBehaviour
{

    public string highscoreURL;
    public GameObject[] nameObjectList;
    public GameObject[] scoreObjectList;
    private string contenuPage;
    private string[] playerList;
    private string[] resList;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetScores());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    IEnumerator GetScores()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(highscoreURL))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = highscoreURL.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                contenuPage = webRequest.downloadHandler.text;
                string[] stringSeparators = new string[] { "name :","score :" };
                playerList = contenuPage.Split(stringSeparators, StringSplitOptions.None);
                for (int i = 1; i < playerList.Length; i=i+2)
                {
                    nameObjectList[(int)(i/2)].GetComponent<TextMeshProUGUI>().text=playerList[i];
                    scoreObjectList[(int)(i/2)].GetComponent<TextMeshProUGUI>().text=playerList[i+1];
                }
            }
        }
    }
}
