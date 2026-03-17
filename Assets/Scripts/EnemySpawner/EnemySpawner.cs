using UnityEngine;

// 일정 시간마다 몬스터를 생성하는 스포너
public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject[] enemyPrefabs; // 생성할 적 프리팹 목록
    [SerializeField] private Transform[] spawnPoints;   // 스폰 위치 목록

    [Header("Timing")]
    [SerializeField] private float spawnInterval = 2f;  // 생성 간격
    [SerializeField] private int maxSpawnCount = 10;    // 최대 생성 수
    [SerializeField] private bool autoStart = true;     // 시작 시 자동 실행 여부

    private float timer;
    private int currentSpawnCount;
    private bool isSpawning;

    private void Start()
    {
        // 자동 시작 설정 시 바로 스폰 시작
        if (autoStart)
        {
            StartSpawning();
        }
    }

    private void Update()
    {
        // 스폰 조건 체크
        if (!isSpawning) return;
        if (enemyPrefabs == null || enemyPrefabs.Length == 0) return;
        if (spawnPoints == null || spawnPoints.Length == 0) return;
        if (currentSpawnCount >= maxSpawnCount) return;

        // 시간 누적
        timer += Time.deltaTime;

        // 일정 시간마다 적 생성
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    // 스폰 시작
    public void StartSpawning()
    {
        isSpawning = true;
        timer = 0f;
    }

    // 스폰 중지
    public void StopSpawning()
    {
        isSpawning = false;
    }

    // 랜덤 위치에서 랜덤 적 생성
    private void SpawnEnemy()
    {
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        currentSpawnCount++;
    }
}