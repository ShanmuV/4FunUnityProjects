using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public List<Transform> spawnPoints;

    public GameObject enemy;

    private int enemiesInWave;

    public float enemySpawnTimer = 1f;
    private float spawnCounter;

    private void Start()
    {
        spawnCounter = enemySpawnTimer;
    }

/*    private void Update()
    {
        if(spawnCounter < 0)
        {
            int index = Random.Range(0, spawnPoints.Count);
            Instantiate(enemy, spawnPoints[index].position, Quaternion.identity);
            spawnCounter = enemySpawnTimer;
        }
        spawnCounter -= Time.deltaTime;
    }*/

    public void BeginWave(int no_of_enemies)
    {
        enemiesInWave = no_of_enemies;
        StartCoroutine(StartWave());
    }

    public IEnumerator StartWave()
    {
        for(int i = 0; i < enemiesInWave; i++)
        {
            int index = Random.Range(0, spawnPoints.Count);
            Instantiate(enemy, spawnPoints[index].position, Quaternion.identity);
            spawnCounter = enemySpawnTimer;
            yield return new WaitForSeconds(enemySpawnTimer);
        }
    }
}
