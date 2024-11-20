using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] public CombatManager combatManager;
    [SerializeField] public EnemySpawner enemySpawner;
    public int level;

    private void OnDestroy()
    {
        if (enemySpawner != null && combatManager != null)
        {
            enemySpawner.TotalEnemyCounter();
            combatManager.TotalEnemyCounter();
        }
    }
}
