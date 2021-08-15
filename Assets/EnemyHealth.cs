using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int maximumHitpoints = 5;
    private int currentHitpoints;
    
    void Start() {
        currentHitpoints = maximumHitpoints;
    }

    private void OnParticleCollision(GameObject other) {
        ProcessHit();
    }

    private void ProcessHit() {
        currentHitpoints--;
        if (currentHitpoints <= 0) {
            Destroy(gameObject);
        }
    }
}
