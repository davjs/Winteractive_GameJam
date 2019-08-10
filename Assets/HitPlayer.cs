using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour {
    public int damage;
    private GameObject player;
    public Transform HitPosition;
    public float hitRadius;
    private Health _playerHealth;

    private void Start() {
        player = GameObject.FindWithTag("Player");
        _playerHealth = player.GetComponent<Health>();
    }

    public void Hit() {
        var distance = Vector3.Distance(HitPosition.position, player.transform.position);
        if (distance < hitRadius) {
            _playerHealth.Current -= damage;
        }
    }
}
