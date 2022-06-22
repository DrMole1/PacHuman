using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Movement_Mobile : MonoBehaviour
{
    private Player_Movement pm;

    public FixedJoystick joystick;

    public float sensibility = 0.6f;
    
    private Button leftbtn;
    private Button rightbtn;
    private Button upbtn;
    private Button downbtn;

    public GameObject current;
    

    private void Awake()
    {
        pm = GameObject.Find("Player").GetComponent<Player_Movement>();

        leftbtn = GameObject.Find("MovLeft").GetComponent<Button>();
        rightbtn = GameObject.Find("MovRight").GetComponent<Button>();
        upbtn = GameObject.Find("MovUp").GetComponent<Button>();
        downbtn = GameObject.Find("MovDown").GetComponent<Button>();


#if UNITY_ANDROID && !UNITY_EDITOR
        current.SetActive(true);
        hideButtons();
#endif
#if UNITY_IOS && !UNITY_EDITOR
        current.SetActive(true);
        hideButtons();
#endif
#if UNITY_WEBGL && !UNITY_EDITOR
        current.SetActive(false);
#endif
#if UNITY_EDITOR
        current.SetActive(false);
#endif

    }

    public void hideButtons()
    {
        GameObject.Find("MovLeft").GetComponent<Image>().enabled = false;
        GameObject.Find("MovRight").GetComponent<Image>().enabled = false;
        GameObject.Find("MovUp").GetComponent<Image>().enabled = false;
        GameObject.Find("MovDown").GetComponent<Image>().enabled = false;
    }

    public void MoveRight()
    {
        rightbtn.onClick.Invoke();
    }
    public void MoveLeft()
    {
        leftbtn.onClick.Invoke();
    }
    public void MoveUp()
    {
        upbtn.onClick.Invoke();
    }
    public void MoveDown()
    {
        downbtn.onClick.Invoke();
    }

    private void Update()
    {

        if (joystick.Horizontal > sensibility && joystick.Vertical < sensibility && joystick.Vertical > -sensibility)
        {
            if (pm)
            {
                MoveRight();
            }
        }
        if (joystick.Horizontal < -sensibility && joystick.Vertical < sensibility && joystick.Vertical > -sensibility)
        {
            if (pm)
            {
                MoveLeft();
            }
                
        }
        if (joystick.Vertical > sensibility && joystick.Horizontal < sensibility && joystick.Horizontal > -sensibility)
        {
            if (pm)
            {
                MoveUp();
            }
            
        }
        if (joystick.Vertical < -sensibility && joystick.Horizontal < sensibility && joystick.Horizontal > -sensibility)
        {
            if (pm)
            {
                MoveDown();
            }
                
        }



    }


}
