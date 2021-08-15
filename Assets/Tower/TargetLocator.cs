using System.Collections;
using UnityEngine;

public class TargetLocator : MonoBehaviour {
    [SerializeField] private Transform weapon;
    [SerializeField] private float scanTimer = 1f;
    [SerializeField] private float range = 50f;
    [SerializeField] private ParticleSystem projectileParticle;
    
    private Transform target;

    private void Update() {
        StartCoroutine(FindClosestTarget());
        AimWeapon();
    }

    private IEnumerator FindClosestTarget() {
        while (Application.isPlaying) {
            var position = transform.position;
            var closestDistance = -1f;
            target = null;
            foreach (var enemy in FindObjectsOfType<Enemy>()) {
                var distanceToEnemy = (enemy.transform.position - position).sqrMagnitude;
                if (!target) {
                    target = enemy.transform;
                    closestDistance = distanceToEnemy;
                } else {
                    if (distanceToEnemy < closestDistance) {
                        target = enemy.transform;
                    }
                }
            }
            yield return new WaitForSeconds(scanTimer);
        }
    }

    private void AimWeapon() {
        if (!target) {
            Attack(false);
            return;
        }
        var targetDistance = (target.position - transform.position).sqrMagnitude;
        weapon.LookAt(target);
        Attack(targetDistance <= range * range);
    }

    private void Attack(bool isAttacking) {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isAttacking;
    }
}
