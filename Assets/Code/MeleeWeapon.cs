using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {
    public Transform Weapon;

    private bool Attacking = false;

    private WasdMovement _movement;

    // Start is called before the first frame update
    void Start() {
        _movement = GetComponent<WasdMovement>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Attack();
        }
    }

    private async void Attack() {
        Attacking = true;
        _movement.enabled = false;
        var playerDirection = WasdMovement.LastDirection;
        var playerDirectionEuler = Vector2.SignedAngle(Vector2.right, playerDirection);
        Weapon.gameObject.SetActive(true);

        for (int i = -45; i < 45; i += 10) {
            var weaponEulerDirection = playerDirectionEuler + i;
            var playerPosition = transform.position;
            Weapon.SetPositionAndRotation(
                playerPosition,
                Quaternion.Euler(0, 0, weaponEulerDirection)
            );
            await Task.Delay(1);
            transform.Translate(playerDirection * 2f);
        }

        Weapon.gameObject.SetActive(false);
        _movement.enabled = true;
        Attacking = false;
    }
}