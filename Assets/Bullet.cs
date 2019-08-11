using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int damage = 2;

    void Start() {
        Destroy(gameObject, 6.0f);
    }

    private void OnCollisionEnter(Collision other) {
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.Damage(damage);
        }
        else {
            var destroyable = other.gameObject.GetComponent<DestroyAble>();
            if (destroyable) {
                destroyable.Damage(damage);
            }
        }
    }
}