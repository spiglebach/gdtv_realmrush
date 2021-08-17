using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int maximumHitpoints = 5;
    [Tooltip("Adds amount to maximumHitpoints when the enemy dies.")]
    [SerializeField] private int difficultyRamp = 1;
    
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
            gameObject.SetActive(false);
            maximumHitpoints += difficultyRamp;
            enemy.RewardGold();
        }
    }
}
