using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private int goldReward = 25;
    [SerializeField] private int goldPenalty = 25;

    private Bank bank;
    
    void Start() {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold() {
        if (!bank) return;
        bank.Deposit(goldReward);
    }

    public void StealGold() {
        if (!bank) return;
        bank.Withdraw(goldPenalty);
    }
}
