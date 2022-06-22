using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackBonus : MonoBehaviour
{
    // ============== VARIABLES ==============

    public GameObject ptcBonusPrefab;

    private Vector3 startSize = default;
    private float growFactorX = 0.3f;
    private float growFactorY = 0.9f;
    private float delay = 0.022f;

    // =======================================


    private void Start()
    {
        startSize = new Vector2(transform.localScale.x, transform.localScale.y);
        StartCoroutine(Grow());
        StartCoroutine(SpawnParticles());
    }

    IEnumerator Grow()
    {
        while(transform.localScale.x < startSize.x + growFactorX)
        {
            yield return new WaitForSeconds(delay);
            transform.localScale = new Vector2(transform.localScale.x + growFactorX / 10, transform.localScale.y + growFactorY / 10);
        }

        while (transform.localScale.x > startSize.x)
        {
            yield return new WaitForSeconds(delay);
            transform.localScale = new Vector2(transform.localScale.x - growFactorX / 10, transform.localScale.y - growFactorY / 10);
        }
    }

    IEnumerator SpawnParticles()
    {
        GameObject ptcBonus;
        ptcBonus = Instantiate(ptcBonusPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(10f);

        StartCoroutine(SpawnParticles());
    }
}
