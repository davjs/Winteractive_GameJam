using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Turrets {
    public static Observable<int> count;
}

public class FixedTurret : MonoBehaviour {
    public float fireRate = 1.0f;
    public Rigidbody bullet;
    public Transform shootPosition;
    public float shootForce = 100.0f;

    private void Start() {
        InvokeRepeating(nameof(Shoot), 0, 1 / fireRate);
    }

    void Shoot() {
        var instanceBullet = Instantiate(this.bullet, shootPosition.position, Quaternion.identity);
        instanceBullet.AddForce(transform.right * shootForce, ForceMode.Impulse );
    }
}
