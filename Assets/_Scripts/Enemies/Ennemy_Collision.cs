using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ennemy_Collision : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        if (GameObject.Find("GameController") != null)
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(collision.gameObject.transform.parent.gameObject);

            if (SceneManager.GetActiveScene().name == "SCENE_RIDDLE01" || SceneManager.GetActiveScene().name == "SCENE_RIDDLE02" || SceneManager.GetActiveScene().name == "SCENE_RIDDLE03" || SceneManager.GetActiveScene().name == "SCENE_RIDDLE04" || SceneManager.GetActiveScene().name == "SCENE_RIDDLE05")
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            }
            else
            {
                gameController.Defeat();
            }
        }
        if (collision.gameObject.name == "Collaborator0" || collision.gameObject.name == "Collaborator1" || collision.gameObject.name == "Collaborator2" || collision.gameObject.name == "Collaborator3")
        {
            Destroy(collision.gameObject.transform.parent.gameObject);

            if (SceneManager.GetActiveScene().name == "SCENE_RIDDLE03" || SceneManager.GetActiveScene().name == "SCENE_RIDDLE04" || SceneManager.GetActiveScene().name == "SCENE_RIDDLE05")
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            }
        }
    }
}
