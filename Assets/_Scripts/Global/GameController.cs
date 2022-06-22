using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // =========================== VARIABLES ===========================
    [Header("Names of Scenes")]
    public string NextSceneName;
    public string MenuSceneName;

    [Header("Objects in Scene")]
    public GameObject player;
    public GameObject[] ennemies;
    public GameObject bonusAtheo;
    public TimeManager timeManager;

    [Header("Prefab")]
    public GameObject collaboPrefab;

    [Header("Pannels")]
    public GameObject endPannel;
    public GameObject bonusPannel;
    public GameObject buttonsPannel;

    [Header("Image of Buttons")]
    public Image[] images;
    public Sprite spriteCollaborator;

    public bool isRiddle;

    private GameObject[] nodesRestantes;
    private Vector2 tf;
    private GameManager gameManager;

    // =================================================================

    private void Awake()
    {
        endPannel = GameObject.Find("EndPannel");
        bonusPannel = GameObject.Find("BonusPannel");
        
        timeManager = GameObject.Find("GameTime").GetComponent<TimeManager>();
    }

    private void Start()
    {
        if (GameObject.Find("GameManager") != null)
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        else
            print("There is no GameManager found");


        endPannel.SetActive(false);
        bonusPannel.SetActive(false);
        
    }

    /// <summary>
    /// Check the number of collectibles in the scene and load next level of they are 0
    /// </summary>
    public void CheckEndLevel()
    {
        nodesRestantes=GameObject.FindGameObjectsWithTag("Collectible");
        if (nodesRestantes.Length == 1)
        {
            GameObject a = GameObject.Find("Unlocker");
            if (a)
            {
                SetLockMode b = a.GetComponent<SetLockMode>();
                if(b)
                {
                    b.setLockModeOff();
                }
            }
                
            DestroyAllEnnemies();

            timeManager.TimeBonus();
        }
    }

    /// <summary>
    /// Start the coroutine of defeat
    /// </summary>
    public void Defeat()
    {
        StartCoroutine(EndGame());
    }

    /// <summary>
    /// Destroy player and active the end pannel
    /// </summary>
    /// <returns></returns>
    IEnumerator EndGame()
    {
        yield return null;
        endPannel.SetActive(true);
        buttonsPannel.SetActive(false);
        gameManager.TimeStop();
    }

    /// <summary>
    /// Back to the menu
    /// </summary>
    public void QuitGame()
    {
        gameManager.TimeContinue();
        StartCoroutine(iQuitGame());
        
    }
    
    IEnumerator iQuitGame()
    {
        TransitionsSystem.Instance.CloseRid();
        yield return new WaitForSeconds(1.3f);
        
        SceneManager.LoadScene("SCENE_SUBMITSCORE", LoadSceneMode.Single);
    }

    /// <summary>
    /// An ennemy will be a player
    /// </summary>
    public void Collaborate(int _ennemy)
    {
        if (ennemies[_ennemy] == null)
        {
            return;
        }

        gameManager.TimeContinue();

        tf = ennemies[_ennemy].transform.position;

        Destroy(ennemies[_ennemy].transform.parent.gameObject);

        GameObject collaborator;
        collaborator = Instantiate(collaboPrefab, tf, Quaternion.identity);
        collaborator.name = "Collab" + _ennemy;
        // collaborator.GetComponent<Player_Movement>().startingNode = tf;
        collaborator.transform.position = tf;

        Player_Movement mov = collaborator.GetComponentInChildren<Player_Movement>();

        addCollabToMovement(mov);

        bonusPannel.SetActive(false);

        if (!isRiddle)
        {
            bonusAtheo.GetComponent<SpawnBonus>().Spawn();
            //images[_ennemy].sprite = spriteCollaborator;
        }
        
        
    }
    private void addCollabToMovement(Player_Movement mov)
    {
        Button leftbtn = GameObject.Find("MovLeft").GetComponent<Button>();
        Button rightbtn = GameObject.Find("MovRight").GetComponent<Button>();
        Button upbtn = GameObject.Find("MovUp").GetComponent<Button>();
        Button downbtn = GameObject.Find("MovDown").GetComponent<Button>();
        
        
        if(mov)
        {
            if (leftbtn) leftbtn.onClick.AddListener(mov.Left);
            if (rightbtn) rightbtn.onClick.AddListener(mov.Right);
            if (upbtn) upbtn.onClick.AddListener(mov.Up);
            if (downbtn) downbtn.onClick.AddListener(mov.Down);
        }



    }

    /// <summary>
    /// Go to the next level after restant time is calculted as bonus score
    /// </summary>
    public void GoToNextLevel()
    {
        SceneManager.LoadScene(NextSceneName, LoadSceneMode.Single);
        gameManager.addRoom(1);
    }

    /// <summary>
    /// Destroy all enemies when player wins
    /// </summary>
    public void DestroyAllEnnemies()
    {
        for(int i = 0; i < ennemies.Length; i++)
        {
            if(ennemies[i] != null)
            {
                Destroy(ennemies[i].transform.parent.gameObject);
            }
        }
    }
}
