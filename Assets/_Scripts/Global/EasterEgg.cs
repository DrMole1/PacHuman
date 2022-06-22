using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    public Transform[] enemies;
    public Sprite[] speachs;
    public GameObject speachPrefab;

    private void Start()
    {
        int choice = UnityEngine.Random.Range(0, 2);

        if (choice == 1)
        {
            StartCoroutine(StartEasterEgg());
        }
    }

    IEnumerator StartEasterEgg()
    {
        yield return new WaitForSeconds(8f);

        GameObject speach1;
        speach1 = Instantiate(speachPrefab, enemies[0].position, Quaternion.identity);
        speach1.GetComponent<SpriteRenderer>().sprite = speachs[0];
        StartCoroutine(SetPosition(speach1.transform));
        StartCoroutine(SetAlpha(speach1.GetComponent<SpriteRenderer>()));

        yield return new WaitForSeconds(3f);

        GameObject speach2;
        speach2 = Instantiate(speachPrefab, enemies[1].position, Quaternion.identity);
        speach2.GetComponent<SpriteRenderer>().sprite = speachs[1];
        StartCoroutine(SetPosition(speach2.transform));
        StartCoroutine(SetAlpha(speach2.GetComponent<SpriteRenderer>()));

        yield return new WaitForSeconds(3f);

        GameObject speach3;
        speach3 = Instantiate(speachPrefab, enemies[2].position, Quaternion.identity);
        speach3.GetComponent<SpriteRenderer>().sprite = speachs[2];
        StartCoroutine(SetPosition(speach3.transform));
        StartCoroutine(SetAlpha(speach3.GetComponent<SpriteRenderer>()));

        yield return new WaitForSeconds(3f);

        GameObject speach4;
        speach4 = Instantiate(speachPrefab, enemies[3].position, Quaternion.identity);
        speach4.GetComponent<SpriteRenderer>().sprite = speachs[3];
        StartCoroutine(SetPosition(speach4.transform));
        StartCoroutine(SetAlpha(speach4.GetComponent<SpriteRenderer>()));
    }

    IEnumerator SetPosition(Transform _transform)
    {
        while(_transform.localPosition.y <= 10)
        {
            _transform.position = new Vector2(_transform.position.x, _transform.position.y + 0.02f);
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(_transform.gameObject);
    }

    IEnumerator SetAlpha(SpriteRenderer _renderer)
    {
        while (_renderer.color.a > 0)
        {
            _renderer.color = new Color(1,1,1, _renderer.color.a - 0.05f);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
