using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{

    // =============== VARIABLES ===============

    public static Collectibles instance;
    public GameObject collectiblePrefab;
    public bool isRiddle;
    public Transform[] riddleCollectibleSpawn;

    private GameObject Nodes;
    [SerializeField] private int collectiblesMax;
    [SerializeField] private int collectiblesToCatch;
    private GameObject player;

    // =========================================



    /// <summary>
    /// Initialization of the instance
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.SpawnCollectibles();
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object !");
            Destroy(this);
        }
    }

    /// <summary>
    /// Spawn several collectibles objets and set the variable collectibles of the instance
    /// </summary>
    public void SpawnCollectibles()
    {
        if (!isRiddle)
        {
            instance.Nodes = GameObject.Find("Nodes");
            player = GameObject.FindGameObjectWithTag("Player");

            int nNodes = instance.Nodes.transform.childCount;
            SetCollectiblesMax(nNodes);
            SetCollectiblesToCatch(nNodes);

            Vector2 pos = new Vector2(0f, 0f);
            for (int i = 0; i < nNodes; i++)
            {
                if (player != null)
                {
                    if (instance.Nodes.transform.GetChild(i).transform != player.GetComponent<Player_Movement>().startingNode)
                    {
                        pos = instance.Nodes.transform.GetChild(i).position;

                        GameObject collectible;
                        collectible = Instantiate(collectiblePrefab, pos, Quaternion.identity);
                        collectible.name = "Collectible " + i;
                        collectible.transform.SetParent(GameObject.Find("Collectibles").transform);
                    }
                }
                else
                {
                    Debug.Log("Pas de joueur dans le niveau.");
                }

            }
        }
        else
        {
            SetCollectiblesMax(riddleCollectibleSpawn.Length);
            SetCollectiblesToCatch(riddleCollectibleSpawn.Length);

            Vector2 pos = new Vector2(0f, 0f);
            for (int i = 0; i < riddleCollectibleSpawn.Length; i++)
            {

                pos = riddleCollectibleSpawn[i].position;

                GameObject collectible;
                collectible = Instantiate(collectiblePrefab, pos, Quaternion.identity);
                collectible.name = "Collectible " + i;
                collectible.transform.SetParent(GameObject.Find("Collectibles").transform);

            }
        }
        
    }




    /// <summary>
    /// Setter of collectibles Max
    /// </summary>
    /// <param name="_collectiblesMax">The number of collectibles the player must catch</param>
    public void SetCollectiblesMax(int _collectiblesMax)
    {
        instance.collectiblesMax = _collectiblesMax;
    }

    /// <summary>
    /// Getter of collectibles Max
    /// </summary>
    /// <returns></returns>
    public int GetCollectiblesMax()
    {
        return instance.collectiblesMax;
    }

    /// <summary>
    /// Setter of collectibles to catch
    /// </summary>
    /// <param name="_collectibles">The number of collectibles the player must catch</param>
    public void SetCollectiblesToCatch(int _collectibles)
    {
        instance.collectiblesToCatch = _collectibles;
    }

    /// <summary>
    /// Getter of collectibles to catch
    /// </summary>
    /// <returns></returns>
    public int GetCollectiblesToCatch()
    {
        return instance.collectiblesToCatch;
    }
}
