using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform Player;

    [SerializeField] GameObject EnemyPrefab;

    float spawnTimer = 2f;
    float spawnTimeCounter;
    
    float minDistance = 2f;
    float maxDistance = 5f;


    private void Update()
    {
        if(spawnTimeCounter < 0)
        {
            Spawn();
            spawnTimeCounter = spawnTimer;
        }
        spawnTimeCounter -= Time.deltaTime;
    }

    private void Spawn()
    {
        Vector2 pos = GetRandomPosition();
        Instantiate(EnemyPrefab, pos, Quaternion.identity);
    }

    private Vector2 GetRandomPosition()
    {
        float randDistance = Random.Range(minDistance, maxDistance);

        float randAngle = Random.Range(0f, 2f*Mathf.PI);

        return new Vector2(Player.position.x + randDistance * Mathf.Cos(randAngle), Player.position.y + randDistance * Mathf.Sin(randAngle));
    }
}
