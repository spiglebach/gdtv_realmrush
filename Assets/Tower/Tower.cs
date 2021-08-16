using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField] private int towerCost = 50;

    public bool CreateTowerAt(Tower tower, Vector3 position) {
        var bank = FindObjectOfType<Bank>();
        if (!bank) return false;
        if (bank.CurrentBalance < towerCost) {
            return false;
        }
        bank.Withdraw(towerCost);
        Instantiate(tower.gameObject, position, Quaternion.identity);
        return true;
    }
}
