using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public Enemy Prefab;
    public float Chance = 0.01f;
    private GameObject _player;
    public TextMesh TextMesh;

    private void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
        SpawnEnemy(130);
        SpawnEnemy(130);
        SpawnEnemy(130);
        SpawnEnemy(130);

        StartCoroutine(Wave("Wave 1",1, 50));
        StartCoroutine(Wave("MORE",2, 60));
        StartCoroutine(Wave("Get rect",4, 70));
        StartCoroutine(Wave("lol",6, 80));
    }

    IEnumerator Wave(string name,int min, int count) {
        yield return new WaitForSeconds(min * 60);
        for (int i = 0; i < count; i++) {
            var enemy = SpawnEnemy(80.0f * Random.value + 50) ;
            enemy.reach = 3.0f;
        }

        TextMesh.text = name;
        TextMesh.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        TextMesh.gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update() {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (enemies.Length < 100 && Random.value < Chance && Random.value < Chance) {
            SpawnEnemy(100);
        }
    }

    private Enemy SpawnEnemy(float additionalOffset) {
        Vector2 playerPos = _player.transform.position;
        var enemy = Instantiate(Prefab,
            playerPos
            + Random.insideUnitCircle.normalized * (Random.value * 200.0f + additionalOffset),
            Quaternion.identity);
        enemy.speed *= (0.8f + Random.value * 0.7f);
        return enemy;
    }
}