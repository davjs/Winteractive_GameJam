using System;
using UnityEngine;

public class HurtEnemy : MonoBehaviour {
    public int Damage;
    private Rigidbody playerBody;

    private void Start() {
        playerBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {
        var enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null) {
            enemy.health -= Damage;
            var towardsPlayerVector = (transform.position - playerBody.transform.position).normalized;
            playerBody.AddForce(towardsPlayerVector * 1000, ForceMode.Impulse);
            var enemyBody = enemy.gameObject.GetComponentInParent<Rigidbody>();
            playerBody.AddForce(towardsPlayerVector * 200, ForceMode.Impulse);
            enemyBody.AddForce(-towardsPlayerVector * 200, ForceMode.Impulse);
            if (enemy.health <= 0) {
                
            }
        }
    }
}