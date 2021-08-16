using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int maximumHitpoints = 5;
    private int currentHitpoints;

    private Enemy enemy;

    void OnEnable() {
        currentHitpoints = maximumHitpoints;
    }
    
    private void Start() {
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other) {
        ProcessHit();
    }

    private void ProcessHit() {
        currentHitpoints--;
        if (currentHitpoints <= 0) {
            enemy.RewardGold();
            gameObject.SetActive(false);
        }
    }
}
