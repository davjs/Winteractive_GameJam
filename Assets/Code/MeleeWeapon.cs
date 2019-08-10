using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {
    public Transform Weapon;
    public int attackForce = 1000;

    private bool Attacking = false;
    private int attackDegree = -45; 

    private WasdMovement _movement;
    private Rigidbody playerBody;

    // Start is called before the first frame update
    void Start() {
        _movement = GetComponent<WasdMovement>();
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (! Attacking && Input.GetMouseButtonDown(0)) {
            StartAttack();
        }

        if (Attacking) {
            var playerDirection = WasdMovement.LastDirection;
            var playerDirectionEuler = Vector2.SignedAngle(Vector2.right, playerDirection);
            var weaponEulerDirection = playerDirectionEuler + attackDegree;
            
            var playerPosition = transform.position;
            Weapon.SetPositionAndRotation(
                playerPosition + Vector3.back * 5,
                Quaternion.Euler(0, 0, weaponEulerDirection)
            );
            attackDegree += 10;
            if (attackDegree > 45) {
                StopAttack();
            }
        }
    }

    public void StopAttack() {
        _movement.enabled = true;
        Attacking = false;
        Weapon.gameObject.SetActive(false);
    }

    private void StartAttack() {
        Attacking = true;
        _movement.enabled = false;
        Weapon.gameObject.SetActive(true);
        attackDegree = -45;
        playerBody.AddForce(WasdMovement.LastDirection * attackForce, ForceMode.Impulse);
    }
}