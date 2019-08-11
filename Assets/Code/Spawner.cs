using UnityEngine;

public class Spawner : MonoBehaviour {
    public Enemy Prefab;
    public float Chance = 0.01f;
    private GameObject _player;

    private void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update() {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length < 100 && Random.value < Chance && Random.value < Chance) {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        Vector2 playerPos = _player.transform.position;
        var enemy = Instantiate(Prefab,
            playerPos
            + Random.insideUnitCircle.normalized * (Random.value * 200.0f + 100.0f),
            Quaternion.identity);
        enemy.speed *= Random.value * 1.8f;
    }
}