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

    public bool isSpawning = false; // Flag untuk mengetahui apakah enemy masih spawn
    private float spawnTimer = 0f; // Untuk interval spawn
    private int enemiesSpawned = 0; // Counter untuk enemy yang sudah muncul
    public bool increaseSpawnCountNextWave = false; 

    private void Start()
    {
        spawnCount = defaultSpawnCount;
    }

    private void Update()
    {
        if (isSpawning == true)
        {
            spawnTimer += Time.deltaTime; // Jika timer sudah mencapai interval spawn, spawn enemy
            if (spawnTimer >= spawnInterval && enemiesSpawned < spawnCount)
            {
                SpawnEnemy();
                enemiesSpawned++;
                spawnTimer = 0f; // Set timer interval kembali ke 0
            }

            if (enemiesSpawned >= spawnCount) // Jika sudah mencapai batas spawn, stop spawning
            {
                isSpawning = false;
                combatManager.OnAllEnemiesSpawned();
            }
        }
    }

    public void SpawnAll()
    {
        if (isSpawning == false && spawnedEnemy.level <= combatManager.waveNumber)
        {
            isSpawning = true;
            enemiesSpawned = 0;
            spawnTimer = 0f;
        }
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
        increaseSpawnCountNextWave = true; 
    }

    combatManager.totalPts += spawnedEnemy.level;
}

    public bool IsSpawning()
    {
        return isSpawning;
    }
}