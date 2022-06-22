using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private const float MICROVALUETOADD = 0.006f;

    private Camera cam;
    private float startingSize;
    private bool isShaking = false;

    private void Start()
    {
        cam = Camera.main;
        startingSize = cam.orthographicSize;
    }

    public void Shake()
    {
        if (isShaking)
            return;

        StartCoroutine(CoroutineShake());
        isShaking = true;
    }


    IEnumerator CoroutineShake()
    {
        while(cam.orthographicSize < startingSize + MICROVALUETOADD * 10)
        {
            yield return new WaitForSeconds(0.005f);
            cam.orthographicSize += MICROVALUETOADD;
        }

        while (cam.orthographicSize > startingSize)
        {
            yield return new WaitForSeconds(0.005f);
            cam.orthographicSize -= MICROVALUETOADD;
        }

        isShaking = false;
    }
}
