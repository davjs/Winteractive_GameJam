using System;
using System.Threading.Tasks;
using UnityEngine;

public class HurtEnemy : MonoBehaviour {
    public int Damage;
    private Rigidbody playerBody;
    public MeleeWeapon weaponController;

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
            if (weaponController) {
//                weaponController.StopAttack();
            }
            if (enemy.health <= 0) {
                Destroy(enemy.gameObject);
            }

            await Task.Delay(100);
            playerBody.AddForce(towardsPlayerVector * 1000, ForceMode.Impulse);
        }
        else {
            var destroyable = other.GetComponent<DestroyAble>();
            if (destroyable) {
                destroyable.Damage(Damage);                
            }
        }
    }
}