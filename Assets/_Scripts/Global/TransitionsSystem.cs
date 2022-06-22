using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionsSystem : MonoBehaviour
{
    public static TransitionsSystem Instance;
    
    private void Awake()
    {
        Instance = this;
        if (Instance == null)
        {
            Instance = GameObject.FindObjectOfType<TransitionsSystem>();
            if (Instance == null)
            {
                GameObject container = new GameObject("Transitions System");
                Instance = container.AddComponent<TransitionsSystem>();
            }
        }

        logoAnim = logo.GetComponent<Animator>();

        
        
    }

    public void LoadSceneX(int x)
    {
        string nom = "SCENE_ARCADE0" + x.ToString();
        SceneManager.LoadScene(nom, LoadSceneMode.Single);
        
    }

    public void LoadEnigme(int x)
    {
        string nom = "SCENE_RIDDLE0" + x.ToString();
        SceneManager.LoadScene(nom, LoadSceneMode.Single);
    }
   

    public GameObject ridA;
    public GameObject ridA_open;
    private Vector2 originalPosA;

    public GameObject ridB;
    public GameObject ridB_open;
    private Vector2 originalPosB;


    public float speed;
    public float interval = 1f;
    public float openingDelay = .7f;

    public GameObject logo;
    private Animator logoAnim;
    private Vector2 logoOriginalPos;

    public bool animAtStart = true;


    private void Start()
    {
        
        originalPosA = ridA.transform.position;
        originalPosB = ridB.transform.position;
        logoOriginalPos = logo.transform.position;

        if (animAtStart)
        {
            OpenRid();
        }
        else
        {
            ridA.transform.position = ridA_open.transform.position;
            ridB.transform.position = ridB_open.transform.position;
        }

    }

    IEnumerator iPlayAnim()
    {
        yield return new WaitForSeconds(0.3f);
        logoAnim.SetTrigger("Play");
        yield return new WaitForSeconds(2f);
        logoAnim.ResetTrigger("Play");
        logo.transform.position = logoOriginalPos;
    }

    public void playAnim()
    {
        StartCoroutine(iPlayAnim());
    }

    /// <summary>
    /// Use this to open curtains
    /// </summary>
    public void OpenRid()
    {
        StartCoroutine(iOpenRid());
    }

    /// <summary>
    /// Use this to close curtains
    /// </summary>
    public void CloseRid()
    {
        StartCoroutine(iCloseRid());
        playAnim();
    }

    public IEnumerator iOpenRid()
    {
        ridA.transform.position = originalPosA;
        ridB.transform.position = originalPosB;

        yield return new WaitForSeconds(openingDelay);
        while(Vector2.Distance(ridA.transform.position, ridA_open.transform.position) > 0.1f)
        {
            ridA.transform.position = Vector2.MoveTowards(ridA.transform.position, ridA_open.transform.position, speed);
            ridB.transform.position = Vector2.MoveTowards(ridB.transform.position, ridB_open.transform.position, speed);
            yield return null;
        }
    }

    public IEnumerator iCloseRid()
    {
        ridA.transform.position = ridA_open.transform.position;
        ridB.transform.position = ridB_open.transform.position;

        while (Vector2.Distance(ridA.transform.position, originalPosA) > 0.1f)
        {
            ridA.transform.position = Vector2.MoveTowards(ridA.transform.position, originalPosA, speed);
            ridB.transform.position = Vector2.MoveTowards(ridB.transform.position, originalPosB, speed);
            yield return null;
        }
    }


}
