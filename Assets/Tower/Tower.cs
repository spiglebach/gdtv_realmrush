using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField] private int towerCost = 50;
    [SerializeField] private float buildTimeInSeconds = 1f;

    private void Start() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        StartCoroutine(Build());
    }

    private IEnumerator Build() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildTimeInSeconds);
        }
    }

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
