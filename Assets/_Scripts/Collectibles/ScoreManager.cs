using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //Multiplicateur de point pour chaque collectibles
    public int multiplicateurCollectible;
    //Multiplicateur de point pour chaque seconde restante après que le joueur ait recolté tout les collectibles
    public int multiplicateurTime = 50;
    //Multiplicateur supplémentaire
    public int bonusFactor = 1;

    private TextMeshProUGUI scoreText;

    private TextMeshProUGUI bonusText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        GameObject a = GameObject.Find("ScoreBonusText");
        if (a) bonusText = a.GetComponent<TextMeshProUGUI>();
        // Initialisation du score à 0
        //playerScore = 0;
        //affichage du score
        PrintScore();
    }

    //fonction permettant d'afficher le score
    private void PrintScore()
    {
        //affichage du score
        scoreText.text = GameManager.Instance.getScore().ToString();
    }

    //fonction permettant d'augmenter le score du joueur
    public void IncreaseScore(string increaseType)
    {
        //définition du calcule à faire en fonction de ce qui augmente le score
        switch (increaseType)
        {
            //si collectible
            case "Collectible":
                //augementation du score
                GameManager.Instance.addScore(multiplicateurCollectible * bonusFactor);
                //playerScore = playerScore + multiplicateurCollectible;
                break;
        }
        //affichage du score
        PrintScore();
    }

    public void AddScoreBonus()
    {
        GameManager.Instance.addScore(multiplicateurTime);
        //playerScore = playerScore + multiplicateurTime;
        PrintScore();
    }

    public void IncreaseBonus()
    {
        bonusFactor++;

        string bonus = "X" + bonusFactor.ToString();
        bonusText.text = bonus;

        switch (bonusFactor)
        {
            case 2:
                bonusText.color = new Color32(255, 255, 255, 255);
                break;
            case 3:
                bonusText.color = new Color32(135, 255, 0, 255);
                bonusText.fontSize = 50f;
                break;
            case 4:
                bonusText.color = new Color32(228, 0, 255, 255);
                bonusText.fontSize = 60f;
                break;
            case 5:
                bonusText.color = new Color32(255, 232, 0, 255);
                bonusText.fontSize = 70f;
                break;
            default:
                print("Incorrect bonus factor.");
                break;
        }
    }
}
