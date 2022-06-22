using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
    // =============== VARIABLES ===============

    public float delayToSpawn = 15f;
    public Transform Nodes;
    public GameObject BonusAtheoPrefab;
    public bool posIsChecked = false;
    public GameObject Collectibles;
    public int nEnnemy = 4;

    private int nSpawn = 0;

    // =========================================

    private void Start()
    {
        StartCoroutine(WaitToSpawn());
    }


    /// <summary>
    /// Accessible method to spawn the bonus
    /// </summary>
    public void Spawn()
    {
        if(nSpawn == nEnnemy)
        {
            return;
        }

        StartCoroutine(WaitToSpawn());
    }


    /// <summary>
    /// Coroutine to wait of spawning a bonus in the map after a delay
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(delayToSpawn);

        if(Collectibles.GetComponent<Collectibles>().GetCollectiblesMax() != Collectibles.GetComponent<Collectibles>().GetCollectiblesToCatch())
        {
            posIsChecked = false;
            CheckPosition();
        }
        else
        {
            StartCoroutine(WaitToSpawn());
        }
    }


    /// <summary>
    /// Find a position that's free to use for spawning a bonus
    /// </summary>
    private void CheckPosition()
    {
        int choosedNode = 0;

        do
        {
            choosedNode = UnityEngine.Random.Range(0, Nodes.childCount);

            if(Nodes.GetChild(choosedNode).gameObject.GetComponent<Node>().wasPassed)
            {
                posIsChecked = true;
            }

        } while (posIsChecked == false);
        

        Vector2 posToSpawn = Nodes.GetChild(choosedNode).position;

        SpawnBonusAtPosition(posToSpawn);
    }

    /// <summary>
    /// Spawn the bonus in the free position
    /// </summary>
    /// <param name="_posToSpawn"></param>
    private void SpawnBonusAtPosition(Vector2 _posToSpawn)
    {
        nSpawn++;

        GameObject bonusAtheo;
        bonusAtheo = Instantiate(BonusAtheoPrefab, _posToSpawn, Quaternion.identity);
        bonusAtheo.transform.SetParent(gameObject.transform);
    }
}
