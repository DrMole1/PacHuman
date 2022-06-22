using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetFinalScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.getScore().ToString();
    }
}
