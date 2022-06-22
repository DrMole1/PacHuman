using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Highscore : MonoBehaviour
{

    public void loadMenu() => StartCoroutine(iLoadMenu());
    IEnumerator iLoadMenu()
    {
        TransitionsSystem.Instance.CloseRid();
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("MAIN_MENU", LoadSceneMode.Single);
    }
}
