using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [Tooltip("Adds amount to maxHitpoints")]
    [SerializeField] int difficultyRamp = 1;
    int currentHitPoints = 0;
    Enemy enemy;
    GameManager _gameManager;

    private void Start() {
        enemy = GetComponent<Enemy>();
        _gameManager = FindObjectOfType<GameManager>();
    }
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other) {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHitPoints --;
        if (currentHitPoints <= 0)
        {
            _gameManager?.EnemyKilled();
            enemy.RewardGold();
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp; 
        }
    }
}
