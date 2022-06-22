using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCollectible : MonoBehaviour
{
    public GameObject ptcDataPrefab;

    //objet permettant d'afficher le score du joueur
    private ScoreManager scoreText;

    //objet permettant de gérer le niveau
    private GameController gameController;

    // AudioManager pour produire un son lorsque le joueur passe sur le collectible
    private AudioManager audioManager;

    private Collectibles parentCol;

    private void Start()
    {

        if (GameObject.Find("GameManager") != null)
            audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();

        //initialiser l'objet scoreText
        scoreText = GameObject.Find("PlayerScore").GetComponent<ScoreManager>();
        //initialiser l'objet gameController
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        parentCol = transform.parent.GetComponent<Collectibles>();

    }

    //en cas de collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si collision avec le joueur
        if (collision.gameObject.tag == "Player")
        {
            //Joue le son de pickup de collectible
            audioManager.playAudioClip(0);

            //Instantiation de particules
            GameObject ptcData;
            ptcData = Instantiate(ptcDataPrefab, transform.position, Quaternion.identity);

            ////initialiser l'objet scoreText
            if (scoreText != null && gameController != null)
            {
                //Ancienne version ====================
                //scoreText.GetComponent<ScoreManager>().IncreaseScore("Collectible");
                //gameController.GetComponent<GameController>().CheckEndLevel();
                //transform.parent.GetComponent<Collectibles>().SetCollectiblesToCatch(transform.parent.GetComponent<Collectibles>().GetCollectiblesToCatch() -1);
                //=====================================

                //Partie fonctionnelle 
                scoreText.IncreaseScore("Collectible");
                gameController.CheckEndLevel();
                int value = parentCol.GetCollectiblesToCatch() - 1;
                parentCol.SetCollectiblesToCatch(value);
                Destroy(this.gameObject);
            }
            else
            {
                if (scoreText == null)
                {
                    Debug.Log("Pas de texte pour le score.");
                }
                if (gameController == null)
                {
                    Debug.Log("Pas de gameController.");
                }

            }
        }
    }
}
