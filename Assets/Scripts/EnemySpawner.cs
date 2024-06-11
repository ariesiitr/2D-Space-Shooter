using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO;
    float maxSpawnRateInSecond = 5f;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemy()
    {
        Vector2 min  =Camera.main.ViewportToWorldPoint (new Vector2 (0,0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.transform.position = new Vector2(Random.Range(min.x,max.x),max.y);

        ScheduleNextEnemySpawn();
       
    }
    void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds;

        if (maxSpawnRateInSecond > 1f)
        {
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSecond);
        }
        else
            spawnInNSeconds = 1f;
        Invoke("SpawnEnemy", spawnInNSeconds);
    }
    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSecond > 1f)
            maxSpawnRateInSecond--;

        if (maxSpawnRateInSecond == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }
    public void ScheduledEnemySpawner()
    {
        maxSpawnRateInSecond = 5f;
        Invoke("SpawnEnemy", maxSpawnRateInSecond);

        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }
    public void UnscheduledEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
