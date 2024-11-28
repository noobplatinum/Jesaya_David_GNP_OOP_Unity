using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f; 
    public int waveNumber = 1; 
    public int totalEnemies = 0;
    public int displayEnemies = 0;
    private bool timerReset = false; // Handler agar timer tidak direset berkali-kali
    private bool allEnemiesSpawned = true; // Jika seluruh enemy sudah muncul
    public int totalPts = 0;
    public string RosterStr = "";
    private MainUI mainUI;
    private void Start()
    {
        waveNumber = 0;
        mainUI = FindObjectOfType<MainUI>();
    }

    private void Update()
    {
        if (totalEnemies <= 0 && allEnemiesSpawned == true) // Jika seluruh enemy sudah mati dan tidak ada yang akan muncul
        {
            if (!timerReset)
            {
                timer = 0; 
                timerReset = true; // Reset ke 0 
            }
            timer += Time.deltaTime; // Tunggu waveInterval sebelum memulai wave baru
            if (timer >= waveInterval)
            {
                StartNewWave(); // Start
            }
        }
    }

    private void StartNewWave()
    {
        RosterStr = "Roster - ";
        timer = 0; // Set timer antarwave ke 0
        waveNumber++; 
        mainUI.StartCoroutine(mainUI.ShowWaveStart(waveNumber)); // Tampilkan wave start
        totalEnemies = 0; 
        timerReset = false; // Timer bisa direset lagi
        allEnemiesSpawned = false;

        foreach(EnemySpawner spawner in enemySpawners)
        {
            if (spawner.increaseSpawnCountNextWave) 
                {
                    spawner.spawnCount = spawner.defaultSpawnCount + (spawner.spawnCountMultiplier * spawner.multiplierIncreaseCount);
                    spawner.multiplierIncreaseCount++;
                    spawner.increaseSpawnCountNextWave = false;
                }
                spawner.SpawnAll(); // Spawn tiap jenis enemy

            if (spawner.isSpawning == true)
            {
                displayEnemies += spawner.spawnCount; // Hitung total enemy yang akan muncul
                switch(spawner.spawnedEnemy.name)
                {
                    case "HorizontalEnemy":
                        RosterStr += "Horizon, ";
                        break;
                    case "VerticalEnemy":
                        RosterStr += "Vertex, ";
                        break;
                    case "TargetingEnemy":
                        RosterStr += "Target, ";
                        break;
                    case "BossEnemy":
                        RosterStr += "Boss";
                        break;
                }
            }   
        }

    }

    public void TotalEnemyCounter()
    {
        totalEnemies--; // Count turun jika enemy mati
        displayEnemies--;
    }

    public void OnEnemySpawned()
    {
        timerReset = false; // Jika ada enemy, timer tidak akan direset
    }

    public void OnAllEnemiesSpawned()
    {
        foreach(EnemySpawner spawner in enemySpawners)
        {
            if (spawner.IsSpawning() == true)
            {
                return;
            }
        }
        allEnemiesSpawned = true; // Jika tiap enemy sudah muncul, maka timer bisa dilanjutkan
    }
}