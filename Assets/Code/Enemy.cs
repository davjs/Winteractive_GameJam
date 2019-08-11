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
    public bool guard = false;
    public bool retreating = false;
    private Vector2 OriginalPosition;
    public Animator Animator;
    public float attackChance = 0.1f;

    private void Start() {
        Enemies.Add(this);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _body = GetComponent<Rigidbody>();
        OriginalPosition = transform.position;
    }

    private void OnDestroy() {
        Enemies.Remove(this);
    }

    // Update is called once per frame
    void Update() {
        if (_dead) return;
       
        var PlayerDistance = Vector2.Distance(transform.position, _player.position);
        var distanceFromGuardpos = Vector2.Distance(transform.position, OriginalPosition);
        
        if (guard && distanceFromGuardpos > 100) {
            retreating = true;
        }
        if (guard && distanceFromGuardpos > 30 && PlayerDistance > distanceFromGuardpos) {
            retreating = true;
        }

        if (retreating) {
            if (distanceFromGuardpos < 10) {
                retreating = false;
                return;
            }
            
            var direction = (OriginalPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
            Mesh.rotation = Quaternion.Euler(0,0 , Vector2.SignedAngle(Vector2.right, direction));
            _body.AddForce(direction * speed);
        }

        if (!retreating && PlayerDistance < 100) {
            var direction = (_player.position - transform.position).normalized;
            Mesh.rotation = Quaternion.Euler(0,0 , Vector2.SignedAngle(Vector2.right, direction));
            _body.AddForce(direction * speed);
        }

        if (!attacking && !retreating && Vector3.Distance(_player.position, transform.position) < 50.0f && Random.value < attackChance) {
            Animator.SetTrigger("Attack"); 
        }
    }
}