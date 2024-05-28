using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField][Range(0, 50)] int poolSize = 5;
    [SerializeField][Range(0.1f, 30f)] float SpawnRate = 1f;
    [SerializeField] List<GameObject> _enemies = new List<GameObject>();
    GameObject[] pool;
    GameManager _gameManager;
    int _targetSpawnCount = 0;
    int _currentSpawnCount = 0;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _targetSpawnCount = _gameManager?.GetEnemiesKillTarget() ?? 0;
        PopulatePool();
        StartCoroutine(SpawnEnemy());
    }
    private List<GameObject> GetAvailableEnemies()
    {
        List<GameObject> enemies = new List<GameObject>();
        
        if (_enemies == null || _enemies.Count == 0)
        {
            return null;
        }
        foreach(var enemy in _enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                Debug.LogWarning("LevelRequiredToSpawn:: "+ _gameManager + " : " + _gameManager?.GetCurrentLevel());
                if (enemyScript.LevelRequiredToSpawn <= _gameManager?.GetCurrentLevel())
                {
                    Debug.LogWarning("Here:: ");
                    enemies.Add(enemy);
                }
            }
        }
        return enemies;
    }
    private GameObject GetRandomEnemy()
    {
        List<GameObject> enemies = GetAvailableEnemies();
        if (enemies == null || enemies.Count == 0)
        {
            return null;
        }
        // Get a random index from the list
        int randomIndex = Random.Range(0, enemies.Count);
        // Return the GameObject at the random index
        return enemies[randomIndex];
    }
    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            GameObject gameObject = GetRandomEnemy();
            pool[i] = Instantiate(gameObject, transform);
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
