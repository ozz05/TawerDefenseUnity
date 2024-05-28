using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _levelRequiredToSpawn = 1;
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;
    public int LevelRequiredToSpawn { get { return _levelRequiredToSpawn;}}

    Bank bank;
    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if(bank  == null) { return;}
        bank.Deposit(goldReward);
    }

    public void StealGold()
    {
        if(bank  == null) { return;}
        bank.Withdraw(goldPenalty);
    }
}
