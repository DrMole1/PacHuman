using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public int timeToStart = 300;
    public GameController gameController;
    public ScoreManager scoreManager;

    [HideInInspector] public bool gameIsFinished = false;

    private int actualTime = 300;


    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        actualTime = timeToStart;

        //affichage du temps
        PrintTime();

        StartCoroutine(Time());
    }

    //fonction permettant d'afficher le temps
    private void PrintTime()
    {
        //affichage du temps
        float minutes = Mathf.Floor(actualTime / 60);
        float secondes = Mathf.RoundToInt(actualTime % 60);

        //this.GetComponent<TextMeshProUGUI>().text = actualTime.ToString();
        this.GetComponent<TextMeshProUGUI>().text = minutes.ToString() + ":" + secondes.ToString();
    }

    //fonction public permettant de récupérer le temps
    public int GetTime()
    {
        //envoie du temps
        return actualTime;
    }

    IEnumerator Time()
    {
        yield return new WaitForSeconds(1f);


        if(actualTime != 0)
        {
            actualTime--;

            PrintTime();

            if(!gameIsFinished)
            {
                StartCoroutine(Time());
            }
        }
        else
        {
            gameController.Defeat();
        }  
    }

    /// <summary>
    /// Stop the timer and add the time as bonus
    /// </summary>
    public void TimeBonus()
    {
        gameIsFinished = true;

        StartCoroutine(CalculBonusScore());
    }

    IEnumerator CalculBonusScore()
    {
        yield return new WaitForSeconds(0.005f);


        if (actualTime != 0)
        {
            actualTime--;

            PrintTime();

            scoreManager.AddScoreBonus();

            StartCoroutine(CalculBonusScore());
        }
        else
        {
            gameController.GoToNextLevel();
        }
    }
}
