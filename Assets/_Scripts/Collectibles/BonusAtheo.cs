using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusAtheo : MonoBehaviour
{
    public GameObject ptcBonusPrefab;

    private GameObject bonusPannel;
    private GameManager gameManager;
    private bool canBeCatch = false;

    private void Awake()
    {

    }

    private void Start()
    {
        if (GameObject.Find("Canvas") != null)
            bonusPannel = GameObject.Find("Canvas").transform.GetChild(2).gameObject;

        if (GameObject.Find("GameManager") != null)
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        else
            print("There is no GameManager found");

        StartCoroutine(BeCatch());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canBeCatch)
        {
            // Implementation of animated and flashy particles
            GameObject ptcBonus;
            ptcBonus = Instantiate(ptcBonusPrefab, new Vector2(8f, -1f), Quaternion.identity);
            Destroy(ptcBonus, 8f);

            bonusPannel.SetActive(true);
            Destroy(gameObject);
            gameManager.TimeStop();
        }
    }

    IEnumerator BeCatch()
    {
        yield return new WaitForSeconds(1f);
        canBeCatch = true;
    }
}
