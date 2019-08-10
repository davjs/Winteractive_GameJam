using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAble : MonoBehaviour {
    public float health = 100;
    public GameObject Drops;

    public void Damage(float dmg) {
        health -= dmg;
        if (health <= 0) {
            Instantiate(Drops, new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}