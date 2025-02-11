using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Array of different enemy prefabs we can spawn
    public GameObject[] enemyPrefabs;    // Now we can set multiple enemy types in Unity
    public Transform spawnPoint;
    public float spawnInterval = 3f;

    [Range(0, 1)]
    public float rangedEnemySpawnChance = 0.3f;    // 30% chance to spawn ranged enemy

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Randomly decide which enemy to spawn
            GameObject enemyToSpawn;

            // Simple random selection using spawn chance
            if (Random.value < rangedEnemySpawnChance)
            {
                // Spawn ranged enemy (assuming it's the second prefab in array)
                enemyToSpawn = enemyPrefabs[1];
            }
            else
            {
                // Spawn regular enemy (assuming it's the first prefab in array)
                enemyToSpawn = enemyPrefabs[0];
            }

            // Spawn the selected enemy
            Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}