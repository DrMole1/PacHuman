using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // =============== VARIABLES ===============

    public bool wasPassed = false;

    // =========================================

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wasPassed = true;
        }
    }
}
