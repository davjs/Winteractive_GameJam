using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {
    public static List<Enemy> Enemies = new List<Enemy>();
    public int health = 3;
    public Transform Mesh;
    
    public float speed = 200;
    private Transform _player;

    private Rigidbody _body;
    private bool _dead = false;
    private bool attacking = false;
    private Vector2 OriginalPosition;
    public Animator Animator;
    public float attackChance = 0.1f;

    private void Start() {
        Enemies.Add(this);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _body = GetComponent<Rigidbody>();
        
    }

    private void OnDestroy() {
        Enemies.Remove(this);
    }

    // Update is called once per frame
    void Update() {
        if (_dead) return;

        if (Vector2.Distance(transform.position, _player.position) < 100) {
            var direction = (_player.position - transform.position).normalized;
            Mesh.rotation = Quaternion.Euler(0,0 , Vector2.SignedAngle(Vector2.right, direction));
            _body.AddForce(direction * speed);
        }

        if (!attacking && Vector3.Distance(_player.position, transform.position) < 50.0f && Random.value < attackChance) {
            Animator.SetTrigger("Attack");   
        }
    }
}