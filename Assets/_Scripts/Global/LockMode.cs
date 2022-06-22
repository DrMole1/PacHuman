using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockMode : MonoBehaviour
{
    public GameObject Lock;
    public Sprite unlockedSprite;

    public Button button;

    public 
    
    void Start()
    {
        if(PlayerPrefs.GetInt("TrainingDone") == 0)
        {
            Lock.SetActive(true);
            button.enabled = false;
        }
        else
        {
            Lock.SetActive(false);
            button.enabled = true;
            button.gameObject.GetComponent<Image>().overrideSprite = unlockedSprite;
        }
    }
    
}
