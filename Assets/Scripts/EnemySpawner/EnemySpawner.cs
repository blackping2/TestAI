using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Timing")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxSpawnCount = 10;
    [SerializeField] private bool autoStart = true;

    private float timer;
    private int currentSpawnCount;
    private bool isSpawning;

    private void Start()
    {
        if (autoStart)
        {
            StartSpawning();
        }
    }

    private void Update()
    {
        if (!isSpawning) return;
        if (enemyPrefabs == null || enemyPrefabs.Length == 0) return;
        if (spawnPoints == null || spawnPoints.Length == 0) return;
        if (currentSpawnCount >= maxSpawnCount) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
        timer = 0f;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    private void SpawnEnemy()
    {
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        currentSpawnCount++;
    }
}