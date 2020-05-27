using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPotion : Spawn
{
    [SerializeField] private float waitMinTime = 0.0f;
    [SerializeField] private float waitMaxTime = 3.0f;
    private bool isClosed;

    private void Start()
    {
        SpawnWithDelay();
    }

    public void SpawnWithDelay()
    {
        float randDelay = Random.Range(waitMinTime, waitMaxTime);
        StartCoroutine(SpawnPotionWithDelay(randDelay));
    }

    IEnumerator SpawnPotionWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneController.instance.SpawnPotion(transform);
        isClosed = true;
    }
}
