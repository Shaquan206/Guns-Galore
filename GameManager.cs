using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;

    public float enemyTimeBetweenSpawn;

    private bool isSpawning;

    private void Update()
    {
        if (isSpawning == false)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        isSpawning = true;
        yield return new WaitForSeconds(enemyTimeBetweenSpawn);
        Instantiate(enemyPrefab, player.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
        isSpawning = false;
    }
}
