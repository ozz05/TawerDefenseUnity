using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaController : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform target;
    [SerializeField] float targetRange = 15f;
    [SerializeField] ParticleSystem projectileParticles;
    // Start is called before the first frame update
    void Start()
    {
        weapon = transform.Find("BallistaTopMesh");
        
    }
    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        if (target)
        {
            if (target.gameObject.activeInHierarchy) { return; }
        }
        EnemyMover[] enemyMovers = FindObjectsOfType<EnemyMover>();
        if (enemyMovers.Length > 0)
        {
            target = enemyMovers[0].transform;
            float closestDistance = Mathf.Infinity; 
            // Assume the first found EnemyMover as the target
            foreach(EnemyMover enemy in enemyMovers)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position); 
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = enemy.transform;
                }
            }
        }
        else
        {
            Debug.LogWarning("No EnemyMover found.");
        }
    }
    void AimWeapon()
    {
        // Rotate the partToRotate to look at the target's position
        if (weapon != null && target != null)
        {
            //Option A
            weapon.LookAt(target);
            //Option B
            //Vector3 direction = target.position - weapon.transform.position;
            //Quaternion rotation = Quaternion.LookRotation(direction);
            //weapon.transform.rotation = rotation;
            float targetDistance = Vector3.Distance(transform.position, target.position);
            if (targetDistance > targetRange)
            {
                Attack(false);
                target = null;
            }
            else
            {
                Attack(true);
            }
        }
    }
    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}
