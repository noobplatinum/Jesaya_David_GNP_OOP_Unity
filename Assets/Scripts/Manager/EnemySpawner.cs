using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public BaseEnemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public bool isSpawning = false;

    private void Start()
    {
        spawnCount = defaultSpawnCount;
    }

    public void SpawnAll()
    {
        if (!isSpawning && spawnedEnemy.level <= combatManager.waveNumber)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        int enemiesSpawned = 0;
        while (isSpawning && enemiesSpawned < spawnCount)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnEnemy();
                enemiesSpawned++;
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        isSpawning = false; 
        combatManager.OnAllEnemiesSpawned();
    }

    private void SpawnEnemy()
    {
        if (spawnedEnemy != null)
        {
            combatManager.totalEnemies++;
            combatManager.OnEnemySpawned();
            BaseEnemy newEnemy = Instantiate(spawnedEnemy);
            newEnemy.GetComponent<BaseEnemy>().combatManager = combatManager;
            newEnemy.GetComponent<BaseEnemy>().enemySpawner = this;
        }
    }

    public void TotalEnemyCounter()
    {
        totalKill++;
        totalKillWave++;

        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            totalKillWave = 0;
            spawnCount = defaultSpawnCount + (spawnCountMultiplier * multiplierIncreaseCount);
            multiplierIncreaseCount++;
        }
    }

    public bool IsSpawning()
    {
        return isSpawning;
    }
}