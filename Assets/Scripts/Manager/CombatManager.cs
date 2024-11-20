using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;
    private bool timerReset = false;
    private bool allEnemiesSpawned = true;

    private void Start()
    {
        waveNumber = 0;
    }

    private void Update()
    {
        if (totalEnemies <= 0 && allEnemiesSpawned)
        {
            if (!timerReset)
            {
                timer = 0; 
                timerReset = true;
            }
            timer += Time.deltaTime;
            if (timer >= waveInterval)
            {
                StartNewWave();
            }
        }
    }

    private void StartNewWave()
    {
        timer = 0;
        waveNumber++;
        totalEnemies = 0;
        timerReset = false;
        allEnemiesSpawned = false;

        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.SpawnAll();
        }
    }

    public void TotalEnemyCounter()
    {
        totalEnemies--;
    }

    public void OnEnemySpawned()
    {
        timerReset = false;
    }

    public void OnAllEnemiesSpawned()
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (spawner.IsSpawning())
            {
                return;
            }
        }
        allEnemiesSpawned = true;
    }
}