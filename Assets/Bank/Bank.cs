using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour {
    [SerializeField] private int startingBalance = 150;
    [SerializeField] private TMP_Text goldDisplay;
    
    private int currentBalance;

    public int CurrentBalance => currentBalance;

    private void Awake() {
        currentBalance = startingBalance;
        DisplayGold();
    }

    public void Deposit(int amount) {
        currentBalance += Mathf.Abs(amount);
        DisplayGold();
    }

    public void Withdraw(int amount) {
        currentBalance -= Mathf.Abs(amount);
        DisplayGold();
        if (currentBalance < 0) {
            ReloadScene();
        }
    }

    private void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void DisplayGold() {
        goldDisplay.text = $"Gold: {currentBalance.ToString()}";
    }
}
