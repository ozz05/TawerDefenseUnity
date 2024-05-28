using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] int towerPrice = 25;

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null) { return false;}

        if (bank.CurrentBalance >= towerPrice) 
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(towerPrice);
            return true;
        }
        else
        {
            return false;
        }
    }
}
