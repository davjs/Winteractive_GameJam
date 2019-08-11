using System;
using System.Threading.Tasks;
using UnityEngine;

public class HurtEnemy : MonoBehaviour {
    public int Damage;
    private Rigidbody playerBody;

    private void Start() {
        playerBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    private async void OnTriggerEnter(Collider other) {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.health -= Damage;
            var towardsPlayerVector = (transform.position - playerBody.transform.position).normalized;
            var enemyBody = enemy.gameObject.GetComponent<Rigidbody>();
            enemyBody.AddForce(-towardsPlayerVector * 1000, ForceMode.Impulse);
            enemy.Damage(Damage);
            
            await Task.Delay(100);
            if (playerBody != null) {
                playerBody.AddForce(towardsPlayerVector * 1000, ForceMode.Impulse);
            }
        }
        else {
            var destroyable = other.GetComponent<DestroyAble>();
            if (destroyable) {
                destroyable.Damage(Damage);                
            }
        }
    }
}