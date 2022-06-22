using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetection : MonoBehaviour
{
    public Transform node;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Node"))
        {
            //collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            node = collision.transform;
        }
        

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Node"))
        {
            //collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            node = collision.transform;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Node"))
        {
            //collision.gameObject.GetComponent<SpriteRenderer>().color = Color.green;

            node = null;
            
        }

        
    }


}
