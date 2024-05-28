using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private int _enemyKillTarget = 10;
    private int _enemiesKilled = 0;

    private void Start() {
        if (_gameData != null)
        {
            float extraEnemies = (((_gameData.CurrentLevel - 1 ) * _enemyKillTarget) * 0.3f);
            _enemyKillTarget = _enemyKillTarget + (int)extraEnemies;
        }
    }
    public void StartGame()
    {
        _gameData.CurrentLevel = 1;
        SceneManager.LoadScene(1);
    }

    public void EnemyKilled()
    {
        _enemiesKilled ++;
        if (_enemiesKilled >= _enemyKillTarget)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            _gameData.CurrentLevel ++;
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}
