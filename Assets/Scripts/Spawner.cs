using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Enemy enemy;

    Wave currentWave;
    int currentWaveNumber; // 현재웨이브

    int enemiesRemainingToSpawn; //남은 소환할 적의 수
    int enemiesRemainingAlive; //살아있는 적의 수
    float nextSpawnTime; //다음 번 소환 시간

    // Start is called before the first frame update
    void Start()
    {
        NextWave();   
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity);
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;
        if (enemiesReaminingAlive == 0)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        print("Wave: " + currentWaveNumber);
        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
        }
    }

    [Serializable]
    public class Wave
    {
        public int enemyCount; //소환할 적의 수
        public float timeBetweenSpawns; // 소환 딜레이
    }
}
