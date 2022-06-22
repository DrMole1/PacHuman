using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    //Instance
    public static GameManager Instance;

    public enum gameState
    {
        Running,
        Paused
    };

    public gameState STATE = gameState.Running;

    private int score = 0;
    [SerializeField] private int room = 1;

    //Setup
    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            if (Instance == null)
            {
                Instance = GameObject.FindObjectOfType<GameManager>();
                if (Instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    Instance = container.AddComponent<GameManager>();
                }
            }
            DontDestroyOnLoad(gameObject);
        }
        
        
    }

    private void Start()
    {
        
    }

    public int getScore()
    {
        return score;
    }
    public void resetScore()
    {
        score = 0;
    }
    public void addScore(int index)
    {
        score += index;
    }

    public int getRoom()
    {
        return room;
    }
    public void resetRoom()
    {
        room = 1;
    }
    public void addRoom(int index)
    {
        room += index;
    }

    /// <summary>
    /// Stop the time of game : players and ennemies dont move anymore
    /// </summary>
    public void TimeStop()
    {
        STATE = gameState.Paused;

        Time.timeScale = 0;
    }

    /// <summary>
    /// Time continues to work : players and ennemies continue to move
    /// </summary>
    public void TimeContinue()
    {
        STATE = gameState.Running;

        Time.timeScale = 1;
    }

    






}
