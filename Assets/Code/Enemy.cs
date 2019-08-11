using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public float reach;
    public GameObject DeadPrefab;
    public MeshRenderer BodyMesh;

    private void Start() {
        Enemies.Add(this);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _body = GetComponent<Rigidbody>();
        OriginalPosition = transform.position;
        reach = 0.6f + Random.value * 1.0f;
    }

    private void OnDestroy() {
        Enemies.Remove(this);
    }

    // Update is called once per frame
    void Update() {
        if (_dead) return;
       
        var PlayerDistance = Vector2.Distance(transform.position, _player.position);
        var distanceFromGuardpos = Vector2.Distance(transform.position, OriginalPosition);
        var playerDistanceFromGaurdPos = Vector2.Distance(_player.position, OriginalPosition);
        
        if (guard && distanceFromGuardpos > 200 * reach) {
            retreating = true;
        }
        if (guard && playerDistanceFromGaurdPos > 150 * reach) {
            retreating = true;
        }

        if (retreating) {
            if (distanceFromGuardpos < 20) {
                retreating = false;
                return;
            }
            
            var direction = (OriginalPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
            Mesh.rotation = Quaternion.Euler(0,0 , Vector2.SignedAngle(Vector2.right, direction));
            _body.AddForce(direction * speed);
        }

        var canFollowPlayer = PlayerDistance < 120 * reach;
        if (guard) {
            canFollowPlayer = playerDistanceFromGaurdPos < 100 * reach;
        }

        if (canFollowPlayer) {
            var direction = (_player.position - transform.position).normalized;
            Mesh.rotation = Quaternion.Euler(0,0 , Vector2.SignedAngle(Vector2.right, direction));
            _body.AddForce(direction * speed);
        }

        if (!attacking && !retreating && Vector3.Distance(_player.position, transform.position) < 50.0f && Random.value < attackChance) {
            Animator.SetTrigger("Attack"); 
        }
    }
    
    
    public async void Damage(int dmg) {
        health -= dmg;
        if (health <= 0) {
            if (DeadPrefab) {
                var gore = Instantiate(DeadPrefab, transform.position, transform.rotation);
                Destroy(gore, 15);
            }
            Destroy(gameObject);
        }
        else {
            var oldMaterial = BodyMesh.material;
            BodyMesh.material = Prefabs.Get.HurtMaterial;
            await Task.Delay(50);
            if (BodyMesh != null) {
                BodyMesh.material = oldMaterial;
            }
        }
        
    }
}