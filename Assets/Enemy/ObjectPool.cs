using System;
using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    [SerializeField] private int poolSize = 5;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTimer = 1f;

    private GameObject[] pool;

    private void Awake() {
        PopulatePool();
    }


    void Start() {
        StartCoroutine(SpawnEnemies());
    }

    private void PopulatePool() {
        pool = new GameObject[poolSize];
        for (var i = 0; i < poolSize; i++) {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }
    
    private IEnumerator SpawnEnemies() {
        while (Application.isPlaying) {
            EnableEnemyInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void EnableEnemyInPool() {
        foreach (var enemy in pool) {
            if (enemy.activeSelf) continue;
            
            enemy.SetActive(true);
            return;
        }
    }
}
