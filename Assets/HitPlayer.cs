using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour {
    public int damage;
    private GameObject player;
    public Transform HitPosition;
    public float hitRadius;

    private void Start() {
        player = GameObject.FindWithTag("Player");
    }

    public void Hit() {
        if (Vector3.Distance(HitPosition.position, player.transform.position) < hitRadius) {
            
        }
    }
}
