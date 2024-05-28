using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private int _enemyKillTarget = 10;
    [SerializeField] TextMeshProUGUI _levelUI;
    private int _enemiesKilled = 0;

    private void Start() {
        if (_gameData != null)
        {
            float extraEnemies = (_gameData.CurrentLevel - 1 ) * _enemyKillTarget * 0.3f;
            _enemyKillTarget = _enemyKillTarget + (int)extraEnemies;
            if (_levelUI != null)
            {
                _levelUI.text = "Level " + _gameData.CurrentLevel;
            }
        }
    }
    public void StartGame()
    {
        _gameData.CurrentLevel = 1;
        SceneManager.LoadScene(1);
    }
    public int GetCurrentLevel()
    {
        return _gameData.CurrentLevel;
    }
    public int GetEnemiesKillTarget()
    {
        return _enemyKillTarget;
    }
    public int GetEnemiesKilled()
    {
        return _enemiesKilled;
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
