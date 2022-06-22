using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collaborator : MonoBehaviour
{
    // ============= VARIABLES =============

    public GameObject ptcFusionPrefab;

    private ScoreManager scoreManager;
    private GameObject gameController;
    private string objectName = default;

    // =====================================


    private void Start()
    {
        if (GameObject.Find("PlayerScore") != null)
            scoreManager = GameObject.Find("PlayerScore").GetComponent<ScoreManager>();

        if (GameObject.Find("GameController") != null)
            gameController = GameObject.Find("GameController");

        SetLayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Fusion();
        }

        if (collision.gameObject.name.Contains("Collaborator"))
        {
            switch (gameObject.layer)
            {
                case 8:
                    if (collision.gameObject.layer == 10 || collision.gameObject.layer == 11)
                    {
                        Fusion();
                    }
                    break;
                case 9:
                    if (collision.gameObject.layer == 8)
                    {
                        Fusion();
                    }
                    break;
                case 10:
                    if (collision.gameObject.layer == 9)
                    {
                        Fusion();
                    }
                    break;
                case 11:
                    if (collision.gameObject.layer == 10 || collision.gameObject.layer == 9)
                    {
                        Fusion();
                    }
                    break;
                default:
                    print("Incorrect layer");
                    break;
            }
        }
    }

    private void SetLayer()
    {
        for (int i = 0; i < 4; i++)
        {
            objectName = "Collaborator" + i.ToString();

            if (GameObject.Find(objectName) == null)
            {
                gameObject.name = objectName;

                switch (i)
                {
                    case 0:
                        gameObject.layer = 8;
                        break;
                    case 1:
                        gameObject.layer = 9;
                        break;
                    case 2:
                        gameObject.layer = 10;
                        break;
                    case 3:
                        gameObject.layer = 11;
                        break;
                    default:
                        print("Incorrect collaborator");
                        break;
                }

                return;
            }
        }
    }

    private void Fusion()
    {
        GameObject ptcFusion;
        ptcFusion = Instantiate(ptcFusionPrefab, transform.position, Quaternion.identity);

        gameController.GetComponent<CameraShake>().Shake();

        scoreManager.IncreaseBonus();
        Destroy(transform.parent.gameObject);
    }
}
