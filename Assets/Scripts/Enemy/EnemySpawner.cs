using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField][Range(0, 50)] int poolSize = 5;
    [SerializeField][Range(0.1f, 30f)] float SpawnRate = 1f;
    GameObject[] pool;
    GameManager _gameManager;
    int _targetSpawnCount = 0;
    int _currentSpawnCount = 0;

    void Awake()
    {
        PopulatePool();
    }
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnEnemy());
        _targetSpawnCount = _gameManager?.GetEnemiesKillTarget() ?? 0;
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(EnemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }
    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (_currentSpawnCount < _targetSpawnCount)
            {
                EnableObjectInPool();
                _currentSpawnCount++;
            }
            else
            {
                EnemyMover[] enemyMovers = FindObjectsOfType<EnemyMover>();
                if (enemyMovers.Length == 0)
                {
                    if (_gameManager?.GetEnemiesKilled() < _targetSpawnCount)
                    {
                        _currentSpawnCount = _gameManager?.GetEnemiesKilled() ?? _currentSpawnCount - 1;
                    }
                }
            }
            yield return new WaitForSeconds(SpawnRate);
        }
        
    }
}
