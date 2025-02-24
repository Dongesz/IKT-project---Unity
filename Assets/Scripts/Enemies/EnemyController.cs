using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int maxEnemies = 2;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Update()
    {
        if (activeEnemies.Count < maxEnemies)
        {
            SpawnEnemies();    
        }
    }
    private void SpawnEnemies()
    {
        if(enemyPrefabs.Count == 0 || spawnPoints.Length == 0) return;
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Transform point = spawnPoints[Random.Range(0,spawnPoints.Length)];
        GameObject newEnemy = Instantiate(prefab, point.position, point.rotation);
        activeEnemies.Add(newEnemy);
    }
}
