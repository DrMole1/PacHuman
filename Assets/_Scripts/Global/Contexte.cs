using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Contexte : MonoBehaviour
{
    public Transform text;
    private bool isChecked = false;
    private int incr = 0;

    void Start()
    {
        StartCoroutine(ScrollText());
    }


    IEnumerator ScrollText()
    {
        text.position = new Vector2(text.position.x, text.position.y + 2.2f);
        incr++;

        yield return new WaitForSeconds(0.02f);

        if(incr >= 620 && isChecked == false)
        {
            isChecked = true;
            StartCoroutine(loadEnigme());
        }

        StartCoroutine(ScrollText());
    }

    public void Pass()
    {
        isChecked = true;
        StartCoroutine(loadEnigme());
    }

    IEnumerator loadEnigme()
    {
        TransitionsSystem.Instance.CloseRid();
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("SCENE_RIDDLE01", LoadSceneMode.Single);
    }
}
